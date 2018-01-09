using ProtoBuf;

namespace PlayerIOClient.Messages.Client
{
	[ProtoContract]
	internal class CreateRoomOutput
	{
		[ProtoMember(1)]
		public string RoomId { get; set; }
	}
}
