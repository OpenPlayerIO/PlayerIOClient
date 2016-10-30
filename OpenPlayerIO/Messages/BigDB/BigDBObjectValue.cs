using System;
using System.Collections.Generic;
using System.Linq;
using PlayerIOClient.Enums;
using ProtoBuf;

namespace PlayerIOClient.Messages.BigDB
{
    [ProtoContract]
    public class BigDBObjectValue
    {
        public object GetRealValue()
        {
            switch (Type) {
                case ObjectType.String:
                    return ValueString;

                case ObjectType.Int:
                    return ValueInteger;

                case ObjectType.UInt:
                    return ValueUInteger;

                case ObjectType.Long:
                    return ValueLong;

                case ObjectType.Bool:
                    return ValueBoolean;

                case ObjectType.Float:
                    return ValueFloat;

                case ObjectType.Double:
                    return ValueDouble;

                case ObjectType.ByteArray:
                    return ValueByteArray;

                case ObjectType.DateTime:
                    return UnixTimestampToDateTime(ValueDateTime);

                case ObjectType.Array:
                    return ValueArray.Select(x => new KeyValuePair<int, object>(x.Key, x.Value.GetRealValue())).ToList();

                case ObjectType.Obj:
                    return ValueObject.Select(x => new KeyValuePair<string, object>(x.Key, x.Value.GetRealValue())).ToList();

                default:
                    return null;
            }
        }

        [ProtoMember(1)]
        public ObjectType Type { get; set; }

        [ProtoMember(2)]
        public string ValueString { get; set; }

        [ProtoMember(3)]
        public int ValueInteger { get; set; }

        [ProtoMember(4)]
        public uint ValueUInteger { get; set; }

        [ProtoMember(5)]
        public long ValueLong { get; set; }

        [ProtoMember(6)]
        public bool ValueBoolean { get; set; }

        [ProtoMember(7)]
        public float ValueFloat { get; set; }

        [ProtoMember(8)]
        public double ValueDouble { get; set; }

        [ProtoMember(9)]
        public byte[] ValueByteArray { get; set; }

        [ProtoMember(10)]
        public long ValueDateTime { get; set; }

        [ProtoMember(11)]
        public KeyValuePair<int, BigDBObjectValue>[] ValueArray { get; set; }

        [ProtoMember(12)]
        public KeyValuePair<string, BigDBObjectValue>[] ValueObject { get; set; }

        public static DateTime UnixTimestampToDateTime(long unixTimeStamp)
        {
            return new DateTime(1970, 1, 1).AddMilliseconds(unixTimeStamp);
        }
    }
}