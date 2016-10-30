using ProtoBuf;

namespace PlayerIOClient.Messages.Client
{
    [ProtoContract]
    internal class SteamConnectArgs
    {
        [ProtoMember(1)]
        public string GameId { get; set; }

        [ProtoMember(2)]
        public string SteamAppId { get; set; }

        [ProtoMember(3)]
        public string SteamSessionTicket { get; set; }
    }
}