using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class PlayerInsightSetSegmentsArgs
	{
		[ProtoMember(1)]
		public string Segments { get; set; }
	}
}
