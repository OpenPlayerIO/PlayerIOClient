using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class GameRequestsSendArgs
	{
		[ProtoMember(1)]
		public string RequestType { get; set; }

		[ProtoMember(2)]
		public Message RequestData { get; set; }

		[ProtoMember(3)]
		public string RequestRecipients { get; set; }
	}
}
