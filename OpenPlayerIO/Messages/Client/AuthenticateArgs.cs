using PlayerIOClient.Helpers;
using ProtoBuf;
using System.Collections.Generic;

namespace PlayerIOClient.Messages.Client
{
	[ProtoContract]
	internal class AuthenticateArgs
	{
		[ProtoMember(1)]
		public string GameId { get; set; }

		[ProtoMember(2)]
		public string ConnectionId { get; set; }

		[ProtoMember(3)]
		public KeyValuePair[] AuthenticationArguments { get; set; }

		[ProtoMember(4)]
		public List<string> PlayerInsightSegments { get; set; }

		[ProtoMember(5)]
		public string ClientAPI { get; set; }

		[ProtoMember(6)]
		public KeyValuePair[] ClientInfo { get; set; }

		[ProtoMember(7)]
		public List<string> PlayCodes { get; set; }
	}
}
