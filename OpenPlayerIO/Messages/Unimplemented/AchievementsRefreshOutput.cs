using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class AchievementsRefreshOutput
	{
		[ProtoMember(1)]
		public string Version { get; set; }

		[ProtoMember(2)]
		public Message Achievements { get; set; }
	}
}
