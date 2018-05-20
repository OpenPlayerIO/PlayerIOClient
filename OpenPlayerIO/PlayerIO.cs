using System;
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

        public static bool UseSecureApiRequests = false;

        public static void SetAPIEndpoint(string endpoint)
        {
            Channel.SetEndpoint(
                PlayerIO.ServerApiEndpoints.Count == 0 ? endpoint :
                ((PlayerIO.ServerApiSecurity== ServerAPISecurity.UseHttps || (PlayerIO.ServerApiSecurity == ServerAPISecurity.RespectClientSetting && PlayerIO.UseSecureApiRequests)) ?
                "https://" : "http://") + endpoint + "/api");
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

        /// <summary>Connects to Player.IO using as the given user</summary>
        /// <param name="gameId">The game id of the game you wish to connect to. This value can be found in the admin panel</param>
        /// <param name="connectionId">The id of the connection, as given in the settings section of the admin panel. 'public' should be used as the default</param>
        /// <param name="authenticationArguments">A dictionary of arguments for the given connection.</param>
        /// <param name="playerInsightSegments">Custom segments for the user in PlayerInsight.</param>
        public static Client Authenticate(string gameId, string connectionId, Dictionary<string, string> authenticationArguments = null, string[] playerInsightSegments = null)
        {
            if (authenticationArguments?.ContainsKey("secureSimpleUserPasswordsOverHttp") == true && authenticationArguments["secureSimpleUserPasswordsOverHttp"] == "true") {
                var identifier = SimpleUserGetSecureLoginInfo();
                authenticationArguments["password"] = PlayerIO.SimpleUserPasswordEncrypt(identifier.PublicKey, authenticationArguments["password"]);
                authenticationArguments["nonce"] = identifier.Nonce;
            }

            var identifier2 = Authenticate(gameId, connectionId, authenticationArguments ?? null, playerInsightSegments?.ToList() ?? null, PlayerIO.GetClientAPI(), PlayerIO.GetClientInfo(), PlayerIO.GetPlayCodes());

            PlayerIO.ServerApiEndpoints = identifier2.ApiServerHosts;
            PlayerIO.ServerApiSecurity = identifier2.ApiSecurity;

            // TODO: Don't want to overwrite any custom user-set end-point...
            PlayerIO.SetAPIEndpoint(PlayerIO.ServerApiEndpoints[0]);

            return new Client(Channel, gameId, identifier2.GameFSRedirectMap, identifier2.Token, identifier2.UserId, identifier2.ShowBranding, identifier2.IsSocialNetworkUser, null);
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