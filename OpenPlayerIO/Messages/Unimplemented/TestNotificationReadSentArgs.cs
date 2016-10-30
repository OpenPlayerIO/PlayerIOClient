using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class TestNotificationReadSentArgs
	{
		[ProtoMember(1)]
		public string ConnectedUserId { get; set; }

		[ProtoMember(2)]
		public string GameId { get; set; }
	}
}
