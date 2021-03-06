using System;
using ProtoBuf;

namespace PlayerIOClient.Messages.PayVault
{
    [ProtoContract]
    public class PayVaultHistoryEntry
    {
        [ProtoMember(1)]
        public Int32 Amount { get; set; }

        [ProtoMember(2)]
        public string Type { get; set; }

        [ProtoMember(3)]
        public long Timestamp { get; set; }

        [ProtoMember(4)]
        public string ItemKeys { get; set; }

        [ProtoMember(5)]
        public string Reason { get; set; }

        [ProtoMember(6)]
        public string ProviderTransactionId { get; set; }

        [ProtoMember(7)]
        public string ProviderPrice { get; set; }
    }
}