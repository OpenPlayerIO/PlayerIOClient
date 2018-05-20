using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using PlayerIOClient.Enums;

namespace PlayerIOClient
{
    /// <summary>
    /// Represents a message sent between the client and the server. 
    /// <para> A message consists of a type (string), and zero or more parameters that are supported. </para>
    /// </summary>
    public class Message : IEnumerable<object>
    {
        private readonly List<Tuple<object, MessageType>> Values = new List<Tuple<object, MessageType>>();
        public string Type { get; } = "";
        public int Count => Values.Count;

        /// <summary> Creates a new Message. </summary>
        /// <param name="type"> The type of message to create. </param>
        /// <param name="parameters"> A list of the data to add to the message. </param>
        public Message(string type, params object[] parameters)
        {
            this.Type = type;
            Add(parameters);
        }

        public static Message Create(string type, params object[] parameters)
            => new Message(type, parameters);

        #region Get

        public object Item(uint index)
        {
            return Values[(int)index].Item1;
        }

        [IndexerName("index")]
        public object this[uint index] => Item(index);

        public string GetString(uint index)
        {
            return (string)this[index];
        }

        public int GetInt(uint index)
        {
            return (int)this[index];
        }

        public int GetInteger(uint index)
        {
            return (int)this[index];
        }

        public uint GetUInt(uint index)
        {
            return (uint)this[index];
        }

        public uint GetUInteger(uint index)
        {
            return (uint)this[index];
        }

        public long GetLong(uint index)
        {
            return (long)this[index];
        }

        public ulong GetULong(uint index)
        {
            return (ulong)this[index];
        }

        public byte[] GetByteArray(uint index)
        {
            return (byte[])this[index];
        }

        public float GetFloat(uint index)
        {
            return (float)this[index];
        }

        public double GetDouble(uint index)
        {
            return (double)this[index];
        }

        public bool GetBoolean(uint index)
        {
            return (bool)this[index];
        }

        #endregion Get

        #region Add

        private const string ParameterNullText = "Can't add null values to Player.IO Messages.";

        public void Add(object obj)
        {
            if (obj == null) {
                throw new ArgumentNullException(ParameterNullText);
            }
            if (obj is string) {
                Values.Add(Tuple.Create(obj, MessageType.String));
            } else if (obj is int) {
                Values.Add(Tuple.Create(obj, MessageType.Integer));
            } else if (obj is bool) {
                Values.Add(Tuple.Create(obj, MessageType.Boolean));
            } else if (obj is float) {
                Values.Add(Tuple.Create(obj, MessageType.Float));
            } else if (obj is double) {
                Values.Add(Tuple.Create(obj, MessageType.Double));
            } else if (obj is byte[]) {
                Values.Add(Tuple.Create(obj, MessageType.ByteArray));
            } else if (obj is uint) {
                Values.Add(Tuple.Create(obj, MessageType.UInteger));
            } else if (obj is long) {
                Values.Add(Tuple.Create(obj, MessageType.Long));
            } else if (obj is ulong) {
                Values.Add(Tuple.Create(obj, MessageType.ULong));
            } else {
                throw new InvalidOperationException(
                    "Player.IO Messages only support objects of types: String, Integer, Boolean, Float, Double, Byte[], UInteger, Long & ULong." + Environment.NewLine +
                    "Type '" + obj.GetType().FullName + "' is not supported.");
            }
        }

        public void Add(params object[] parameters)
        {
            foreach (var obj in parameters) {
                Add(obj);
            }
        }

        #region Separate methods for allowed object types

        public void Add(string parameter)
        {
            if (parameter == null) {
                throw new ArgumentNullException(nameof(parameter), ParameterNullText);
            }
            Values.Add(Tuple.Create((object)parameter, MessageType.String));
        }

        public void Add(int parameter)
        {
            Values.Add(Tuple.Create((object)parameter, MessageType.Integer));
        }

        public void Add(uint parameter)
        {
            Values.Add(Tuple.Create((object)parameter, MessageType.UInteger));
        }

        public void Add(long parameter)
        {
            Values.Add(Tuple.Create((object)parameter, MessageType.Long));
        }

        public void Add(ulong parameter)
        {
            Values.Add(Tuple.Create((object)parameter, MessageType.ULong));
        }

        public void Add(byte[] parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter), ParameterNullText);

            Values.Add(Tuple.Create((object)parameter, MessageType.ByteArray));
        }

        public void Add(float parameter)
        {
            Values.Add(Tuple.Create((object)parameter, MessageType.Float));
        }

        public void Add(double parameter)
        {
            Values.Add(Tuple.Create((object)parameter, MessageType.Double));
        }

        public void Add(bool parameter)
        {
            Values.Add(Tuple.Create((object)parameter, MessageType.Boolean));
        }

        #endregion Separate methods for allowed object types

        #endregion Add

        public override string ToString()
        {
            var sb = new StringBuilder("");
            sb.AppendLine(string.Concat("  msg.Type= ", this.Type, ", ", this.Count, " entries"));

            for (var i = 0; i < Values.Count; i++)
                sb.AppendLine(string.Concat("  msg[", i, "] = ", Values[i].Item1, "  (", Values[i].Item2, ")"));

            return sb.ToString();
        }

        public IEnumerator<object> GetEnumerator()
        {
            return Values.Select(t => t.Item1).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}