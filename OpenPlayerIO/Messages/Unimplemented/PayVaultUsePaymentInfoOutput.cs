using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.PayVault
{
	[ProtoContract]
	internal class PayVaultUsePaymentInfoOutput
	{
		[ProtoMember(1)]
		public Message ProviderResults { get; set; }

		[ProtoMember(2)]
		public Message VaultContents { get; set; }
	}
}
