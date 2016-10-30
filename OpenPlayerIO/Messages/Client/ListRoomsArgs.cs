using ProtoBuf;

namespace PlayerIOClient.Messages.Client
{
    [ProtoContract]
    internal class ListRoomsArgs
    {
        [ProtoMember(1)]
        public string RoomType { get; set; }

        [ProtoMember(2)]
        public KeyValuePair[] SearchCriteria { get; set; }

        [ProtoMember(3)]
        public int ResultLimit { get; set; }

        [ProtoMember(4)]
        public int ResultOffset { get; set; }

        [ProtoMember(5)]
        public bool OnlyDevRooms { get; set; }
    }
}