using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using PlayerIOClient.Enums;
using PlayerIOClient.Error;
using PlayerIOClient.Messages.BigDB;

namespace PlayerIOClient.Helpers
{
    public partial class DatabaseObject : IEnumerable<KeyValuePair<string, object>>, IEnumerable
    {
        public string Table { get; set; }

        #region Enumerator

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            foreach (KeyValuePair<string, BigDBObjectValue> current in Properties)
                yield return new KeyValuePair<string, object>(current.Key, current.Value.GetRealValue());
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Properties.Select(x => x.Value).GetEnumerator();
        }

        #endregion Enumerator

        #region Get

        [IndexerName("propertyExpression")]
        public object this[string propertyExpression] => Item(propertyExpression);

        internal object Item(string propertyExpression)
        {
            if (Properties == null)
                return null;

            var output = Properties.Where(x => x.Key == propertyExpression).FirstOrDefault().Value;

            return output?.GetRealValue();
        }

        internal object Item(string propertyExpression, ObjectType expectedType)
        {
            if (Properties == null)
                return null;

            var output = Properties.Where(x => x.Key == propertyExpression).FirstOrDefault().Value;

            if (output.Type == expectedType)
                return output?.GetRealValue();
            else
                throw new PlayerIOError(ErrorCode.GeneralError, $"The value is not { expectedType.ToString() }, it's type is: { output.Type.ToString() }");
        }

        public bool GetBool(string propertyExpression) => (bool)this.Item(propertyExpression, ObjectType.Bool);

        public bool GetBool(string propertyExpression, bool defaultValue)
        {
            if (!this.Properties.Any(property => property.Key == propertyExpression))
                return defaultValue;
            else
                return (bool)this.Item(propertyExpression, ObjectType.Bool);
        }

        public byte[] GetBytes(string propertyExpression) => (byte[])this.Item(propertyExpression, ObjectType.ByteArray);

        public byte[] GetBytes(string propertyExpression, byte[] defaultValue)
        {
            if (!this.Properties.Any(property => property.Key == propertyExpression))
                return defaultValue;
            else
                return (byte[])this.Item(propertyExpression, ObjectType.ByteArray);
        }

        public double GetDouble(string propertyExpression) => (double)this.Item(propertyExpression, ObjectType.Double);

        public double GetDouble(string propertyExpression, double defaultValue)
        {
            if (!this.Properties.Any(property => property.Key == propertyExpression))
                return defaultValue;
            else
                return (double)this.Item(propertyExpression, ObjectType.Double);
        }

        public float GetFloat(string propertyExpression) => (float)this.Item(propertyExpression, ObjectType.Float);

        public float GetFloat(string propertyExpression, float defaultValue)
        {
            if (!this.Properties.Any(property => property.Key == propertyExpression))
                return defaultValue;
            else
                return (float)this.Item(propertyExpression, ObjectType.Float);
        }

        public int GetInt(string propertyExpression) => (int)this.Item(propertyExpression, ObjectType.Int);

        public int GetInt(string propertyExpression, int defaultValue)
        {
            if (!this.Properties.Any(property => property.Key == propertyExpression))
                return defaultValue;
            else
                return (int)this.Item(propertyExpression, ObjectType.Int);
        }

        public long GetLong(string propertyExpression) => (long)this.Item(propertyExpression, ObjectType.Long);

        public long GetLong(string propertyExpression, long defaultValue)
        {
            if (!this.Properties.Any(property => property.Key == propertyExpression))
                return defaultValue;
            else
                return (long)this.Item(propertyExpression, ObjectType.Long);
        }

        public string GetString(string propertyExpression) => (string)this.Item(propertyExpression, ObjectType.String);

        public string GetString(string propertyExpression, string defaultValue)
        {
            if (!this.Properties.Any(property => property.Key == propertyExpression))
                return defaultValue;
            else
                return (string)this.Item(propertyExpression, ObjectType.String);
        }

        public uint GetUInt(string propertyExpression) => (uint)this.Item(propertyExpression, ObjectType.UInt);

        public uint GetUInt(string propertyExpression, uint defaultValue)
        {
            if (!this.Properties.Any(property => property.Key == propertyExpression))
                return defaultValue;
            else
                return (uint)this.Item(propertyExpression, ObjectType.UInt);
        }

        public object GetValue(string propertyExpression)
        {
            return this.Item(propertyExpression);
        }

        public DateTime GetDateTime(string propertyExpression)
        {
            return (DateTime)this.Item(propertyExpression);
        }

