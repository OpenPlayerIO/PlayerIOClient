using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class AchievementsRefreshArgs
	{
		[ProtoMember(1)]
		public string LastVersion { get; set; }
	}
}
