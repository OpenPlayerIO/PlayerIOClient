using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class GameRequestsRefreshArgs
	{
		[ProtoMember(1)]
		public string PlayCodes { get; set; }
	}
}
