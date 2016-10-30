using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.BigDB
{
    [ProtoContract]
    internal class LoadMyPlayerObjectOutput
    {
        [ProtoMember(1)]
        public DatabaseObject PlayerObject { get; set; }
    }
}