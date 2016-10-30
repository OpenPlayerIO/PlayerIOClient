using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class AchievementsLoadArgs
	{
		[ProtoMember(1)]
		public string UserIds { get; set; }
	}
}
