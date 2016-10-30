using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using PlayerIOClient.Enums;
using PlayerIOClient.Helpers;

namespace PlayerIOClient
{
    /// <summary>
    /// Represents a message sent between the client and the server. 
    /// <para> A message consists of a type (string), and zero or more parameters that are supported. </para>
    /// </summary>
    public class Message : IEnumerable<object>
    {
        /// <summary> Creates a new Message. </summary>
        /// <param name="type"> The type of message to create. </param>
        /// <param name="parameters"> A list of the data to add to the message. </param>
        public Message(string type, params object[] parameters)
        {
            Type = type;
            Add(parameters);
        }

        public string Type { get; private set; }
        private readonly List<Tuple<object, MessageType>> _parameters = new List<Tuple<object, MessageType>>();

        public int Count => _parameters.Count;

        #region Get

        public object Item(uint index)
        {
            return _parameters[(int)index].Item1;
        }

        [IndexerName("index")]
        public object this[uint index] => Item(index);

        public string GetString(uint index)
        {
            return (string)this[index];
        }

        public int GetInteger(uint index)
        {
            return (int)this[index];
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
            switch ((System.Type.GetTypeCode(obj.GetType()))) {
                case TypeCode.String:
                    break;
                case TypeCode.Int32:
                    break;
                case TypeCode.Int64:
                    break;
                case TypeCode.Boolean:
                    break;
                case TypeCode.Single:
                    break;
                case TypeCode.Double:
                    break;
                case TypeCode.UInt32:
                    break;
                case TypeCode.UInt64:
                    break;
                case TypeCode.Object:
                    break;
            }

            if (obj == null) {
                throw new ArgumentNullException(ParameterNullText);
            }
            if (obj is string) {
                _parameters.Add(Tuple.Create(obj, MessageType.String));
            } else if (obj is int) {
                _parameters.Add(Tuple.Create(obj, MessageType.Integer));
            } else if (obj is bool) {
                _parameters.Add(Tuple.Create(obj, MessageType.Boolean));
            } else if (obj is float) {
                _parameters.Add(Tuple.Create(obj, MessageType.Float));
            } else if (obj is double) {
                _parameters.Add(Tuple.Create(obj, MessageType.Double));
            } else if (obj is byte[]) {
                _parameters.Add(Tuple.Create(obj, MessageType.ByteArray));
            } else if (obj is uint) {
                _parameters.Add(Tuple.Create(obj, MessageType.UInteger));
            } else if (obj is long) {
                _parameters.Add(Tuple.Create(obj, MessageType.Long));
            } else if (obj is ulong) {
                _parameters.Add(Tuple.Create(obj, MessageType.ULong));
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
                throw new ArgumentNullException("parameter", ParameterNullText);
            }
            _parameters.Add(Tuple.Create((object)parameter, MessageType.String));
        }

        public void Add(int parameter)
        {
            _parameters.Add(Tuple.Create((object)parameter, MessageType.Integer));
        }

        public void Add(uint parameter)
        {
            _parameters.Add(Tuple.Create((object)parameter, MessageType.UInteger));
        }

        public void Add(long parameter)
        {
            _parameters.Add(Tuple.Create((object)parameter, MessageType.Long));
        }

        public void Add(ulong parameter)
        {
            _parameters.Add(Tuple.Create((object)parameter, MessageType.ULong));
        }

        public void Add(byte[] parameter)
        {
            if (parameter == null) {
                throw new ArgumentNullException("parameter", ParameterNullText);
            }
            _parameters.Add(Tuple.Create((object)parameter, MessageType.ByteArray));
        }

        public void Add(float parameter)
        {
            _parameters.Add(Tuple.Create((object)parameter, MessageType.Float));
        }

        public void Add(double parameter)
        {
            _parameters.Add(Tuple.Create((object)parameter, MessageType.Double));
        }

        public void Add(bool parameter)
        {
            _parameters.Add(Tuple.Create((object)parameter, MessageType.Boolean));
        }

        #endregion Separate methods for allowed object types

        #endregion Add

        public override string ToString()
        {
            var sb = new StringBuilder("");
            sb.AppendLine(string.Concat("  msg.Type= ", Type, ", ", Count, " entries"));

            for (int i = 0; i < _parameters.Count; i++) {
                sb.AppendLine(string.Concat("  msg[", i, "] = ", _parameters[i].Item1, "  (", _parameters[i].Item2, ")"));
            }

            return sb.ToString();
        }

        internal byte[] Serialize() => new BinarySerializer().Serialize(this);

        public IEnumerator<object> GetEnumerator()
        {
            return _parameters.Select(t => t.Item1).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}