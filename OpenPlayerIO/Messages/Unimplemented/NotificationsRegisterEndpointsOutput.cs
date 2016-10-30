using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class NotificationsRegisterEndpointsOutput
	{
		[ProtoMember(1)]
		public string Version { get; set; }

		[ProtoMember(2)]
		public Message Endpoints { get; set; }
	}
}
