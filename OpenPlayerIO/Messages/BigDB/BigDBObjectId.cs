using ProtoBuf;

namespace PlayerIOClient.Messages.BigDB
{
    [ProtoContract]
    internal class BigDBObjectId
    {
        [ProtoMember(1)]
        public string Table { get; set; }

        [ProtoMember(2)]
        public string[] Keys { get; set; }
    }
}