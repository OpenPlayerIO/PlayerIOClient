using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class OneScoreLoadOutput
	{
		[ProtoMember(1)]
		public Message OneScores { get; set; }
	}
}
