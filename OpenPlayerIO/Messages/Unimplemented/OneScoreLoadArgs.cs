using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class OneScoreLoadArgs
	{
		[ProtoMember(1)]
		public string UserIds { get; set; }
	}
}
