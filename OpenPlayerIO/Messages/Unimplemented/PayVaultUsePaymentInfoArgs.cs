using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.PayVault
{
	[ProtoContract]
	internal class PayVaultUsePaymentInfoArgs
	{
		[ProtoMember(1)]
		public string Provider { get; set; }

		[ProtoMember(2)]
		public Message ProviderArguments { get; set; }
	}
}
