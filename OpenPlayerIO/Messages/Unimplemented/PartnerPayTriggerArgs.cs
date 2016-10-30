using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class PartnerPayTriggerArgs
	{
		[ProtoMember(1)]
		public string Key { get; set; }

		[ProtoMember(2)]
		public uint Count { get; set; }
	}
}
