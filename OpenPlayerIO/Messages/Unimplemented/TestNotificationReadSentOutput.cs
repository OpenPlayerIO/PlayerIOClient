using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class TestNotificationReadSentOutput
	{
		[ProtoMember(1)]
		public Message TestNotifications { get; set; }
	}
}
