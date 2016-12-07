using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.BigDB
{
    [ProtoContract]
    internal class CreateObjectsArgs
    {
        [ProtoMember(1)]
        public SentDatabaseObject[] Objects { get; set; }

        [ProtoMember(2)]
        public bool LoadExisting { get; set; }
    }
}