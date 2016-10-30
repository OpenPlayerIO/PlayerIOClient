using System;
using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class LoadMatchingObjectsArgs
	{
		[ProtoMember(1)]
		public string Table { get; set; }

		[ProtoMember(2)]
		public string Index { get; set; }

		[ProtoMember(3)]
		public Message IndexValue { get; set; }

		[ProtoMember(4)]
		public Int32 Limit { get; set; }
	}
}
