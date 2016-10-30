using System.Collections.Generic;
using PlayerIOClient.Messages.BigDB;
using ProtoBuf;

namespace PlayerIOClient.Messages.PayVault
{
    [ProtoContract]
    public class PayVaultItem
    {
        [ProtoMember(1)]
        public string Id { get; set; }

        [ProtoMember(2)]
        public string ItemKey { get; set; }

        [ProtoMember(3)]
        public long PurchaseDate { get; set; }

        [ProtoMember(4)]
        public KeyValuePair<string, BigDBObjectValue>[] Properties { get; set; }
    }
}