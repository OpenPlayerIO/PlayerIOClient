using System.Collections.Generic;
using PlayerIOClient.Messages.BigDB;
using ProtoBuf;

namespace PlayerIOClient.Helpers
{
    [ProtoContract]
    public partial class DatabaseObject
    {
        [ProtoMember(1)]
        public string Key { get; set; }

        [ProtoMember(2)]
        public string Version { get; set; }

        [ProtoMember(3)]
        public List<KeyValuePair<string, BigDBObjectValue>> Properties { get; set; }

        [ProtoMember(4)]
        public uint Creator { get; set; }
    }
}