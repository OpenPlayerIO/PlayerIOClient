using System;
using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class LoadIndexRangeArgs
	{
		[ProtoMember(1)]
		public string Table { get; set; }

		[ProtoMember(2)]
		public string Index { get; set; }

		[ProtoMember(3)]
		public Message StartIndexValue { get; set; }

		[ProtoMember(4)]
		public Message StopIndexValue { get; set; }

		[ProtoMember(5)]
		public Int32 Limit { get; set; }
	}
}
