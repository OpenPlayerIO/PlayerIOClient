using System;
using ProtoBuf;

namespace PlayerIOClient.Messages.Client
{
	[ProtoContract]
	internal class SimpleGetCaptchaArgs
	{
		[ProtoMember(1)]
		public string GameId { get; set; }

		[ProtoMember(2)]
		public int Width { get; set; }

		[ProtoMember(3)]
		public int Height { get; set; }
	}
}
