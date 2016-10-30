using ProtoBuf;

namespace PlayerIOClient.Messages.PayVault
{
    [ProtoContract]
    internal class PayVaultGiveArgs
    {
        [ProtoMember(1)]
        public PayVaultBuyItemInfo[] Items { get; set; }

        [ProtoMember(2)]
        public string TargetUserId { get; set; }
    }
}