using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.Unimplemented
{
	[ProtoContract]
	internal class SaveObjectChangesArgs
	{
		[ProtoMember(1)]
		public int LockType { get; set; }

		[ProtoMember(2)]
		public Message Changesets { get; set; }

		[ProtoMember(3)]
		public bool CreateIfMissing { get; set; }
	}
}
