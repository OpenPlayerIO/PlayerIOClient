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
            Set(index.ToString(), value);
        }

        public void Add(object value)
        {
            Set(this.Properties.Count + 1, value);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override DatabaseObject Set(string propertyExpression, object value)
        {
            throw new Exception("You cannot set a string property from a DatabaseArray.");
        }
    }
}