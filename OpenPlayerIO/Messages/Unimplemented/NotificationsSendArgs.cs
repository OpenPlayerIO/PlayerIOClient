using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class NotificationsSendArgs
	{
		[ProtoMember(1)]
		public Message Notifications { get; set; }
	}
}
