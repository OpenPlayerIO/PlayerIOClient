using ProtoBuf;

namespace PlayerIOClient.Messages.PayVault
{
    [ProtoContract]
    internal class PayVaultDebitOutput
    {
        [ProtoMember(1)]
        public PayVaultContents VaultContents { get; set; }
    }
}