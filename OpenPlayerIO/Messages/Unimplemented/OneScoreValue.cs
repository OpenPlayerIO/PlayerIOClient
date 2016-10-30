using System;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class OneScoreValue
	{
		[ProtoMember(1)]
		public string UserId { get; set; }

		[ProtoMember(2)]
		public Int32 Score { get; set; }

		[ProtoMember(3)]
		public float Percentile { get; set; }

		[ProtoMember(4)]
		public Int32 TopRank { get; set; }
	}
}
