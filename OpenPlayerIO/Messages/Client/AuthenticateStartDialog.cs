using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Client
{
	[ProtoContract]
	internal class AuthenticateStartDialog
	{
		[ProtoMember(1)]
		public string Name { get; set; }

		[ProtoMember(2)]
		public KeyValuePair[] Arguments { get; set; }
	}
}
