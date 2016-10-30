using ProtoBuf;

namespace PlayerIOClient.Messages.PayVault
{
    [ProtoContract]
    internal class PayVaultReadHistoryArgs
    {
        [ProtoMember(1)]
        public uint Page { get; set; }

        [ProtoMember(2)]
        public uint PageSize { get; set; }

        [ProtoMember(3)]
        public string TargetUserId { get; set; }
    }
}