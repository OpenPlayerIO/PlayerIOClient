using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class TestNotification
	{
		[ProtoMember(1)]
		public uint EndpointGameId { get; set; }

		[ProtoMember(2)]
		public uint EndpointUserId { get; set; }

		[ProtoMember(3)]
		public string EndpointType { get; set; }

		[ProtoMember(4)]
		public string EndpointIdentifier { get; set; }

		[ProtoMember(5)]
		public Message EndpointConfiguration { get; set; }

		[ProtoMember(6)]
		public bool EndpointEnabled { get; set; }

		[ProtoMember(7)]
		public Message Data { get; set; }
	}
}
