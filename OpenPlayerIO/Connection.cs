using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using PlayerIOClient.Error;
using PlayerIOClient.Helpers;

namespace PlayerIOClient
{
    /// <summary> Used to add a message handler to the OnMessage event of an instance of Connection. </summary>
    public delegate void MessageReceivedEventHandler(object sender, Message e);

    /// <summary>
    /// Used to add a disconnect handler to the OnDisconnect event of an instance of Connection.
    /// </summary>
    /// <param name="message"> The reason of disconnecting explained by words. </param>
    public delegate void DisconnectEventHandler(object sender, string message);

    /// <summary> A connection to a running Player.IO multiplayer room. </summary>
    public class Connection
    {
        /// <summary> Determines if the connection is currently connected to a remote host or not. </summary>
        public bool Connected { get; private set; }

        /// <summary> Event fired everytime a message is received. </summary>
        public event MessageReceivedEventHandler OnMessage;

        /// <summary> Event fired when the connection gets disconnected. </summary>
        public event DisconnectEventHandler OnDisconnect;

        private readonly ServerEndpoint _endpoint;
        private readonly ProxySocket _socket;
        private readonly Stream _stream;
        private readonly BinaryDeserializer _deserializer;
        private readonly BinarySerializer _serializer;

        private readonly byte[] _buffer = new byte[65536];
        private readonly string _joinKey;

        public void Send(string type, params object[] parameters) => _socket.Send(_serializer.Serialize(new Message(type, parameters)));

        public void Disconnect() => Terminate(new Exception("Connection forcefully closed by client."));

        public Connection(ServerEndpoint endpoint, string joinKey, MultiplayerProxy proxy = null)
        {
            _endpoint = endpoint;
            _joinKey = joinKey;
            
            _socket = new ProxySocket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            if (proxy != null) {
                _socket.ProxyEndPoint = new IPEndPoint(IPAddress.Parse(proxy.Address), proxy.Port);
                _socket.ProxyType = proxy.Type;

                if (proxy.Username != null)
                    _socket.ProxyUser = proxy.Username;

                if (proxy.Password != null)
                    _socket.ProxyPass = proxy.Password;
            }

            // TODO: check for functional non-transparent proxy connection per MultiplayerProxy.StrictProxyMode

            _socket.Connect(endpoint.Address, endpoint.Port);

            _stream = new NetworkStream(_socket);

            _serializer = new BinarySerializer();
            _deserializer = new BinaryDeserializer();

            _socket.Send(new[] { (byte)Enums.ProtocolType.Binary });
            _socket.Send(_serializer.Serialize(new Message("join", joinKey)));

            OnMessage += (sender, message) => {
                if (message.Type == "playerio.joinresult") {
                    if (!message.GetBoolean(0)) {
                        throw new PlayerIOError((ErrorCode)message.GetInteger(1), message.GetString(2));
                    }

                    this.Connected = true;
                }
            };

            _deserializer.OnDeserializedMessage += (message) => OnMessage.Invoke(this, message);
            _stream.BeginRead(_buffer, 0, _buffer.Length, new AsyncCallback(this.ReceiveCallback), null);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            var length = _stream.EndRead(ar);
            var received = _buffer.Take(length).ToArray();

            if (length == 0) {
                Terminate(new Exception("Connection unexpectedly terminated. (receivedBytes == 0)"));
            }

            _deserializer.AddBytes(received);
            _stream.BeginRead(_buffer, 0, _buffer.Length, new AsyncCallback(this.ReceiveCallback), null);
        }

        private void Terminate(Exception exception)
        {
            _stream.Close();
            _socket.Disconnect(false);
            _socket.Close();

            this.Connected = false;

            if (OnDisconnect != null) {
                OnDisconnect?.Invoke(this, exception.Message);
            } else {
                throw new PlayerIOError(ErrorCode.InternalError, string.Concat(new object[] { "Connection from ", _endpoint.Address, " was closed. ", ", message: ", exception.Message }));
            }
        }
    }
}