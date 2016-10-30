using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class NotificationsEndpoint
	{
		[ProtoMember(1)]
		public string Type { get; set; }

		[ProtoMember(2)]
		public string Identifier { get; set; }

		[ProtoMember(3)]
		public Message Configuration { get; set; }

		[ProtoMember(4)]
		public bool Enabled { get; set; }
	}
}
