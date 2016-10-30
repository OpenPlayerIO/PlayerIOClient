using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class OneScoreRefreshOutput
	{
		[ProtoMember(1)]
		public Message OneScore { get; set; }
	}
}
