using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class PlayerInsightRefreshOutput
	{
		[ProtoMember(1)]
		public Message State { get; set; }
	}
}
