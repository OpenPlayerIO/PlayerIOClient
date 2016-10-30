using ProtoBuf;

namespace PlayerIOClient.Messages.Client
{
    [ProtoContract]
    internal class JoinRoomOutput
    {
        [ProtoMember(1)]
        public string JoinKey { get; set; }

        [ProtoMember(2)]
        public ServerEndpoint[] Endpoints { get; set; }
    }
}