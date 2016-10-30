using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.PayVault
{
	[ProtoContract]
	internal class PayVaultBuyArgs
	{
		[ProtoMember(1)]
		public Message Items { get; set; }

		[ProtoMember(2)]
		public bool StoreItems { get; set; }

		[ProtoMember(3)]
		public string TargetUserId { get; set; }
	}
}
