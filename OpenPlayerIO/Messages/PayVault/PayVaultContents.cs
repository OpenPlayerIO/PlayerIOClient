using System;
using ProtoBuf;

namespace PlayerIOClient.Messages.PayVault
{
    [ProtoContract]
    public class PayVaultContents
    {
        [ProtoMember(1)]
        public string Version { get; set; }

        [ProtoMember(2)]
        public Int32 Coins { get; set; }

        [ProtoMember(3)]
        public PayVaultItem[] Items { get; set; }
    }
}