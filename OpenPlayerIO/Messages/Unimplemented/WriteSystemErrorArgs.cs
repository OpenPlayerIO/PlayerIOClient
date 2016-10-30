using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class WriteSystemErrorArgs
	{
		[ProtoMember(1)]
		public string Source { get; set; }

		[ProtoMember(2)]
		public string MachineName { get; set; }

		[ProtoMember(3)]
		public string Error { get; set; }

		[ProtoMember(4)]
		public string Details { get; set; }

		[ProtoMember(5)]
		public string Stacktrace { get; set; }

		[ProtoMember(6)]
		public string Version { get; set; }
	}
}
