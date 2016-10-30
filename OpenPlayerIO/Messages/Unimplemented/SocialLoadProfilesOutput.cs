using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class SocialLoadProfilesOutput
	{
		[ProtoMember(1)]
		public Message Profiles { get; set; }
	}
}
