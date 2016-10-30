using ProtoBuf;

namespace PlayerIOClient.Messages.Client
{
    [ProtoContract]
    internal class FacebookOAuthConnectArgs
    {
        [ProtoMember(1)]
        public string GameId { get; set; }

        [ProtoMember(2)]
        public string AccessToken { get; set; }
    }
}