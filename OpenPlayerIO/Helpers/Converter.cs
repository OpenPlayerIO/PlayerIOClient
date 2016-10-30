using System.Collections.Generic;
using System.Linq;
using PlayerIOClient.Messages;

namespace PlayerIOClient.Helpers
{
    internal static class Converter
    {
        internal static KeyValuePair[] Convert(Dictionary<string, string> dict)
        {
            var keyValuePairs = new List<KeyValuePair>();

            if (dict != null) {
                keyValuePairs.AddRange((from kvp in dict select new KeyValuePair { Key = kvp.Key, Value = kvp.Value }));
            }

            return keyValuePairs.ToArray();
        }

        internal static Dictionary<string, string> Convert(KeyValuePair[] keyValuePair)
        {
            var dict = new Dictionary<string, string>();

            if (keyValuePair != null) {
                foreach (var valuePair in keyValuePair) {
                    dict[valuePair.Key] = valuePair.Value;
                }
            }

            return dict;
        }

        internal static ServerEndpoint Convert(Messages.ServerEndpoint serverEndpoint)
        {
            return new ServerEndpoint(serverEndpoint.Address, serverEndpoint.Port);
        }
    }
}