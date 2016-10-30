using System.Collections.Generic;
using ProtoBuf;

namespace PlayerIOClient.Messages.ErrorLog
{
    [ProtoContract]
    internal class WriteErrorArgs
    {
        [ProtoMember(1)]
        public string Source { get; set; }

        [ProtoMember(2)]
        public string Error { get; set; }

        [ProtoMember(3)]
        public string Details { get; set; }

        [ProtoMember(4)]
        public string Stacktrace { get; set; }

        [ProtoMember(5)]
        public KeyValuePair<string, string>[] ExtraData { get; set; }
    }
}