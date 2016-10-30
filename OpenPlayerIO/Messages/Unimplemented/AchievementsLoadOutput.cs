using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class AchievementsLoadOutput
	{
		[ProtoMember(1)]
		public Message UserAchievements { get; set; }
	}
}
