namespace PlayerIOClient.Error
{
    /// <summary> The code of the error that happened. </summary>
    public enum ErrorCode : byte
    {
        /// <summary> The method requested is not supported. </summary>
        UnsupportedMethod = 0,

        /// <summary> A general error has just occurred. </summary>
        GeneralError = 1,

        /// <summary>
        /// An unexpected error has just occurred inside the Player.IO WebService. Please try again later.
        /// </summary>
        InternalError = 2,

        /// <summary> The content you've just wanted to access is not available for you. </summary>
        AccessDenied = 3,

        /// <summary> The message is malformatted. </summary>
        InvalidMessageFormat = 4,

        /// <summary> A value is missing. </summary>
        MissingValue = 5,

        /// <summary> A game is required to do this action. </summary>
        GameRequired = 6,

        /// <summary> An error has just occurred while communicating with an external service. </summary>
        ExternalError = 7,

        /// <summary> The given argument value is outside the range of allowed values. </summary>
        ArgumentOutOfRange = 8,

        /// <summary> The given type is invalid. </summary>
        InvalidType = 80,

        /// <summary> The index is out of bounds from the range of acceptable values. </summary>
        IndexOutOfBounds = 81,

        /// <summary> The game has been disabled, most likely because of a missing payment. </summary>
        GameDisabled = 9,

        /// <summary> The game requested is not known by the server. </summary>
        UnknownGame = 10,

        /// <summary> The connection requested is not known by the server. </summary>
        UnknownConnection = 11,

        /// <summary> The auth given is invalid or malformatted. </summary>
        InvalidAuth = 12,

        /// <summary>
        /// There is no server in any of the selected server clusters for the game that is eligible
        /// to start a new room (they're all at full capacity or there are no servers in any of the
        /// clusters). Either change the selected clusters for your game by using the admin panel,
        /// try again later, or start some more servers for one of your clusters.
        /// </summary>
        NoAvailableServers = 13,

        /// <summary> The room data for the room was over the allowed size limit. </summary>
        RoomDataTooLarge = 14,

        /// <summary>
        /// You're unable to create this room because there is already a room with the specified ID.
        /// </summary>
        RoomAlreadyExists = 15,

        /// <summary>
        /// The game you're connected to doesn't have a room type with the specified name.
        /// </summary>
        UnknownServerType = 16,

        /// <summary> There's no room running with that ID. </summary>
        UnknownRoom = 17,

        /// <summary> You can't join a room when the RoomID is null or the empty string. </summary>
        MissingRoomId = 18,

        /// <summary> The room already has the maxmium amount of users in it. </summary>
        RoomIsFull = 19,

        /// <summary>
        /// The key you specified is not set as searchable. You can change the searchable keys for
        /// this server type by using the admin panel.
        /// </summary>
        NotASearchColumn = 20,

        /// <summary>
        /// The QuickConnect method (Simple, Facebook, Kongregate) is not enabled for the game. You
        /// can enable the various methods for the game by using the admin panel.
        /// </summary>
        QuickConnectMethodNotEnabled = 21,

        /// <summary> The user is unknown. </summary>
        UnknownUser = 22,

        /// <summary> The supplied password is not correct. </summary>
        InvalidPassword = 23,

        /// <summary> The supplied registration data is not correct. </summary>
        InvalidRegistrationData = 24,

        /// <summary>
        /// The key given for the BigDB object is not a valid BigDB key. BigDB keys must be between 1
        /// and 50 characters in length (without spaces).
        /// </summary>
        InvalidBigDBKey = 25,

        /// <summary> The object exceeds the maximum allowed size for BigDB objects. </summary>
        BigDBObjectTooLarge = 26,

        /// <summary> Couldn't find the BigDB object. </summary>
        BigDBObjectDoesNotExist = 27,

        /// <summary> The specified table doesn't exists. </summary>
        UnknownTable = 28,

        /// <summary> The specified index doesn't exists. </summary>
        UnknownIndex = 29,

        /// <summary> The value given for the index doesn't match the expected type. </summary>
        InvalidIndexValue = 30,

        /// <summary>
        /// The operation has just been aborted because the user attempting the operation was not the
        /// original creator of the object accessed.
        /// </summary>
        NotObjectCreator = 31,

        /// <summary> The key is already in use by an other BigDB object. </summary>
        KeyAlreadyUsed = 32,

        /// <summary> BigDB object couldn't be saved using optimistic locks as it's out of date. </summary>
        StaleVersion = 33,

        /// <summary> Can't create circular references inside BigDB objects. </summary>
        CircularReference = 34,

        /// <summary> The server couldn't complete the heartbeat. </summary>
        HeartbeatFailed = 40,

        /// <summary> The game code is invalid. </summary>
        InvalidGameCode = 41,

        /// <summary>
        /// Can't access coins or items before the vault has been loaded. Please refresh the vault first.
        /// </summary>
        VaultNotLoaded = 50,

        /// <summary> There is no PayVault provider with the specified ID. </summary>
        UnknownPayVaultProvider = 51,

        /// <summary> The specified PayVault provider doesn't support direct purchases. </summary>
        DirectPurchaseNotSupportedByProvider = 52,

        /// <summary> The specified PayVault provider doesn't support buying coins. </summary>
        BuyingCoinsNotSupportedByProvider = 54,

        /// <summary>
        /// The user doesn't have enough coins in the PayVault to complete the purchase or debit.
        /// </summary>
        NotEnoughCoins = 55,

        /// <summary> The item does not exists in the vault. </summary>
        ItemNotInVault = 56,

        /// <summary> The chosen provider rejected one or more of the purchase arguments. </summary>
        InvalidPurchaseArguments = 57,

        /// <summary> The chosen provider is not configured correctly in the admin panel. </summary>
        InvalidPayVaultProviderSetup = 58,

        /// <summary> Unable to locate the custom PartnerPay action with the given key. </summary>
        UnknownPartnerPayAction = 70
    }
}