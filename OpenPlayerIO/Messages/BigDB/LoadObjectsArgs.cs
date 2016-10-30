using ProtoBuf;

namespace PlayerIOClient.Messages.BigDB
{
    [ProtoContract]
    internal class LoadObjectsArgs
    {
        [ProtoMember(1)]
        public BigDBObjectId ObjectIds { get; set; }
    }
}