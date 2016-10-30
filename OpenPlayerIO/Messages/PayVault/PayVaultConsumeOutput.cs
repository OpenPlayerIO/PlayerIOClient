using ProtoBuf;

namespace PlayerIOClient.Messages.PayVault
{
    [ProtoContract]
    internal class PayVaultConsumeOutput
    {
        [ProtoMember(1)]
        public PayVaultItem VaultContents { get; set; }
    }
}