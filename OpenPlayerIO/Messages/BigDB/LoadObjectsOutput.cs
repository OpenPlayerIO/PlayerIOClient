using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.BigDB
{
    [ProtoContract]
    internal class LoadObjectsOutput
    {
        [ProtoMember(1)]
        public DatabaseObject[] Objects { get; set; }
    }
}