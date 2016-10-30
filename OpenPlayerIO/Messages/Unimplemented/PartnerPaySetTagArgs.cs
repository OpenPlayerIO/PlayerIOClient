using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class PartnerPaySetTagArgs
	{
		[ProtoMember(1)]
		public string PartnerId { get; set; }
	}
}
