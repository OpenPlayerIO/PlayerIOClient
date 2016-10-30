using System;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class AchievementsProgressMaxArgs
	{
		[ProtoMember(1)]
		public string AchievementId { get; set; }

		[ProtoMember(2)]
		public Int32 Progress { get; set; }
	}
}
