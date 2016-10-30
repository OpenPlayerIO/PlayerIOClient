using ProtoBuf;

namespace PlayerIOClient.Messages.Client
{
    [ProtoContract]
    internal class JoinRoomArgs
    {
        [ProtoMember(1)]
        public string RoomId { get; set; }

        [ProtoMember(2)]
        public KeyValuePair[] JoinData { get; set; }

        [ProtoMember(3)]
        public bool IsDevRoom { get; set; }
    }
}