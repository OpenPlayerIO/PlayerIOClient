using ProtoBuf;

namespace PlayerIOClient.Messages.PayVault
{
    [ProtoContract]
    internal class PayVaultBuyOutput
    {
        [ProtoMember(1)]
        public PayVaultBuyItemInfo VaultContents { get; set; }
    }
}