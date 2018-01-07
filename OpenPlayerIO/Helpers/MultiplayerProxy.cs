using System.Net;

namespace PlayerIOClient
{
    public class MultiplayerProxy
    {
        public string Address { get; set; }
        public int Port { get; set; }

        /// <summary> Optional setting. </summary>
        public string Username { get; set; }

        /// <summary> Optional setting. </summary>
        public string Password { get; set; }

        //public bool StrictProxyMode { get; set; } = false;

        /// <summary>
        /// Default: <see cref="ProxyType.Socks5"/>
        /// </summary>
        public ProxyType Type { get; set; }

        public MultiplayerProxy(IPEndPoint proxyEndPoint, ProxyType proxyType = ProxyType.Socks5, string username = null, string password = null)
        {
            this.Address = proxyEndPoint.Address.ToString();
            this.Port = proxyEndPoint.Port;

            this.Type = proxyType;

            this.Username = username;
            this.Password = password;
        }
    }
}