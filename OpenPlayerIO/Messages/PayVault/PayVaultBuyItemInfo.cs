using System.Collections.Generic;
using PlayerIOClient.Messages.BigDB;
using ProtoBuf;

namespace PlayerIOClient.Messages.PayVault
{
    [ProtoContract, ProtoInclude(128, typeof(BuyItemInfo))]
    public class PayVaultBuyItemInfo
    {
        [ProtoMember(1)]
        public string ItemKey { get; set; }

        [ProtoMember(2)]
        public KeyValuePair<string, BigDBObjectValue>[] Payload { get; set; }
    }
}