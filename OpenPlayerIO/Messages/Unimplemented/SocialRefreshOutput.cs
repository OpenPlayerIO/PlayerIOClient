using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class SocialRefreshOutput
	{
		[ProtoMember(1)]
		public Message MyProfile { get; set; }

		[ProtoMember(2)]
		public Message Friends { get; set; }

		[ProtoMember(3)]
		public string Blocked { get; set; }
	}
}
