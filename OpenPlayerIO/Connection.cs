using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
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
        /// <summary>
        /// Determines if the connection is currently connected to a remote host or not.
        /// </summary>
        public bool Connected { get; private set; }

        /// <summary> Event fired everytime a message is received. </summary>
        public event MessageReceivedEventHandler OnMessage;

        /// <summary> Event fired when the connection gets disconnected. </summary>
        public event DisconnectEventHandler OnDisconnect;

        private readonly ServerEndpoint _endpoint;
        private readonly Socket _socket;
        private readonly Stream _stream;
        private readonly Thread _receiveThread;
        private readonly byte[] _receiveBuffer = new byte[65536];
        private readonly string _joinKey;

        public void Send(string type, params object[] parameters) => _socket.Send(new BinarySerializer().Serialize(new Message(type, parameters)));

        public Connection(ServerEndpoint endpoint, string joinKey)
        {
            _endpoint = endpoint;
            _joinKey = joinKey;

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(endpoint.Address, endpoint.Port);

            _stream = new NetworkStream(_socket);

            _socket.Send(new[] { (byte)Enums.ProtocolType.Binary });
            _socket.Send(new Message("join", joinKey).Serialize());

            OnMessage = (sender, message) => {
                if (message.Type == "playerio.joinresult") {
                    if (!message.GetBoolean(0)) {
                        throw new PlayerIOError((ErrorCode)message.GetInteger(1), message.GetString(2));
                    }

                    this.Connected = true;
                }
            };

            _receiveThread = new Thread(() => {
                while (true) {
                    int receivedBytes = _stream.Read(_receiveBuffer, 0, _receiveBuffer.Length);

                    if (receivedBytes == 0) {
                        this.Terminate(ErrorCode.GeneralError, new Exception("Connection unexpectedly terminated. (receivedBytes == 0)"));
                    }

                    var messages = new BinaryDeserializer().Deserialize(_receiveBuffer.ToList().Take(receivedBytes).ToArray());
                    foreach (var message in messages) {
                        OnMessage?.Invoke(this, message);
                    }
                }
            });

            _receiveThread.IsBackground = true;
            _receiveThread.Start();
        }

        public void Disconnect()
        {
            this.Terminate(ErrorCode.GeneralError, new Exception("Connection forcefully closed by client."));
        }

        private void Terminate(ErrorCode code, Exception exception)
        {
            _stream.Close();
            _socket.Disconnect(false);
            _socket.Close();
            _receiveThread.Abort(exception);

            this.Connected = false;

            if (OnDisconnect != null)
                OnDisconnect?.Invoke(this, exception.Message);
            else
                throw new PlayerIOError(ErrorCode.InternalError, string.Concat(new object[] { "Connection from ", _endpoint.Address, " was closed. ", ", message: ", exception.Message }));
        }
    }
}