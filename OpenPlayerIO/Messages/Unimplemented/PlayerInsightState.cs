using System;
using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class PlayerInsightState
	{
		[ProtoMember(1)]
		public Int32 PlayersOnline { get; set; }

		[ProtoMember(2)]
		public Message Segments { get; set; }
	}
}
