using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class SocialLoadProfilesArgs
	{
		[ProtoMember(1)]
		public string UserIds { get; set; }
	}
}
