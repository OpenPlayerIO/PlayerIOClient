using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using PlayerIOClient.Error;
using PlayerIOClient.Messages.Error;
using ProtoBuf;

namespace PlayerIOClient.Helpers
{
    internal class HttpChannel
    {
        private const string EndpointUri = "http://api.playerio.com/api";
        private Dictionary<string, string> _headers;

        public TResponse Request<TRequest, TResponse, TError>(int method, TRequest args) where TError : Exception
        {
            var r = default(TResponse);
            var request = GetRequest(method);
            var wc = new WebClient { Proxy = null };

            using (var requestStream = request.GetRequestStream()) {
                Serializer.Serialize(requestStream, args);
            }

            try {
                var response = request.GetResponse();
                using (var responseStream = response.GetResponseStream()) {
                    if (ReadHeader(responseStream)) {
                        r = Serializer.Deserialize<TResponse>(responseStream);
                    } else {
                        throw GetError<TError>(responseStream);
                    }
                }
            }
            catch (WebException webException) {
                if (webException.Response == null) {
                    throw new PlayerIOError(ErrorCode.GeneralError, "Connection to the Player.IO WebService has just been unexpectedly terminated.");
                }

                using (var stream = webException.Response.GetResponseStream()) {
                    if (stream == null)
                        return r;

                    using (var streamReader = new StreamReader(stream)) {
                        throw new PlayerIOError(ErrorCode.GeneralError, "Error communicating with the Player.IO WebService: " + streamReader.ReadToEnd());
                    }
                }
            }

            return r;
        }

        private WebRequest GetRequest(int method)
        {
            var value = WebRequest.Create(EndpointUri + "/" + method);
            value.Timeout = 15000;
            value.Method = "POST";

            if (_headers != null) {
                lock (_headers) {
                    foreach (var header in _headers) {
                        value.Headers[header.Key] = header.Value;
                    }
                }
            }
            return value;
        }

        private bool ReadHeader(Stream responseStream)
        {
            if (responseStream.ReadByte() == 1) {
                var num = (ushort)(responseStream.ReadByte() << 8 | responseStream.ReadByte());
                var numArray = new byte[num];
                responseStream.Read(numArray, 0, numArray.Length);
                lock (_headers) {
                    _headers["playertoken"] = Encoding.UTF8.GetString(numArray, 0, numArray.Length);
                }
            }

            return responseStream.ReadByte() == 1;
        }

        public static TError GetError<TError>(Stream errorStream) where TError : Exception
        {
            if (typeof(TError) != typeof(PlayerIOError)) {
                if (typeof(TError) != typeof(PlayerIORegistrationError)) {
                    return new ApplicationException("Unexpected error type: " + typeof(TError).FullName) as TError;
                }

                var regError = Serializer.Deserialize<RegistrationError>(errorStream);
                return new PlayerIORegistrationError(regError.ErrorCode, regError.Message, regError.UsernameError, regError.PasswordError, regError.EmailError, regError.CaptchaError) as TError;
            }
            var err = Serializer.Deserialize<Messages.Error.Error>(errorStream);
            return new PlayerIOError(err.ErrorCode, err.Message) as TError;
        }

        public void SetToken(string token) => _headers = new Dictionary<string, string>() { ["playertoken"] = token };
    }

    public static class StreamExtensions
    {
        public static byte[] ReadAllBytes(this Stream instream)
        {
            if (instream is MemoryStream)
                return ((MemoryStream)instream).ToArray();

            using (var memoryStream = new MemoryStream()) {
                instream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}