using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class CreateRoomOutput
	{
		[ProtoMember(1)]
		public string RoomId { get; set; }
	}
}
