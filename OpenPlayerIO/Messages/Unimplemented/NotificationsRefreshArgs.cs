using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class NotificationsRefreshArgs
	{
		[ProtoMember(1)]
		public string LastVersion { get; set; }
	}
}
