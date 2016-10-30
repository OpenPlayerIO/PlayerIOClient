using System;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class PlayerInsightTrackExternalPaymentArgs
	{
		[ProtoMember(1)]
		public string Currency { get; set; }

		[ProtoMember(2)]
		public Int32 Amount { get; set; }
	}
}
