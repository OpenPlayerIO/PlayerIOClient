using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class TestNotificationRegisteredEndpoint
	{
		[ProtoMember(1)]
		public uint GameId { get; set; }

		[ProtoMember(2)]
		public uint UserId { get; set; }

		[ProtoMember(3)]
		public string Type { get; set; }

		[ProtoMember(4)]
		public string Identifier { get; set; }

		[ProtoMember(5)]
		public Message Configuration { get; set; }

		[ProtoMember(6)]
		public bool Enabled { get; set; }
	}
}
