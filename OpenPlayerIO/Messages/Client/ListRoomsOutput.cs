using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Client
{
    [ProtoContract]
    internal class ListRoomsOutput
    {
        [ProtoMember(1)]
        public RoomInfo[] RoomInfo { get; set; }
    }
}