using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using PlayerIOClient.Enums;
using PlayerIOClient.Helpers;
using ProtoBuf;

namespace PlayerIOClient.Messages.BigDB
{
    public partial class BigDBObjectValue
    {
        public object Value { get; set; }

        public BigDBObjectValue()
        {

        }

        internal BigDBObjectValue(ObjectType type, object value)
        {
            this.Type = type;
            this.Value = value;

            foreach (var property in this.GetProperties())
                property.SetValue(this, this.Value);
        }

        public static BigDBObjectValue Create(object value)
        {
            switch (value) {
                case string _: return new BigDBObjectValue(ObjectType.String, value);
                case int _:    return new BigDBObjectValue(ObjectType.Int, value);
                case uint _:   return new BigDBObjectValue(ObjectType.UInt, value);
                case long _:   return new BigDBObjectValue(ObjectType.Long, value);
                case float _:  return new BigDBObjectValue(ObjectType.Float, value);
                case double _: return new BigDBObjectValue(ObjectType.Double, value);
                case bool _:   return new BigDBObjectValue(ObjectType.Bool, value);
                case byte[] _: return new BigDBObjectValue(ObjectType.ByteArray, value);

                case DateTime DateTime:              return new BigDBObjectValue(ObjectType.DateTime, DateTime.ToUnixTime());
                case DatabaseObject DatabaseObject:  return new BigDBObjectValue(ObjectType.DatabaseObject, DatabaseObject.Properties.ToArray());
                case DatabaseObject[] DatabaseArray: return new BigDBObjectValue(ObjectType.DatabaseArray, DatabaseArray.SelectMany(p => p.Properties).ToArray());

                default: throw new ArgumentException($"The type { value.GetType().FullName } is not supported.", nameof(value));
            }
        }

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            foreach (var property in this.GetProperties())
                this.Value = property.GetValue(this);
        }

        internal IEnumerable<PropertyInfo> GetProperties()
        {
            foreach (var property in this.GetType().GetProperties()) {
                var attributes = property.GetCustomAttributes(typeof(ProtoMemberAttribute), true).Select(attribute => (ProtoMemberAttribute)attribute);

                foreach (var attribute in attributes)
                    if (attribute.Tag == (int)this.Type + 2)
                        yield return property;
            }
        }
    }
}
