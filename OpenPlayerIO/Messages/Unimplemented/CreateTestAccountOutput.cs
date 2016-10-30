using System;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class CreateTestAccountOutput
	{
		[ProtoMember(1)]
		public Int32 AccountId { get; set; }

		[ProtoMember(2)]
		public string TestGameId { get; set; }

		[ProtoMember(3)]
		public string CaptchaTestGameId { get; set; }

		[ProtoMember(4)]
		public string AuthSharedSecret { get; set; }

		[ProtoMember(5)]
		public string PartnerId1 { get; set; }

		[ProtoMember(6)]
		public string PartnerId2 { get; set; }

		[ProtoMember(7)]
		public string AuthKeyForBob { get; set; }

		[ProtoMember(8)]
		public string AuthKeyForJohn { get; set; }

		[ProtoMember(9)]
		public string AuthKeyForJulie { get; set; }

		[ProtoMember(10)]
		public string YahooUserTokenForOliver { get; set; }

		[ProtoMember(11)]
		public string YahooUserTokenForJonas { get; set; }

		[ProtoMember(12)]
		public string YahooUserTokenForHenrik { get; set; }
	}
}
