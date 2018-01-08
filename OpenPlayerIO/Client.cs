using PlayerIOClient.Helpers;

namespace PlayerIOClient
{
    /// <summary> Represents a client you can access the various Player.IO services with. </summary>
    public class Client
    {
        /// <summary>
        /// If not null, rooms will be created on the development server at the address defined by
        /// the server endpoint, instead of using the live Player.IO servers.
        /// </summary>
        public ServerEndpoint DevelopmentServer;

        /// <summary>
        /// The Player.IO Multiplayer Service 
        /// <para> . </para>
        /// </summary>
        public Multiplayer Multiplayer => _multiplayer;

        /// <summary> The ConnectUserId of this client </summary>
        public string ConnectUserId => _connectUserId;

        /// <summary> The Token of this client </summary>
        public string Token => _token;

        /// <summary>
        /// The Player.IO BigDB service 
        /// <para> . </para>
        /// </summary>
        public BigDB BigDB => _bigdb;

        /// <summary>
        /// The Player.IO ErrorLog service 
        /// <para> . </para>
        /// </summary>
        public ErrorLog ErrorLog => _errorlog;

        /// <summary>
        /// The Player.IO PayVault service 
        /// <para> . </para>
        /// </summary>
        public PayVault PayVault => _payvault;

        private readonly string _token;
        private readonly string _connectUserId;
        private readonly BigDB _bigdb;
        private readonly PayVault _payvault;
        private readonly HttpChannel _channel;
        private readonly ErrorLog _errorlog;
        private readonly Multiplayer _multiplayer;

        private string _gameFSRedirectMap;

        internal Client(HttpChannel channel, string token, string connectUserId)
        {
            _connectUserId = connectUserId;
            _channel = channel;
            _token = token;
            _channel.SetToken(_token);

            _errorlog = new ErrorLog(channel);
            _payvault = new PayVault(channel);
            _bigdb = new BigDB(channel);
            _multiplayer = new Multiplayer(channel);
        }

        internal Client(HttpChannel channel, string gameId, string gameFSRedirectMap, string token, string connectUserId, bool showBranding, bool isSocialNetworkUser, object playerInsightState)
        {
            _connectUserId = connectUserId;
            _channel = channel;
            _token = token;
            _channel.SetToken(_token);

            _errorlog = new ErrorLog(channel);
            _payvault = new PayVault(channel);
            _bigdb = new BigDB(channel);
            _multiplayer = new Multiplayer(channel);

            _gameFSRedirectMap = gameFSRedirectMap;
            //_gameId = gameId;
            //_showBranding = showBranding;
            //_isSocialNetworkUser = isSocialNetworkUser;
            //_playerInsightState = playerInsightState;
        }
    }
}