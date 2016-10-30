using System;
using System.ComponentModel;
using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient
{
    [ProtoContract]
    public class DatabaseArray : DatabaseObject
    {
        public void Set(int index, object value)
        {
            SetInternal(index, value);
        }

        public void Add(object value)
        {
            SetInternal(Properties.Count + 1, value);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override void Set(string propertyExpression, object value)
        {
            throw new Exception("You cannot set a string property from a DatabaseArray.");
        }
    }
}