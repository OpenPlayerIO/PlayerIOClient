﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using OpenPIO.PlayerIOClient.Enums;
using PlayerIOClient.Error;
using PlayerIOClient.Helpers;
using PlayerIOClient.Messages.Client;

namespace PlayerIOClient
{
    /// <summary> Entry class for the initial connection to Player.IO. </summary>
    public static class PlayerIO
    {
        private static readonly Lazy<QuickConnect> _quickConnect = new Lazy<QuickConnect>(() => new QuickConnect(Channel));

        private static readonly HttpChannel Channel = new HttpChannel();
        public static QuickConnect QuickConnect => _quickConnect.Value;

        public static bool UseProxyForAPIRequests = false;
        public static bool UseSecureApiRequests = false;

        public static void SetAPIEndpoint(string endpoint)
        {
            Channel.SetEndpoint(
                ServerApiEndpoints.Count == 0 ? endpoint :
                ((ServerApiSecurity == ServerAPISecurity.UseHttps || (ServerApiSecurity == ServerAPISecurity.RespectClientSetting && UseSecureApiRequests)) ?
                "https://" : "http://") + endpoint + "/api");
        }

        public static string APIProxy { get; set; }

        public static void SetAPIProxy(string proxy, bool enable = true)
        {
            APIProxy = proxy;
            UseProxyForAPIRequests = enable;
        }

        internal static List<string> ServerApiEndpoints = new List<string>();
        internal static ServerAPISecurity ServerApiSecurity;

        /// <summary> Connects to a game based on Player.IO as the given user. </summary>
        /// <param name="gameId">
        /// The ID of the game you wish to connect to. This value can be found in the admin panel.
        /// </param>
        /// <param name="connectionId">
        /// The ID of the connection, as given in the settings section of the admin panel. 'public'
        /// should be used as the default.
        /// </param>
        /// <param name="userId"> The ID of the user you wish to authenticate. </param>
        /// <param name="auth">
        /// If the connection identified by ConnectionIdentifier only accepts authenticated requests:
        /// The auth value generated based on 'userId'. You can generate an auth value using the
        /// CalcAuth() method.
        /// </param>
        public static Client Connect(string gameId, string connectionId, string userId, string auth = null)
        {
            var connectArg = new ConnectArgs {
                GameId = gameId,
                ConnectionId = connectionId,
                UserId = userId,
                Auth = auth
            };

            var connectOutput = Channel.Request<ConnectArgs, ConnectOutput, PlayerIOError>(10, connectArg);
            return new Client(Channel, connectOutput.Token, connectOutput.UserId);
        }

        /// <summary> Connects to a game based on Player.IO as the given user. </summary>
        /// <param name="gameId">
        /// The ID of the game you wish to connect to. This value can be found in the admin panel.
        /// </param>
        /// <param name="connectionId">
        /// The ID of the connection, as given in the settings section of the admin panel. 'public'
        /// should be used as the default.
        /// </param>
        /// <param name="userId"> The ID of the user you wish to authenticate. </param>
        /// <param name="auth">
        /// If the connection identified by ConnectionIdentifier only accepts authenticated requests:
        /// The auth value generated based on 'userId'. You can generate an auth value using the
        /// CalcAuth() method.
        /// </param>
        /// <param name="successCallback"> A callback called when successfully connected. </param>
        /// <param name="errorCallback"> A callback called instead of <paramref name="successCallback"/> when an error occurs during connection. </param>
        public static void Connect(string gameId, string connectionId, string userId, string auth = null, Callback<Client> successCallback = null, Callback<PlayerIOError> errorCallback = null)
        {
            var connectOutput = Channel.Request<ConnectArgs, ConnectOutput, PlayerIOError>(10, new ConnectArgs {
                GameId = gameId,
                ConnectionId = connectionId,
                UserId = userId,
                Auth = auth
            }, errorCallback);

            if (connectOutput != null)
                successCallback(new Client(Channel, connectOutput.Token, connectOutput.UserId));
        }

