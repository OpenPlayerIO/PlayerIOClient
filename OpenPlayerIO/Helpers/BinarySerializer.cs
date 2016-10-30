using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PlayerIOClient.Helpers
{
    internal enum Pattern
    {
        STRING_SHORT_PATTERN = 0xC0,
        STRING_PATTERN = 0x0C,

        BYTE_ARRAY_SHORT_PATTERN = 0x40,
        BYTE_ARRAY_PATTERN = 0x10,

        UNSIGNED_LONG_SHORT_PATTERN = 0x38,
        UNSIGNED_LONG_PATTERN = 0x3C,

        LONG_SHORT_PATTERN = 0x30,
        LONG_PATTERN = 0x34,

        UNSIGNED_INT_SHORT_PATTERN = 0x80,
        UNSIGNED_INT_PATTERN = 0x08,

        INT_PATTERN = 0x04,

        DOUBLE_PATTERN = 0x03,
        FLOAT_PATTERN = 0x02,

        BOOLEAN_TRUE_PATTERN = 0x01,
        BOOLEAN_FALSE_PATTERN = 0x00,

        DOES_NOT_EXIST = 0xFF
    }

    internal class BinaryDeserializer
    {
        private enum State { Init, Header, Data }

        private MemoryStream _buffer = new MemoryStream();
        private List<Message> _messages = new List<Message>();

        private List<object> _values = new List<object>();

        private State _status = State.Init;
        private Pattern _pattern = Pattern.DOES_NOT_EXIST;

        private int PartLength = 0;

        public Message[] Deserialize(byte[] input)
        {
            foreach (var value in input)
                DeserializeValue(value);

            var index = 0;
            while (index < _values.Count) {
                var count = (int)_values[index];
                var type = (string)_values[index + 1];

                _messages.Add(new Message(type, _values.Skip(index + 2).Take(count).ToArray()));

                index += count + 2;
            }

            return _messages.ToArray();
        }

        /*
        public Message[] Deserialize(byte[] input)
        {
            foreach (var value in input)
                DeserializeValue(value);

            var index = 0;
            while (index < _values.Count) {
                var count = (int)_values[index];
                Console.WriteLine("countx: " + count);

                var message = new Message((string)_values[index + 1], _values.Skip(index + 2).Take(count).ToArray());

                _messages.Add(message);

                index += count + 2;
            }

            return _messages.ToArray();
        }
        */

        private void OnValueDeserialized(object value)
        {
            _values.Add(value);

            _status = State.Init;
        }

        private void DeserializeValue(byte value)
        {
            switch (_status) {

                #region State.Init

                case State.Init:
                    var pattern = value.RetrieveFlagPattern();

                    switch (pattern) {
                        case Pattern.STRING_SHORT_PATTERN:
                            _pattern = pattern;
                            PartLength = value.RetrievePartLength(pattern);

                            if (PartLength > 0)
                                _status = State.Data;
                            else
                                OnValueDeserialized("");
                            break;

                        case Pattern.STRING_PATTERN:
                            _pattern = pattern;
                            PartLength = value.RetrievePartLength(pattern) + 1;

                            _status = State.Header;
                            break;

                        case Pattern.BYTE_ARRAY_SHORT_PATTERN:
                            _pattern = pattern;
                            PartLength = value.RetrievePartLength(pattern);

                            if (PartLength > 0)
                                _status = State.Data;
                            else
                                OnValueDeserialized(new byte[] { });
                            break;

                        case Pattern.BYTE_ARRAY_PATTERN:
                            _pattern = pattern;
                            PartLength = value.RetrievePartLength(pattern) + 1;

                            _status = State.Header;
                            break;

                        case Pattern.UNSIGNED_INT_SHORT_PATTERN:
                            OnValueDeserialized(value.RetrievePartLength(pattern));
                            break;

                        case Pattern.UNSIGNED_INT_PATTERN:
                            _pattern = pattern;
                            PartLength = value.RetrievePartLength(pattern) + 1;

                            _status = State.Data;
                            break;

                        case Pattern.INT_PATTERN:
                            _pattern = pattern;
                            PartLength = value.RetrievePartLength(pattern) + 1;

                            _status = State.Data;
                            break;

                        case Pattern.UNSIGNED_LONG_SHORT_PATTERN:
                            _pattern = pattern;
                            PartLength = 1;

                            _status = State.Data;
                            break;

                        case Pattern.UNSIGNED_LONG_PATTERN:
                            _pattern = pattern;
                            PartLength = 6;

                            _status = State.Data;
                            break;

                        case Pattern.LONG_SHORT_PATTERN:
                            _pattern = pattern;
                            PartLength = 1;

                            _status = State.Data;
                            break;

                        case Pattern.LONG_PATTERN:
                            _pattern = pattern;
                            PartLength = 6;

                            _status = State.Data;
                            break;

                        case Pattern.DOUBLE_PATTERN:
                            _pattern = pattern;
                            PartLength = 8;

                            _status = State.Data;
                            break;

                        case Pattern.FLOAT_PATTERN:
                            _pattern = pattern;
                            PartLength = 4;

                            _status = State.Data;
                            break;

                        case Pattern.BOOLEAN_TRUE_PATTERN:
                            OnValueDeserialized(true);
                            break;

                        case Pattern.BOOLEAN_FALSE_PATTERN:
                            OnValueDeserialized(false);
                            break;
                    }
                    break;

                #endregion State.Init

                #region State.Header

                case State.Header:
                    _buffer.WriteByte(value);

                    if (_buffer.Position == PartLength) {
                        var buffer = _buffer.ToArray();
                        var length = new List<byte>(4) { 0, 0, 0, 0 }.Select((b, index) => index <= PartLength - 1 ? buffer[index] : (byte)0);

                        PartLength = BitConverter.ToInt32(length.ToArray(), 0);
                        _status = State.Data;

                        _buffer.Position = 0;
                    }
                    break;

                #endregion State.Header

                #region State.Data

                case State.Data:
                    _buffer.WriteByte(value);

                    if (_buffer.Position == PartLength) {
                        var buffer = _buffer.ToArray();
                        var length = new List<byte>(8) { 0, 0, 0, 0, 0, 0, 0, 0 }.Select((v, index) => index <= PartLength - 1 ? buffer[index] : (byte)0);

                        Array.Reverse(buffer, 0, PartLength);

                        switch (_pattern) {
                            case Pattern.STRING_SHORT_PATTERN:
                            case Pattern.STRING_PATTERN:
                                OnValueDeserialized(Encoding.UTF8.GetString(_buffer.ToArray()));
                                break;

                            case Pattern.UNSIGNED_INT_PATTERN:
                                OnValueDeserialized(BitConverter.ToUInt32(length.ToArray(), 0));
                                break;

                            case Pattern.INT_PATTERN:
                                OnValueDeserialized(BitConverter.ToInt32(length.ToArray(), 0));
                                break;

                            case Pattern.UNSIGNED_LONG_SHORT_PATTERN:
                            case Pattern.UNSIGNED_LONG_PATTERN:
                                OnValueDeserialized(BitConverter.ToUInt64(length.ToArray(), 0));
                                break;

                            case Pattern.LONG_SHORT_PATTERN:
                            case Pattern.LONG_PATTERN:
                                OnValueDeserialized(BitConverter.ToInt64(length.ToArray(), 0));
                                break;

                            case Pattern.DOUBLE_PATTERN:
                                OnValueDeserialized(BitConverter.ToDouble(length.ToArray(), 0));
                                break;

                            case Pattern.FLOAT_PATTERN:
                                OnValueDeserialized(BitConverter.ToSingle(length.ToArray(), 0));
                                break;
                        }

                        _buffer.Position = 0;
                    }
                    break;

                    #endregion State.Data
            }
        }
    }

    internal static class PatternHelper
    {
        public static Pattern RetrieveFlagPattern(this byte input)
        {
            foreach (var pattern in ((Pattern[])Enum.GetValues(typeof(Pattern))).OrderByDescending(p => p))
                if ((input & (byte)pattern) == (byte)pattern)
                    return pattern;

            return Pattern.DOES_NOT_EXIST;
        }

        public static int RetrievePartLength(this byte input, Pattern pattern)
        {
            return input & ~(byte)pattern;
        }
    }

    internal class BinarySerializer
    {
        private static MemoryStream _buffer = new MemoryStream();

        public byte[] Serialize(Message message)
        {
            SerializeValue(message.Count);
            SerializeValue(message.Type);

            foreach (var value in message)
                SerializeValue(value);

            return _buffer.ToArray();
        }

        private void SerializeValue(object value)
        {
            switch (Type.GetTypeCode(value.GetType())) {
                case TypeCode.String:
                    Writer.WriteTagWithLength(Encoding.UTF8.GetBytes(value as string).Length, Pattern.STRING_SHORT_PATTERN, Pattern.STRING_PATTERN);
                    Writer.Write(Encoding.UTF8.GetBytes(value as string));
                    break;

                case TypeCode.Int32:
                    Writer.WriteTagWithLength((int)value, Pattern.UNSIGNED_INT_SHORT_PATTERN, Pattern.INT_PATTERN);
                    break;

                case TypeCode.UInt32:
                    Writer.WriteBottomPatternAndBytes(Pattern.UNSIGNED_INT_PATTERN, LittleEndianToNetworkOrderBitConverter.GetBytes((uint)value));
                    break;

                case TypeCode.Int64:
                    Writer.WriteLongPattern(Pattern.LONG_SHORT_PATTERN, Pattern.LONG_PATTERN, LittleEndianToNetworkOrderBitConverter.GetBytes((long)value));
                    break;

                case TypeCode.UInt64:
                    Writer.WriteLongPattern(Pattern.UNSIGNED_LONG_SHORT_PATTERN, Pattern.UNSIGNED_LONG_PATTERN, LittleEndianToNetworkOrderBitConverter.GetBytes((ulong)value));
                    break;

                case TypeCode.Double:
                    Writer.Write(Pattern.DOUBLE_PATTERN);
                    Writer.Write(LittleEndianToNetworkOrderBitConverter.GetBytes((double)value));
                    break;

                case TypeCode.Single:
                    Writer.Write(Pattern.FLOAT_PATTERN);
                    Writer.Write(LittleEndianToNetworkOrderBitConverter.GetBytes((float)value));
                    break;

                case TypeCode.Boolean:
                    Writer.Write((bool)value ? Pattern.BOOLEAN_TRUE_PATTERN : Pattern.BOOLEAN_FALSE_PATTERN);
                    break;

                case TypeCode.Object:
                    if (!(value is byte[]))
                        break;

                    var array = (byte[])value;

                    Writer.WriteTagWithLength(array.Length, Pattern.BYTE_ARRAY_SHORT_PATTERN, Pattern.BYTE_ARRAY_PATTERN);
                    Writer.Write(array);
                    break;
            }
        }

        private class Writer
        {
            internal static void WriteTagWithLength(int length, Pattern topPattern, Pattern bottomPattern)
            {
                if (length > 63 || length < 0) {
                    var bytes = LittleEndianToNetworkOrderBitConverter.GetBytes(length);
                    WriteBottomPatternAndBytes(bottomPattern, bytes);
                } else {
                    _buffer.WriteByte((byte)((int)topPattern | length));
                }
            }

            internal static void WriteBottomPatternAndBytes(Pattern pattern, byte[] bytes)
            {
                var counter = bytes[0] != 0 ? 3 : bytes[1] != 0 ? 2 : bytes[2] != 0 ? 1 : 0;

                _buffer.WriteByte((byte)((int)pattern | counter));
                _buffer.Write(bytes, bytes.Length - counter - 1, counter + 1);
            }

            internal static void WriteLongPattern(Pattern shortPattern, Pattern longPattern, byte[] bytes)
            {
                int counter = 0;
                for (int nc = 0; nc != 7; nc++) {
                    if (bytes[nc] != 0) {
                        counter = 7 - nc;
                        break;
                    }
                }

                if (counter > 3)
                    _buffer.WriteByte((byte)((byte)longPattern | counter - 4));
                else
                    _buffer.WriteByte((byte)((byte)shortPattern | counter));

                _buffer.Write(bytes, bytes.Length - counter - 1, counter + 1);
            }

            internal static void Write(Pattern pattern) => Write((byte)pattern);

            internal static void Write(byte value)
            {
                _buffer.WriteByte(value);
            }

            internal static void Write(byte[] value)
            {
                _buffer.Write(value, 0, value.Length);
            }
        }
    }

    internal class LittleEndianToNetworkOrderBitConverter
    {
        public static byte[] GetBytes(int value) => ReverseArray(BitConverter.GetBytes(value), 4, 0);

        public static byte[] GetBytes(uint value) => ReverseArray(BitConverter.GetBytes(value), 4, 0);

        public static byte[] GetBytes(long value) => ReverseArray(BitConverter.GetBytes(value), 8, 0);

        public static byte[] GetBytes(ushort value) => ReverseArray(BitConverter.GetBytes(value), 2, 0);

        public static byte[] GetBytes(ulong value) => ReverseArray(BitConverter.GetBytes(value), 8, 0);

        public static byte[] GetBytes(float value) => ReverseArray(BitConverter.GetBytes(value), 4, 0);

        public static byte[] GetBytes(double value) => ReverseArray(BitConverter.GetBytes(value), 8, 0);

        public static int ToInt32(byte[] value, int startIndex, int length) => BitConverter.ToInt32(ReverseArray(value, length, startIndex), startIndex);

        public static uint ToUInt32(byte[] value, int startIndex, int length) => BitConverter.ToUInt32(ReverseArray(value, length, startIndex), startIndex);

        public static long ToInt64(byte[] value, int startIndex, int length) => BitConverter.ToInt64(ReverseArray(value, length, startIndex), startIndex);

        public static ulong ToUInt64(byte[] value, int startIndex, int length) => BitConverter.ToUInt64(ReverseArray(value, length, startIndex), startIndex);

        public static float ToSingle(byte[] value, int startIndex, int length) => BitConverter.ToSingle(ReverseArray(value, length, startIndex), startIndex);

        public static double ToDouble(byte[] value, int startIndex, int length) => BitConverter.ToDouble(ReverseArray(value, length, startIndex), startIndex);

        private static byte[] ReverseArray(byte[] input, int length, int startIndex)
        {
            Array.Reverse(input, startIndex, length);
            return input;
        }
    }
}