using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class PlayerInsightTrackInvitedByArgs
	{
		[ProtoMember(1)]
		public string InvitingUserId { get; set; }

		[ProtoMember(2)]
		public string InvitationChannel { get; set; }
	}
}
