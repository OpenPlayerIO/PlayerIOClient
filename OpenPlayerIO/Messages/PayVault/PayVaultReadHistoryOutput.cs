using ProtoBuf;

namespace PlayerIOClient.Messages.PayVault
{
    [ProtoContract]
    public class PayVaultReadHistoryOutput
    {
        [ProtoMember(1)]
        public PayVaultHistoryEntry[] Entries { get; set; }
    }
}