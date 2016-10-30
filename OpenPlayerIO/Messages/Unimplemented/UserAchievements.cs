using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class UserAchievements
	{
		[ProtoMember(1)]
		public string UserId { get; set; }

		[ProtoMember(2)]
		public Message Achievements { get; set; }
	}
}
