namespace PlayerIOClient.Helpers
{
    /// <summary> The address and port where a server can be reached. </summary>
    public sealed class ServerEndpoint
    {
        /// <summary> The address or hostname of the server. </summary>
        public string Address { get; private set; }

        /// <summary> The port of the server. </summary>
        public int Port { get; private set; }

        public ServerEndpoint(string address, int port)
        {
            Address = address;
            Port = port;
        }
    }
}