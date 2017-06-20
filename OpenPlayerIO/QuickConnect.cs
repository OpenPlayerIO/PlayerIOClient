using System.Collections.Generic;
using PlayerIOClient.Error;
using PlayerIOClient.Helpers;
using PlayerIOClient.Messages;
using PlayerIOClient.Messages.Client;

namespace PlayerIOClient
{
    public class QuickConnect
    {
        private readonly HttpChannel _channel;

        internal QuickConnect(HttpChannel channel)
        {
            _channel = channel;
        }

        #region Connect

        /// <summary> Connects to a game based on Player.IO as a simple user. </summary>
        /// <param name="gameId">
        /// The ID of the game you wish to connect to. This value can be found in the admin panel.
        /// </param>
        /// <param name="usernameOrEmail">
        /// The username or e-mail address of the user you wish to authenticate.
        /// </param>
        /// <param name="password"> The password of the user you wish to authenticate. </param>
        /// <param name="playerInsightSegments"></param>
        public Client SimpleConnect(string gameId, string usernameOrEmail, string password, string[] playerInsightSegments = null)
        {
            var simpleConnectArgs = new SimpleConnectArgs {
                GameId = gameId,
                UsernameOrEmail = usernameOrEmail,
                Password = password
            };
            var simpleConnectOutput =
                _channel.Request<SimpleConnectArgs, ConnectOutput, PlayerIOError>(400,
                                                                                  simpleConnectArgs);

            return new Client(_channel, simpleConnectOutput.Token, simpleConnectOutput.UserId);
        }

        /// <summary> Connects to a game based on Player.IO as a simple user. </summary>
        /// <param name="gameId">
        /// The ID of the game you wish to connect to. This value can be found in the admin panel.
        /// </param>
        /// <param name="usernameOrEmail">
        /// The username or e-mail address of the user you wish to authenticate.
        /// </param>
        /// <param name="password"> The password of the user you wish to authenticate. </param>
        /// <param name="playerInsightSegments"></param>
        /// <param name="successCallback"> A callback called when successfully connected. </param>
        /// <param name="errorCallback"> A callback called instead of <paramref name="successCallback"/> when an error occurs during connection. </param>
        public void SimpleConnect(string gameId, string usernameOrEmail, string password, string[] playerInsightSegments = null, Callback<Client> successCallback = null, Callback<PlayerIOError> errorCallback = null)
        {
            var simpleConnectArgs = new SimpleConnectArgs {
                GameId = gameId,
                UsernameOrEmail = usernameOrEmail,
                Password = password
            };
            var simpleConnectOutput =
                _channel.Request<SimpleConnectArgs, ConnectOutput, PlayerIOError>(400,
                                                                                  simpleConnectArgs,
                                                                                  errorCallback: errorCallback);

            if (simpleConnectOutput != null)
                successCallback(new Client(_channel, simpleConnectOutput.Token, simpleConnectOutput.UserId));
        }

        /// <summary> Connects to a game based on Player.IO as a Facebook user. </summary>
        /// <param name="gameId">
        /// The ID of the game you wish to connect to. This value can be found in the admin panel.
        /// </param>
        /// <param name="accessToken"> The Facebook access token of the user you wish to authenticate. </param>
        public Client FacebookOAuthConnect(string gameId, string accessToken)
        {
            var facebookConnectArgs = new FacebookOAuthConnectArgs { GameId = gameId, AccessToken = accessToken };
            var facebookConnectOutput =
                _channel.Request<FacebookOAuthConnectArgs, FacebookOAuthConnectOutput, PlayerIOError>(418,
                                                                                         facebookConnectArgs);
            return new Client(_channel, facebookConnectOutput.Token, facebookConnectOutput.UserId);
        }

        /// <summary> Connects to a game based on Player.IO as a Facebook user. </summary>
        /// <param name="gameId">
        /// The ID of the game you wish to connect to. This value can be found in the admin panel.
        /// </param>
        /// <param name="accessToken"> The Facebook access token of the user you wish to authenticate. </param>
        /// <param name="successCallback"> A callback called when successfully connected. </param>
        /// <param name="errorCallback"> A callback called instead of <paramref name="successCallback"/> when an error occurs during connection. </param>
        public void FacebookOAuthConnect(string gameId, string accessToken, Callback<Client> successCallback = null, Callback<PlayerIOError> errorCallback = null)
        {
            var facebookConnectArgs = new FacebookOAuthConnectArgs { GameId = gameId, AccessToken = accessToken };
            var facebookConnectOutput =
                _channel.Request<FacebookOAuthConnectArgs, FacebookOAuthConnectOutput, PlayerIOError>(418,
                                                                                         facebookConnectArgs);

            if (facebookConnectOutput != null)
                successCallback(new Client(_channel, facebookConnectOutput.Token, facebookConnectOutput.UserId));
        }

