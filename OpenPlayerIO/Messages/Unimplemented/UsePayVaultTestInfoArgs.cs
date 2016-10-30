using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class UsePayVaultTestInfoArgs
	{
		[ProtoMember(1)]
		public Message Info { get; set; }
	}
}
