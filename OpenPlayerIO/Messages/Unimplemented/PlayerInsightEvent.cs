using System;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class PlayerInsightEvent
	{
		[ProtoMember(1)]
		public string EventType { get; set; }

		[ProtoMember(2)]
		public Int32 Value { get; set; }
	}
}