        public DateTime GetDateTime(string propertyExpression, DateTime defaultValue)
        {
            if (!this.Properties.Any(property => property.Key == propertyExpression))
                return defaultValue;
            else
                return new DateTime(1970, 1, 1).AddMilliseconds((double)this.Item(propertyExpression, ObjectType.DateTime));
        }

        public DatabaseArray GetArray(string propertyExpression)
        {
            return (DatabaseArray)this.Item(propertyExpression, ObjectType.Array);
        }

        public DatabaseObject GetObject(string propertyExpression)
        {
            return (DatabaseObject)this.Item(propertyExpression, ObjectType.Array);
        }

        #endregion Get

        #region Set

        public virtual void Set(string propertyExpression, object value) => SetInternal(propertyExpression, value);

        internal void SetInternal(object propertyExpressionOrIndex, object value)
        {
            var objectValue = new BigDBObjectValue();

            if (value is string) {
                objectValue.Type = ObjectType.String;
                objectValue.ValueString = (string)value;
            }
            if (value is int) {
                objectValue.Type = ObjectType.Int;
                objectValue.ValueInteger = (int)value;
            }
            if (value is uint) {
                objectValue.Type = ObjectType.UInt;
                objectValue.ValueUInteger = (uint)value;
            }
            if (value is long) {
                objectValue.Type = ObjectType.Long;
                objectValue.ValueLong = (long)value;
            }
            if (value is bool) {
                objectValue.Type = ObjectType.Bool;
                objectValue.ValueBoolean = (bool)value;
            }
            if (value is float) {
                objectValue.Type = ObjectType.Float;
                objectValue.ValueFloat = (float)value;
            }
            if (value is double) {
                objectValue.Type = ObjectType.Double;
                objectValue.ValueDouble = (double)value;
            }
            if (value is byte[]) {
                objectValue.Type = ObjectType.ByteArray;
                objectValue.ValueByteArray = (byte[])value;
            }
            if (value is DateTime) {
                objectValue.Type = ObjectType.DateTime;
                objectValue.ValueLong = Convert.ToInt64((((DateTime)value) - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);
            }
            if (value.GetType().IsArray && typeof(BigDBObjectValue).IsAssignableFrom(value.GetType().GetElementType())) {
                objectValue.Type = ObjectType.Array;
                objectValue.ValueArray = ((BigDBObjectValue[])value).Select((v, i) => new KeyValuePair<int, BigDBObjectValue>(i, v)).ToArray();
            }
            if (value is KeyValuePair<string, BigDBObjectValue>) {
                objectValue.Type = ObjectType.Obj;
                objectValue.ValueObject = (KeyValuePair<string, BigDBObjectValue>[])value;
            }
            if (value is DatabaseObject) {
                objectValue.Type = ObjectType.Obj;
                objectValue.ValueObject = ((DatabaseObject)value).Properties.ToArray();
            }
            if (value is DatabaseArray) {
                objectValue.Type = ObjectType.Array;
                objectValue.ValueArray = ((DatabaseObject)value).Properties.Select((v, i) => new KeyValuePair<int, BigDBObjectValue>(i, v.Value)).ToArray();
            }

            Properties.Add(new KeyValuePair<string, BigDBObjectValue>(propertyExpressionOrIndex as string, objectValue));
        }

        #endregion Set

        public void Remove(string propertyExpression)
        {
            Properties.RemoveAll(x => x.Key == propertyExpression);
        }

        public void Clear()
        {
            Properties.Clear();
        }

        public bool Contains(string propertyExpression)
        {
            return Properties.Any(property => property.Key == propertyExpression);
        }

        public override string ToString()
        {
            return SimpleJson.FormatJson(SimpleJson.SerializeObject(ToDictionary(this)));
        }

        internal object ToDictionary(dynamic input)
        {
            var dictionary = new System.Dynamic.ExpandoObject() as IDictionary<string, object>;

            if (input is DatabaseObject)
                foreach (var property in input.Properties)
                    dictionary.Add(property.Key, ToDictionary(property.Value));
            else if ((input is BigDBObjectValue)) {
                switch ((input as BigDBObjectValue).Type) {
                    case ObjectType.Obj:
                    case ObjectType.Array:
                        foreach (var property in input.GetRealValue())
                            dictionary.Add(property.Key.ToString(), ToDictionary(property.Value));
                        break;

                    default:
                        return ToDictionary(input.GetRealValue());
                }
            } else if (input is List<KeyValuePair<int, object>>)
                foreach (var property in input)
                    dictionary.Add(property.Key.ToString(), ToDictionary(property.Value));
            else
                return input;

            return dictionary;
        }
    }
}