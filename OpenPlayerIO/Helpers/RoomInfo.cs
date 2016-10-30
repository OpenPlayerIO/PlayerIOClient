using System.Collections.Generic;
using ProtoBuf;

namespace PlayerIOClient.Helpers
{
    [ProtoContract]
    public class RoomInfo
    {
        [ProtoMember(1)]
        public string Id { get; private set; }

        [ProtoMember(2)]
        public string RoomType { get; private set; }

        [ProtoMember(3)]
        public int OnlineUsers { get; private set; }

        /// <summary> The current room data for the room. </summary>
        [ProtoMember(4)]
        public Dictionary<string, string> RoomData { get; private set; }

        internal RoomInfo(string id, string roomType, int onlineUsers, Dictionary<string, string> roomData)
        {
            RoomData = roomData ?? new Dictionary<string, string>();
            OnlineUsers = onlineUsers;
            RoomType = roomType;
            Id = id;
        }
    }
}