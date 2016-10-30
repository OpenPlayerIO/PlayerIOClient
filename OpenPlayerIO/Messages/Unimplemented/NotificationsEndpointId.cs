using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class NotificationsEndpointId
	{
		[ProtoMember(1)]
		public string Type { get; set; }

		[ProtoMember(2)]
		public string Identifier { get; set; }
	}
}
