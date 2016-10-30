using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class AuthenticateArgs
	{
		[ProtoMember(1)]
		public string GameId { get; set; }

		[ProtoMember(2)]
		public string ConnectionId { get; set; }

		[ProtoMember(3)]
		public Message AuthenticationArguments { get; set; }

		[ProtoMember(4)]
		public string PlayerInsightSegments { get; set; }

		[ProtoMember(5)]
		public string ClientAPI { get; set; }

		[ProtoMember(6)]
		public Message ClientInfo { get; set; }

		[ProtoMember(7)]
		public string PlayCodes { get; set; }
	}
}
