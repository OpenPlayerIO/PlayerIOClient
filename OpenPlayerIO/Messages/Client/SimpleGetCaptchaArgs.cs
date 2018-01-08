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
		public Int32 Width { get; set; }

		[ProtoMember(3)]
		public Int32 Height { get; set; }
	}
}
