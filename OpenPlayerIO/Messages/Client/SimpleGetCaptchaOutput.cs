using ProtoBuf;

namespace PlayerIOClient.Messages.Client
{
	[ProtoContract]
	public class SimpleGetCaptchaOutput
	{
		[ProtoMember(1)]
		public string CaptchaKey { get; set; }

		[ProtoMember(2)]
		public string CaptchaImageUrl { get; set; }
	}
}
