using System.Collections.Generic;
using ProtoBuf;

namespace PlayerIOClient.Messages.PayVault
{
    [ProtoContract]
    public class PayVaultPaymentInfoOutput
    {
        [ProtoMember(1)]
        public KeyValuePair<string, string>[] ProviderArguments { get; set; }
    }
}