using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class BigDBChangeset
	{
		[ProtoMember(1)]
		public string Table { get; set; }

		[ProtoMember(2)]
		public string Key { get; set; }

		[ProtoMember(3)]
		public string OnlyIfVersion { get; set; }

		[ProtoMember(4)]
		public Message Changes { get; set; }

		[ProtoMember(5)]
		public bool FullOverwrite { get; set; }
	}
}
