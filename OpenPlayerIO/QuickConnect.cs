using System;
using System.Collections.Generic;
using System.Threading;
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
        public Client SimpleConnect(string gameId, string usernameOrEmail, string password, string[] playerInsightSegments = null)
        {
            var simpleConnectOutput = _channel.Request<SimpleConnectArgs, ConnectOutput, PlayerIOError>(400, new SimpleConnectArgs {
                GameId = gameId,
                UsernameOrEmail = usernameOrEmail,
                Password = password
            });

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
        /// <param name="successCallback"> A callback called when successfully connected. </param>
        /// <param name="errorCallback"> A callback called instead of <paramref name="successCallback"/> when an error occurs during connection. </param>
        public void SimpleConnect(string gameId, string usernameOrEmail, string password, string[] playerInsightSegments = null, Callback<Client> successCallback = null, Callback<PlayerIOError> errorCallback = null)
        {
            var simpleConnectOutput = _channel.Request<SimpleConnectArgs, ConnectOutput, PlayerIOError>(400, new SimpleConnectArgs {
                GameId = gameId,
                UsernameOrEmail = usernameOrEmail,
                Password = password
            }, errorCallback);

            if (simpleConnectOutput != null)
                successCallback(new Client(_channel, simpleConnectOutput.Token, simpleConnectOutput.UserId));
        }

        /// <summary> Connects to a game based on Player.IO as a Facebook user. </summary>
        /// <param name="gameId"> The ID of the game you wish to connect to. This value can be found in the admin panel. </param>
        /// <param name="accessToken"> The Facebook access token of the user you wish to authenticate. </param>
        public Client FacebookOAuthConnect(string gameId, string accessToken)
        {
            var facebookConnectOutput = _channel.Request<FacebookOAuthConnectArgs, FacebookOAuthConnectOutput, PlayerIOError>(418, new FacebookOAuthConnectArgs {
                GameId = gameId,
                AccessToken = accessToken
            });

            return new Client(_channel, facebookConnectOutput.Token, facebookConnectOutput.UserId);
        }

        /// <summary> Connects to a game based on Player.IO as a Facebook user. </summary>
        /// <param name="gameId"> The ID of the game you wish to connect to. This value can be found in the admin panel. </param>
        /// <param name="accessToken"> The Facebook access token of the user you wish to authenticate. </param>
        /// <param name="successCallback"> A callback called when successfully connected. </param>
        /// <param name="errorCallback"> A callback called instead of <paramref name="successCallback"/> when an error occurs during connection. </param>
        public void FacebookOAuthConnect(string gameId, string accessToken, Callback<Client> successCallback = null, Callback<PlayerIOError> errorCallback = null)
        {
            var facebookConnectOutput = _channel.Request<FacebookOAuthConnectArgs, FacebookOAuthConnectOutput, PlayerIOError>(418, new FacebookOAuthConnectArgs {
                GameId = gameId,
                AccessToken = accessToken
            }, errorCallback);

            if (facebookConnectOutput != null)
                successCallback(new Client(_channel, facebookConnectOutput.Token, facebookConnectOutput.UserId));
        }

        /// <summary> Connects to a game based on Player.IO as a Kongregate user. </summary>
        /// <param name="gameId"> The ID of the game you wish to connect to. This value can be found in the admin panel. </param>
        /// <param name="userId"> The Kongregate user ID of the user you wish to authenticate. </param>
        /// <param name="gameAuthToken">
        /// The Kongregate auth token of the game you wish to connect to (depends on the user you
        /// wish to authenticate).
        /// </param>
        public Client KongregateConnect(string gameId, string userId, string gameAuthToken)
        {
            var kongregateConnectOutput = _channel.Request<KongregateConnectArgs, KongregateConnectOutput, PlayerIOError>(400, new KongregateConnectArgs {
                GameId = gameId,
                UserId = userId,
                GameAuthToken = gameAuthToken
            });

            return new Client(_channel, kongregateConnectOutput.Token, kongregateConnectOutput.UserId);
        }

        /// <summary> Connects to a game based on Player.IO as a Kongregate user. </summary>
        /// <param name="gameId"> The ID of the game you wish to connect to. This value can be found in the admin panel. </param>
        /// <param name="userId"> The Kongregate user ID of the user you wish to authenticate. </param>
        /// <param name="gameAuthToken">
        /// The Kongregate auth token of the game you wish to connect to (depends on the user you
        /// wish to authenticate).
        /// </param>
        /// <param name="successCallback"> A callback called when successfully connected. </param>
        /// <param name="errorCallback"> A callback called instead of <paramref name="successCallback"/> when an error occurs during connection. </param>
        public void KongregateConnect(string gameId, string userId, string gameAuthToken, Callback<Client> successCallback = null, Callback<PlayerIOError> errorCallback = null)
        {
            var kongregateConnectOutput = _channel.Request<KongregateConnectArgs, KongregateConnectOutput, PlayerIOError>(400, new KongregateConnectArgs {
                GameId = gameId,
                UserId = userId,
                GameAuthToken = gameAuthToken
            }, errorCallback);

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
            var steamConnectOutput = _channel.Request<SteamConnectArgs, SteamConnectOutput, PlayerIOError>(421, new SteamConnectArgs {
                GameId = gameId,
                SteamAppId = steamAppId,
                SteamSessionTicket = steamSessionTicket
            }, errorCallback);

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
            var simpleRegisterOutput = _channel.Request<SimpleRegisterArgs, SimpleRegisterOutput, PlayerIORegistrationError>(403, new SimpleRegisterArgs {
                GameId = gameId,
                Username = username,
                Password = password,
                Email = email,
                CaptchaKey = captchaKey,
                CaptchaValue = captchaValue,
                ExtraData = Converter.Convert(extraData)
            });

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
            var simpleRegisterOutput = _channel.Request<SimpleRegisterArgs, SimpleRegisterOutput, PlayerIORegistrationError>(403, new SimpleRegisterArgs {
                GameId = gameId,
                Username = username,
                Password = password,
                Email = email,
                CaptchaKey = captchaKey,
                CaptchaValue = captchaValue,
                ExtraData = Converter.Convert(extraData)
            }, errorCallback);

            if (simpleRegisterOutput != null)
                successCallback(new Client(_channel, simpleRegisterOutput.Token, simpleRegisterOutput.UserId));
        }

        /// <summary>
        /// Initiates the password recovery process for a user who has supplied an e-mail address
        /// during registration.
        /// </summary>
        /// <param name="gameId"> The ID of the game the user is registered in. </param>
        /// <param name="usernameOrEmail"> The username or e-mail address of the user who wishes to recover their password. </param>
        public void SimpleRecoverPassword(string gameId, string usernameOrEmail)
        {
            _channel.Request<SimpleRecoverPasswordArgs, NoArgsOrOutput, PlayerIOError>(406, new SimpleRecoverPasswordArgs {
                GameId = gameId,
                UsernameOrEmail = usernameOrEmail
            });
        }

        /// <summary>
        /// Changes the email address of the specified user using the username provided.
        /// </summary>
        /// <param name="connectionId"> For example, 'simpleUsers' </param>
        /// <param name="currentUsername"> The username of the user </param>
        /// <param name="currentPassword"> The password of the user </param>
        /// <param name="newEmail"> The new email address for the user </param>
        /// <param name="requestResponseMessage"> The response received from Player.IO </param>
        /// <returns> Whether the email address has been successfully changed. </returns>
        public bool SimpleChangeEmailByUsername(string gameId, string connectionId, string currentUsername, string currentPassword, string newEmail, out string requestResponseMessage) =>
            this.SimpleChangeEmail(gameId, connectionId, "username", currentUsername, currentPassword, newEmail, out requestResponseMessage);

        /// <summary>
        /// Changes the email address of the specified user using the email address provided.
        /// </summary>
        /// <param name="connectionId"> For example, 'simpleUsers' </param>
        /// <param name="currentEmail"> The email of the user </param>
        /// <param name="currentPassword"> The password of the user </param>
        /// <param name="newEmail"> The new email address for the user </param>
        /// <param name="requestResponseMessage"> The response received from Player.IO </param>
        /// <returns> Whether the email address has been successfully changed. </returns>
        public bool SimpleChangeEmailByEmail(string gameId, string connectionId, string currentEmail, string currentPassword, string newEmail, out string requestResponseMessage) =>
            this.SimpleChangeEmail(gameId, connectionId, "email", currentEmail, currentPassword, newEmail, out requestResponseMessage);

        /// <summary>
        /// Changes the password of the specified user using the username provided.
        /// </summary>
        /// <param name="connectionId"> For example, 'simpleUsers' </param>
        /// <param name="currentUsername"> The username of the user </param>
        /// <param name="currentPassword"> The password of the user </param>
        /// <param name="newPassword"> The new password for the user </param>
        /// <param name="requestResponseMessage"> The response received from Player.IO </param>
        /// <returns> Whether the password has been successfully changed. </returns>
        public bool SimpleChangePasswordByUsername(string gameId, string connectionId, string currentUsername, string currentPassword, string newPassword, out string requestResponseMessage) =>
            this.SimpleChangePassword(gameId, connectionId, "username", currentUsername, currentPassword, newPassword, out requestResponseMessage);

        /// <summary>
        /// Changes the password of the specified user using the email address provided.
        /// </summary>
        /// <param name="connectionId"> For example, 'simpleUsers' </param>
        /// <param name="currentEmail"> The email of the user </param>
        /// <param name="currentPassword"> The password of the user </param>
        /// <param name="newPassword"> The new password for the user </param>
        /// <param name="requestResponseMessage"> The response received from Player.IO </param>
        /// <returns> Whether the password has been successfully changed. </returns>
        public bool SimpleChangePasswordByEmail(string gameId, string connectionId, string currentEmail, string currentPassword, string newPassword, out string requestResponseMessage) =>
            this.SimpleChangePassword(gameId, connectionId, "email", currentEmail, currentPassword, newPassword, out requestResponseMessage);

        #region Authenticate
        internal bool SimpleChangePassword(string gameId, string connectionId, string connectionType, string currentUsernameOrEmail, string currentPassword, string newPassword, out string requestResponse)
        {
            var requestFinished = false;
            var changeSuccessful = false;
            var playerIOError = default(PlayerIOError);

            _channel.Request<AuthenticateArgs, NoArgsOrOutput, PlayerIOError>(13, new AuthenticateArgs
            {
                GameId = gameId,
                ClientAPI = PlayerIO.GetClientAPI(),
                ConnectionId = connectionId,
                AuthenticationArguments = new KeyValuePair[] {
                    new KeyValuePair() { Key = connectionType, Value = currentUsernameOrEmail },
                    new KeyValuePair() { Key = "password", Value = currentPassword },
                    new KeyValuePair() { Key = "changepassword", Value = "true" },
                    new KeyValuePair() { Key = "newpassword", Value = newPassword }
                }
            }, new Callback<PlayerIOError>((error) => {
                if (error.ErrorCode == ErrorCode.GeneralError && error.Message.ToLower().Contains("email address changed"))
                    changeSuccessful = true;

                playerIOError = error;
                requestFinished = true;
            }));

            // this is really sloppy and could be improved, but it's quick and easy...
            // and the same description would generally apply to Player.IO as a whole :wink:
            // - atillabyte

            requestResponse = playerIOError?.Message ?? "";

            var requestTimeout = 0;
            while (!requestFinished && ++requestTimeout <= 10000 / 100)
                Thread.Sleep(100);

            if (!requestFinished)
                throw new Exception("The email change request timed out without returning a response.");

            if (requestFinished && changeSuccessful)
                return true;

            return false;
        }

        internal bool SimpleChangeEmail(string gameId, string connectionId, string connectionType, string currentUsernameOrEmail, string currentPassword, string newEmail, out string requestResponse)
        {
            var requestFinished = false;
            var changeSuccessful = false;
            var playerIOError = default(PlayerIOError);

            _channel.Request<AuthenticateArgs, NoArgsOrOutput, PlayerIOError>(13, new AuthenticateArgs
            {
                GameId = gameId,
                ClientAPI = PlayerIO.GetClientAPI(),
                ConnectionId = connectionId,
                AuthenticationArguments = new KeyValuePair[] {
                    new KeyValuePair() { Key = connectionType, Value = currentUsernameOrEmail },
                    new KeyValuePair() { Key = "password", Value = currentPassword },
                    new KeyValuePair() { Key = "changeemail", Value = "true" },
                    new KeyValuePair() { Key = "newemail", Value = newEmail }
                }
            }, new Callback<PlayerIOError>((error) => {
                if (error.ErrorCode == ErrorCode.GeneralError && error.Message.ToLower().Contains("email address changed"))
                    changeSuccessful = true;
                
                playerIOError = error;
                requestFinished = true;
            }));

            // this is really sloppy and could be improved, but it's quick and easy...
            // and the same description would generally apply to Player.IO as a whole :wink:
            // - atillabyte

            requestResponse = playerIOError?.Message ?? "";

            var requestTimeout = 0;
            while (!requestFinished && ++requestTimeout <= 10000/100)
                Thread.Sleep(100);

            if (!requestFinished)
                throw new Exception("The email change request timed out without returning a response.");

            if (requestFinished && changeSuccessful)
                return true;

            return false;
        }

        #endregion

        /// <summary>
        /// Retrieves a key and captcha image URI, to be used for registrations where the added security of captchas are required.
        /// </summary>
        /// <param name="gameId"> The ID of the game you wish to register to. </param>
        /// <param name="width"> The width of the captcha image. </param>
        /// <param name="height"> The height of the captcha image. </param>
        public SimpleGetCaptchaOutput SimpleGetCaptcha(string gameId, int width, int height)
        {
            return _channel.Request<SimpleGetCaptchaArgs, SimpleGetCaptchaOutput, PlayerIOError>(415, new SimpleGetCaptchaArgs {
                GameId = gameId,
                Width = width,
                Height = height
            });
        }

        /// <summary>
        /// Retrieves a key and captcha image URI, to be used for registrations where the added security of captchas are required.
        /// </summary>
        /// <param name="gameId"> The ID of the game you wish to register to. </param>
        /// <param name="width"> The width of the captcha image. </param>
        /// <param name="height"> The height of the captcha image. </param>
        /// <param name="successCallback"> A callback called with information about the captcha image, if successful. </param>
        /// <param name="errorCallback"> A callback called instead of <paramref name="successCallback"/> when an error occurs during captcha retrieval. </param>
        public void SimpleGetCaptcha(string gameID, int width, int height, Callback<SimpleGetCaptchaOutput> successCallback, Callback<PlayerIOError> errorCallback = null)
        {
            var simpleGetCaptchaOutput = _channel.Request<SimpleGetCaptchaArgs, SimpleGetCaptchaOutput, PlayerIOError>(415, new SimpleGetCaptchaArgs {
                GameId = gameID,
                Width = width,
                Height = height
            }, errorCallback);

            if (simpleGetCaptchaOutput != null)
                successCallback(simpleGetCaptchaOutput);
        }
    }
}