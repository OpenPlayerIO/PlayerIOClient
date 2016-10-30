using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class LoadIndexRangeOutput
	{
		[ProtoMember(1)]
		public Message Objects { get; set; }
	}
}