        /// <summary> Calculate an auth hash for use in the Connect method. </summary>
        /// <param name="userId"> The UserID to use when generating the hash. </param>
        /// <param name="sharedSecret">
        /// The shared secret to use when generating the hash. This must be the same value as the one
        /// given to a connection in the admin panel.
        /// </param>
        public static string CalcAuth(string userId, string sharedSecret)
        {
            var unixTime = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
            using (var hmacInstance = new HMACSHA1(Encoding.UTF8.GetBytes(sharedSecret))) {
                var hmacHash = hmacInstance.ComputeHash(Encoding.UTF8.GetBytes(unixTime + ":" + userId));

                var strBld = new StringBuilder(unixTime + ":" + BitConverter.ToString(hmacHash));
                return strBld.Replace("-", "").ToString().ToLower(System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        /// <summary> Connects to Player.IO using as the given user </summary>
        /// <param name="gameId"> The game ID of the game you wish to connect to. This value can be found in the admin panel </param>
        /// <param name="connectionId"> The ID of the connection, as given in the settings section of the admin panel. 'public' should be used as the default </param>
        /// <param name="authenticationArguments"> A dictionary of arguments for the given connection. </param>
        /// <param name="playerInsightSegments"> Custom segments for the user in PlayerInsight. </param>
        public static Client Authenticate(string gameId, string connectionId, Dictionary<string, string> authenticationArguments = null, string[] playerInsightSegments = null)
        {
            if (authenticationArguments?.ContainsKey("secureSimpleUserPasswordsOverHttp") == true && authenticationArguments["secureSimpleUserPasswordsOverHttp"] == "true") {
                var secureLoginInfo = SimpleUserGetSecureLoginInfo();
                authenticationArguments["password"] = PlayerIO.SimpleUserPasswordEncrypt(secureLoginInfo.PublicKey, authenticationArguments["password"]);
                authenticationArguments["nonce"] = secureLoginInfo.Nonce;
            }

            var authenticationOutput = Authenticate(gameId, connectionId, authenticationArguments ?? null, playerInsightSegments?.ToList() ?? null, GetClientAPI(), GetClientInfo(), GetPlayCodes());

            PlayerIO.ServerApiEndpoints = authenticationOutput.ApiServerHosts;
            PlayerIO.ServerApiSecurity = authenticationOutput.ApiSecurity;

            // TODO: Don't want to overwrite any custom user-set endpoint...
            PlayerIO.SetAPIEndpoint(PlayerIO.ServerApiEndpoints[0]);

            return new Client(Channel, gameId,
                authenticationOutput.GameFSRedirectMap, 
                authenticationOutput.Token, 
                authenticationOutput.UserId,
                authenticationOutput.ShowBranding,
                authenticationOutput.IsSocialNetworkUser, null);
        }

        /// <summary> Connects to Player.IO using as the given user </summary>
        /// <param name="gameId"> The game ID of the game you wish to connect to. This value can be found in the admin panel </param>
        /// <param name="connectionId"> The ID of the connection, as given in the settings section of the admin panel. 'public' should be used as the default </param>
        /// <param name="authenticationArguments"> A dictionary of arguments for the given connection. </param>
        /// <param name="playerInsightSegments"> Custom segments for the user in PlayerInsight. </param>
        /// <param name="successCallback"> A callback called when successfully connected. </param>
        /// <param name="errorCallback"> A callback called instead of <paramref name="successCallback"/> when an error occurs during connection. </param>
        public static void Authenticate(string gameId, string connectionId, Dictionary<string, string> authenticationArguments = null, string[] playerInsightSegments = null, Callback<Client> successCallback = null, Callback<PlayerIOError> errorCallback = null)
        {
            var authenticationOutput = Channel.Request<AuthenticateArgs, AuthenticateOutput, PlayerIOError>(13, new AuthenticateArgs
            {
                GameId = gameId,
                ConnectionId = connectionId,
                AuthenticationArguments = Converter.Convert(authenticationArguments ?? new Dictionary<string, string>()),
                PlayerInsightSegments = playerInsightSegments?.ToList() ?? new List<string>(),
                ClientAPI = GetClientAPI(),
                ClientInfo = Converter.Convert(GetClientInfo()),
                PlayCodes = GetPlayCodes()
            }, errorCallback);

            if (authenticationOutput != null)
            {
                PlayerIO.ServerApiEndpoints = authenticationOutput.ApiServerHosts;
                PlayerIO.ServerApiSecurity = authenticationOutput.ApiSecurity;

                // TODO: Don't want to overwrite any custom user-set endpoint...
                PlayerIO.SetAPIEndpoint(PlayerIO.ServerApiEndpoints[0]);

                successCallback(new Client(Channel, gameId,
                    authenticationOutput.GameFSRedirectMap,
                    authenticationOutput.Token,
                    authenticationOutput.UserId,
                    authenticationOutput.ShowBranding,
                    authenticationOutput.IsSocialNetworkUser, null));
            }
        }

        internal static AuthenticateOutput Authenticate(string gameId, string connectionId, Dictionary<string, string> authenticationArguments, List<string> playerInsightSegments, string clientAPI, Dictionary<string, string> clientInfo, List<string> playCodes)
        {
            return Channel.Request<AuthenticateArgs, AuthenticateOutput, PlayerIOError>(13, new AuthenticateArgs {
                GameId = gameId,
                ConnectionId = connectionId,
                AuthenticationArguments = Converter.Convert(authenticationArguments ?? new Dictionary<string, string>()),
                PlayerInsightSegments = playerInsightSegments ?? new List<string>(),
                ClientAPI = clientAPI,
                ClientInfo = Converter.Convert(clientInfo),
                PlayCodes = playCodes
            });
        }

        internal static SimpleUserGetSecureLoginInfoOutput SimpleUserGetSecureLoginInfo()
        {
            return Channel.Request<SimpleUserGetSecureLoginInfoArgs, SimpleUserGetSecureLoginInfoOutput, PlayerIOError>(424, new SimpleUserGetSecureLoginInfoArgs());
        }

        internal static string SimpleUserPasswordEncrypt(byte[] certificateBytes, string password)
        {
            var rsacryptoServiceProvider = new RSACryptoServiceProvider(new CspParameters {
                ProviderType = 1
            });

            rsacryptoServiceProvider.ImportCspBlob(certificateBytes);
            var bytes = Encoding.UTF8.GetBytes(password);
            var inArray = rsacryptoServiceProvider.Encrypt(bytes, false);
            return Convert.ToBase64String(inArray);
        }

        internal static List<string> GetPlayCodes()
        {
            return new List<string>();
        }

        internal static string GetClientAPI()
        {
            return "csharp";
        }

        internal static Dictionary<string, string> GetClientInfo()
        {
            var dictionary = new Dictionary<string, string>();
            try {
                dictionary["os"] = Environment.OSVersion.Platform + "/" + Environment.OSVersion.VersionString;
            }
            catch { }
            return dictionary;
        }
    }
}