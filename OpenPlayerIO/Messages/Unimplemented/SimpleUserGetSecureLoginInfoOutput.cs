using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class SimpleUserGetSecureLoginInfoOutput
	{
		[ProtoMember(1)]
		public byte[] PublicKey { get; set; }

		[ProtoMember(2)]
		public string Nonce { get; set; }
	}
}
