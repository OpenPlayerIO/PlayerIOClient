using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class AchievementsProgressAddOutput
	{
		[ProtoMember(1)]
		public Message Achievement { get; set; }

		[ProtoMember(2)]
		public bool CompletedNow { get; set; }
	}
}
