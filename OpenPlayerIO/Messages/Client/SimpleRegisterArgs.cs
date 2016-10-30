using ProtoBuf;

namespace PlayerIOClient.Messages.Client
{
    [ProtoContract]
    internal class SimpleRegisterArgs
    {
        [ProtoMember(1)]
        public string GameId { get; set; }

        [ProtoMember(2)]
        public string Username { get; set; }

        [ProtoMember(3)]
        public string Password { get; set; }

        [ProtoMember(4)]
        public string Email { get; set; }

        [ProtoMember(5)]
        public string CaptchaKey { get; set; }

        [ProtoMember(6)]
        public string CaptchaValue { get; set; }

        [ProtoMember(7)]
        public KeyValuePair[] ExtraData { get; set; }
    }
}