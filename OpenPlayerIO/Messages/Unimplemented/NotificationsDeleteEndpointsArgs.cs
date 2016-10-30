using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class NotificationsDeleteEndpointsArgs
	{
		[ProtoMember(1)]
		public string LastVersion { get; set; }

		[ProtoMember(2)]
		public Message Endpoints { get; set; }
	}
}
