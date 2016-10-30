using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class PlayerInsightTrackEventsArgs
	{
		[ProtoMember(1)]
		public Message Events { get; set; }
	}
}
