using ProtoBuf;

namespace PlayerIOClient.Messages.BigDB
{
    [ProtoContract]
    internal class LoadIndexRangeArgs
    {
        [ProtoMember(1)]
        public string Table { get; set; }

        [ProtoMember(2)]
        public string Index { get; set; }

        [ProtoMember(3)]
        public BigDBObjectValue[] StartIndexValue { get; set; }

        [ProtoMember(4)]
        public BigDBObjectValue[] StopIndexValue { get; set; }

        [ProtoMember(5)]
        public int Limit { get; set; }
    }
}