using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class AuthenticateStartDialog
	{
		[ProtoMember(1)]
		public string Name { get; set; }

		[ProtoMember(2)]
		public Message Arguments { get; set; }
	}
}
