using System;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class OneScoreSetArgs
	{
		[ProtoMember(1)]
		public Int32 Score { get; set; }
	}
}
