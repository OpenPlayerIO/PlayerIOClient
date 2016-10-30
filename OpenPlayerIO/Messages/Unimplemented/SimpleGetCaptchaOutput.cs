using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class SimpleGetCaptchaOutput
	{
		[ProtoMember(1)]
		public string CaptchaKey { get; set; }

		[ProtoMember(2)]
		public string CaptchaImageUrl { get; set; }
	}
}
