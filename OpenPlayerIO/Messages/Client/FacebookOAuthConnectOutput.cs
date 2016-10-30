using ProtoBuf;

namespace PlayerIOClient.Messages.Client
{
    [ProtoContract]
    internal class FacebookOAuthConnectOutput
    {
        [ProtoMember(1)]
        public string Token { get; set; }

        [ProtoMember(2)]
        public string UserId { get; set; }

        [ProtoMember(3)]
        public bool ShowBranding { get; set; }

        [ProtoMember(4)]
        public string GameFSRedirectMap { get; set; }

        [ProtoMember(5)]
        public string FacebookUserId { get; set; }

        [ProtoMember(6)]
        public string PartnerId { get; set; }

        /*
        [ProtoMember(7)]
        public string PlayerInsightState { get; set; }
        */
    }
}