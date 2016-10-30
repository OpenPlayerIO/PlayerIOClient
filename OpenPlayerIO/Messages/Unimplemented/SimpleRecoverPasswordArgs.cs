using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
    [ProtoContract]
    internal class SimpleRecoverPasswordArgs
    {
        [ProtoMember(1)]
        public string GameId { get; set; }

        [ProtoMember(2)]
        public string UsernameOrEmail { get; set; }
    }
}