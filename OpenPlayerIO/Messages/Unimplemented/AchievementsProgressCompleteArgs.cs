using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class AchievementsProgressCompleteArgs
	{
		[ProtoMember(1)]
		public string AchievementId { get; set; }
	}
}
