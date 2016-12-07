using System.Collections.Generic;
using Newtonsoft.Json;
using PlayerIOClient.Enums;
using ProtoBuf;

namespace PlayerIOClient.Messages.BigDB
{
    [ProtoContract]
    public partial class BigDBObjectValue
    {
        [ProtoMember(1), JsonIgnore]
        public ObjectType Type { get; set; }

        [ProtoMember(2), JsonIgnore]
        public string ValueString { get; set; }

        [ProtoMember(3), JsonIgnore]
        public int ValueInteger { get; set; }

        [ProtoMember(4), JsonIgnore]
        public uint ValueUInteger { get; set; }

        [ProtoMember(5), JsonIgnore]
        public long ValueLong { get; set; }

        [ProtoMember(6), JsonIgnore]
        public bool ValueBoolean { get; set; }

        [ProtoMember(7), JsonIgnore]
        public float ValueFloat { get; set; }

        [ProtoMember(8), JsonIgnore]
        public double ValueDouble { get; set; }

        [ProtoMember(9), JsonIgnore]
        public byte[] ValueByteArray { get; set; }

        [ProtoMember(10), JsonIgnore]
        public long ValueDateTime { get; set; }

        [ProtoMember(11), JsonIgnore]
        public KeyValuePair<int, BigDBObjectValue>[] ValueArray { get; set; }

        [ProtoMember(12), JsonIgnore]
        public KeyValuePair<string, BigDBObjectValue>[] ValueObject { get; set; }
    }
}
