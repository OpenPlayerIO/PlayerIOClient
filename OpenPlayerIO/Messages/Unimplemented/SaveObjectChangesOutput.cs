using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class SaveObjectChangesOutput
	{
		[ProtoMember(1)]
		public string Versions { get; set; }
	}
}
