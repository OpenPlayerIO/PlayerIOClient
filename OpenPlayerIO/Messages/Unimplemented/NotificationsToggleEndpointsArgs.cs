using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class NotificationsToggleEndpointsArgs
	{
		[ProtoMember(1)]
		public string LastVersion { get; set; }

		[ProtoMember(2)]
		public Message Endpoints { get; set; }

		[ProtoMember(3)]
		public bool Enabled { get; set; }
	}
}
