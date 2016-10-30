using PlayerIOClient.Error;
using ProtoBuf;

namespace PlayerIOClient.Messages.Error
{
    [ProtoContract]
    internal class Error
    {
        [ProtoMember(1)]
        public ErrorCode ErrorCode { get; set; }

        [ProtoMember(2)]
        public string Message { get; set; }

        public Error()
        {
            ErrorCode = ErrorCode.InternalError;
        }
    }
}