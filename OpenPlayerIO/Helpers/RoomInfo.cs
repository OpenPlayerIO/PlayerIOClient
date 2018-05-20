using System.Collections.Generic;
using ProtoBuf;

namespace PlayerIOClient.Helpers
{
    [ProtoContract]
    public class RoomInfo
    {
        [ProtoMember(1)]
        public string Id { get; }

        [ProtoMember(2)]
        public string RoomType { get; }

        [ProtoMember(3)]
        public int OnlineUsers { get; }

        /// <summary> The current room data for the room. </summary>
        [ProtoMember(4)]
        public Dictionary<string, string> RoomData { get; }

        internal RoomInfo(string id, string roomType, int onlineUsers, Dictionary<string, string> roomData)
        {
            this.RoomData = roomData ?? new Dictionary<string, string>();
            this.OnlineUsers = onlineUsers;
            this.RoomType = roomType;
            this.Id = id;
        }
    }
}