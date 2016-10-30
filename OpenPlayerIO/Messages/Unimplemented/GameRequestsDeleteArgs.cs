using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class GameRequestsDeleteArgs
	{
		[ProtoMember(1)]
		public string RequestIds { get; set; }
	}
}
