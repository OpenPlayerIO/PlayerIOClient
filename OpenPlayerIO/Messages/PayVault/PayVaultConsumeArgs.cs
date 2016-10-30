using ProtoBuf;

namespace PlayerIOClient.Messages.PayVault
{
    [ProtoContract]
    internal class PayVaultConsumeArgs
    {
        [ProtoMember(1)]
        public string[] Ids { get; set; }

        [ProtoMember(2)]
        public string TargetUserId { get; set; }
    }
}