        /// <summary> Connects to a game based on Player.IO as a Kongregate user. </summary>
        /// <param name="gameId">
        /// The ID of the game you wish to connect to. This value can be found in the admin panel.
        /// </param>
        /// <param name="userId"> The Kongregate user ID of the user you wish to authenticate. </param>
        /// <param name="gameAuthToken">
        /// The Kongregate auth token of the game you wish to connect to (depends on the user you
        /// wish to authenticate).
        /// </param>
        public Client KongregateConnect(string gameId, string userId, string gameAuthToken)
        {
            var kongregateConnectArgs = new KongregateConnectArgs {
                GameId = gameId,
                UserId = userId,
                GameAuthToken = gameAuthToken
            };
            var kongregateConnectOutput =
                _channel.Request<KongregateConnectArgs, KongregateConnectOutput, PlayerIOError>(400,
                                                                                      kongregateConnectArgs);
            return new Client(_channel, kongregateConnectOutput.Token, kongregateConnectOutput.UserId);
        }

        /// <summary> Connects to a game based on Player.IO as a Kongregate user. </summary>
        /// <param name="gameId">
        /// The ID of the game you wish to connect to. This value can be found in the admin panel.
        /// </param>
        /// <param name="userId"> The Kongregate user ID of the user you wish to authenticate. </param>
        /// <param name="gameAuthToken">
        /// The Kongregate auth token of the game you wish to connect to (depends on the user you
        /// wish to authenticate).
        /// </param>
        /// <param name="successCallback"> A callback called when successfully connected. </param>
        /// <param name="errorCallback"> A callback called instead of <paramref name="successCallback"/> when an error occurs during connection. </param>
        public void KongregateConnect(string gameId, string userId, string gameAuthToken, Callback<Client> successCallback = null, Callback<PlayerIOError> errorCallback = null)
        {
            var kongregateConnectArgs = new KongregateConnectArgs {
                GameId = gameId,
                UserId = userId,
                GameAuthToken = gameAuthToken
            };
            var kongregateConnectOutput =
                _channel.Request<KongregateConnectArgs, KongregateConnectOutput, PlayerIOError>(400,
                                                                                      kongregateConnectArgs);

            if (kongregateConnectOutput != null)
                successCallback(new Client(_channel, kongregateConnectOutput.Token, kongregateConnectOutput.UserId));
        }

        /// <summary> Connects to a game based on Player.IO as a Steam user. </summary>
        /// <param name="gameId">
        /// The ID of the game you wish to connect to. This value can be found in the admin panel.
        /// </param>
        /// <param name="steamAppId">
        /// The Steam application ID of the game you wish to connect to.
        /// </param>
        /// <param name="steamSessionTicket">
        /// The Steam session ticket of the user you wish to authenticate.
        /// </param>
        public Client SteamConnect(string gameId, string steamAppId, string steamSessionTicket)
        {
            var steamConnectArgs = new SteamConnectArgs {
                GameId = gameId,
                SteamAppId = steamAppId,
                SteamSessionTicket = steamSessionTicket
            };
            var steamConnectOutput =
                _channel.Request<SteamConnectArgs, SteamConnectOutput, PlayerIOError>(421,
                                                                                 steamConnectArgs);
            return new Client(_channel, steamConnectOutput.Token, steamConnectOutput.UserId);
        }

        /// <summary> Connects to a game based on Player.IO as a Steam user. </summary>
        /// <param name="gameId">
        /// The ID of the game you wish to connect to. This value can be found in the admin panel.
        /// </param>
        /// <param name="steamAppId">
        /// The Steam application ID of the game you wish to connect to.
        /// </param>
        /// <param name="steamSessionTicket">
        /// The Steam session ticket of the user you wish to authenticate.
        /// </param>
        /// <param name="successCallback"> A callback called when successfully connected. </param>
        /// <param name="errorCallback"> A callback called instead of <paramref name="successCallback"/> when an error occurs during connection. </param>
        public void SteamConnect(string gameId, string steamAppId, string steamSessionTicket, Callback<Client> successCallback = null, Callback<PlayerIOError> errorCallback = null)
        {
            var steamConnectArgs = new SteamConnectArgs {
                GameId = gameId,
                SteamAppId = steamAppId,
                SteamSessionTicket = steamSessionTicket
            };
            var steamConnectOutput =
                _channel.Request<SteamConnectArgs, SteamConnectOutput, PlayerIOError>(421,
                                                                                 steamConnectArgs);

            if (steamConnectOutput != null)
                successCallback(new Client(_channel, steamConnectOutput.Token, steamConnectOutput.UserId));
        }

