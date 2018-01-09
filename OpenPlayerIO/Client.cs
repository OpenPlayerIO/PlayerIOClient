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
        public Multiplayer Multiplayer { get; }

        /// <summary> The ConnectUserId of this client </summary>
        public string ConnectUserId { get; }

        /// <summary> The Token of this client </summary>
        public string Token { get; }

        /// <summary>
        /// The Player.IO BigDB service 
        /// <para> . </para>
        /// </summary>
        public BigDB BigDB { get; }

        /// <summary>
        /// The Player.IO ErrorLog service 
        /// <para> . </para>
        /// </summary>
        public ErrorLog ErrorLog { get; }

        /// <summary>
        /// The Player.IO PayVault service 
        /// <para> . </para>
        /// </summary>
        public PayVault PayVault { get; }

        private readonly HttpChannel _channel;

        private string _gameFSRedirectMap;

        internal Client(HttpChannel channel, string token, string connectUserId)
        {
            this.ConnectUserId = connectUserId;
            this._channel = channel;
            this.Token = token;
            this._channel.SetToken(this.Token);

            this.ErrorLog = new ErrorLog(channel);
            this.PayVault = new PayVault(channel);
            this.BigDB = new BigDB(channel);
            this.Multiplayer = new Multiplayer(channel);
        }

        internal Client(HttpChannel channel, string gameId, string gameFSRedirectMap, string token, string connectUserId, bool showBranding, bool isSocialNetworkUser, object playerInsightState)
        {
            this.ConnectUserId = connectUserId;
            this._channel = channel;
            this.Token = token;
            this._channel.SetToken(this.Token);

            this.ErrorLog = new ErrorLog(channel);
            this.PayVault = new PayVault(channel);
            this.BigDB = new BigDB(channel);
            this.Multiplayer = new Multiplayer(channel);

            this._gameFSRedirectMap = gameFSRedirectMap;
        }
    }
}