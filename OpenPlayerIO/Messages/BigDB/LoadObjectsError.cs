using ProtoBuf;

namespace PlayerIOClient.Messages.BigDB
{
    [ProtoContract]
    internal class LoadObjectsError
    {
        [ProtoMember(1)]
        public int ErrorCode { get; set; }

        [ProtoMember(2)]
        public string Message { get; set; }
    }
}