        #endregion Connect

        /// <summary> Registers a new user in the simple user database. </summary>
        /// <param name="gameId">
        /// The ID of the game you wish to register and connect to. This value can be found in the
        /// admin panel.
        /// </param>
        /// <param name="username"> The desired username of the new user. </param>
        /// <param name="password"> The desired password of the new user. </param>
        /// <param name="email"> The e-mail address of the new user. </param>
        /// <param name="captchaKey">
        /// Only if captcha is required: The key of the captcha image used to get the user to type in
        /// the captcha's value.
        /// </param>
        /// <param name="captchaValue">
        /// Only if captcha is required: The string the user entered in response to the captcha image.
        /// </param>
        /// <param name="extraData">
        /// Any extra data that you wish to store with the user, such as gender, birthdate, etc.
        /// </param>
        /// <returns> The Client of the newly registered user. </returns>
        public Client SimpleRegister(string gameId, string username, string password, string email = null, string captchaKey = null, string captchaValue = null, Dictionary<string, string> extraData = null)
        {
            var simpleRegisterArgs = new SimpleRegisterArgs {
                GameId = gameId,
                Username = username,
                Password = password,
                Email = email,
                CaptchaKey = captchaKey,
                CaptchaValue = captchaValue,
                ExtraData = Converter.Convert(extraData)
            };
            var simpleRegisterOutput = _channel.Request<SimpleRegisterArgs, SimpleRegisterOutput, PlayerIORegistrationError>(403,
                                                                                                                      simpleRegisterArgs);

            return new Client(_channel, simpleRegisterOutput.Token, simpleRegisterOutput.UserId);
        }

        /// <summary> Registers a new user in the simple user database. </summary>
        /// <param name="gameId">
        /// The ID of the game you wish to register and connect to. This value can be found in the
        /// admin panel.
        /// </param>
        /// <param name="username"> The desired username of the new user. </param>
        /// <param name="password"> The desired password of the new user. </param>
        /// <param name="email"> The e-mail address of the new user. </param>
        /// <param name="captchaKey">
        /// Only if captcha is required: The key of the captcha image used to get the user to type in
        /// the captcha's value.
        /// </param>
        /// <param name="captchaValue">
        /// Only if captcha is required: The string the user entered in response to the captcha image.
        /// </param>
        /// <param name="extraData">
        /// Any extra data that you wish to store with the user, such as gender, birthdate, etc.
        /// </param>
        /// <param name="successCallback"> A callback called when the user is successfully registered. </param>
        /// <param name="errorCallback"> A callback called instead of <paramref name="successCallback"/> when an error occurs during registration. </param>
        /// <returns> The Client of the newly registered user. </returns>
        public void SimpleRegister(string gameId, string username, string password, string email = null, string captchaKey = null, string captchaValue = null, Dictionary<string, string> extraData = null, Callback<Client> successCallback = null, Callback<PlayerIORegistrationError> errorCallback = null)
        {
            var simpleRegisterArgs = new SimpleRegisterArgs {
                GameId = gameId,
                Username = username,
                Password = password,
                Email = email,
                CaptchaKey = captchaKey,
                CaptchaValue = captchaValue,
                ExtraData = Converter.Convert(extraData)
            };
            var simpleRegisterOutput = _channel.Request<SimpleRegisterArgs, SimpleRegisterOutput, PlayerIORegistrationError>(403,
                                                                                                                      simpleRegisterArgs);

            if (simpleRegisterOutput != null)
                successCallback(new Client(_channel, simpleRegisterOutput.Token, simpleRegisterOutput.UserId));
        }

        /// <summary>
        /// Initiates the password recovery process for a user who has supplied an e-mail address
        /// during registration.
        /// </summary>
        /// <param name="gameId"> The ID of the game the user is registered in. </param>
        /// <param name="usernameOrEmail">
        /// The username or e-mail address of the user who wishes to recover their password.
        /// </param>
        public void SimpleRecoverPassword(string gameId, string usernameOrEmail)
        {
            var simpleRecoverPasswordArgs = new SimpleRecoverPasswordArgs {
                GameId = gameId,
                UsernameOrEmail = usernameOrEmail
            };
            _channel.Request<SimpleRecoverPasswordArgs, NoArgsOrOutput, PlayerIOError>(406,
                                                                                       simpleRecoverPasswordArgs);
        }
    }
}