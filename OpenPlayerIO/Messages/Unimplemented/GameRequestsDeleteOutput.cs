using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class GameRequestsDeleteOutput
	{
		[ProtoMember(1)]
		public Message Requests { get; set; }

		[ProtoMember(2)]
		public bool MoreRequestsWaiting { get; set; }
	}
}
