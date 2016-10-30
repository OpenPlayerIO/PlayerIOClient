using ProtoBuf;

namespace PlayerIOClient.Messages.PayVault
{
    [ProtoContract]
    internal class PayVaultGiveOutput
    {
        [ProtoMember(1)]
        public PayVaultContents VaultContents { get; set; }
    }
}