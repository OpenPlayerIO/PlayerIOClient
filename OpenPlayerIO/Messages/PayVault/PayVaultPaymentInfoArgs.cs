using System.Collections.Generic;
using ProtoBuf;

namespace PlayerIOClient.Messages.PayVault
{
    [ProtoContract]
    public class PayVaultPaymentInfoArgs
    {
        [ProtoMember(1)]
        public string Provider { get; set; }

        [ProtoMember(2)]
        public KeyValuePair<string, string>[] PurchaseArguments { get; set; }

        [ProtoMember(3)]
        public PayVaultBuyItemInfo[] Items { get; set; }
    }
}