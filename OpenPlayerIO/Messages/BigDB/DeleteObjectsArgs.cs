using ProtoBuf;

namespace PlayerIOClient.Messages.BigDB
{
    [ProtoContract]
    internal class DeleteObjectsArgs
    {
        [ProtoMember(1)]
        public string[] ObjectIds { get; set; }
    }
}