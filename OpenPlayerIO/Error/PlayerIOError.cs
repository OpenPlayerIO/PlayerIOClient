using System;

namespace PlayerIOClient.Error
{
    /// <summary>
    /// The base class for catching and reporting errors related to the Player.IO client.
    /// </summary>
    public class PlayerIOError : ApplicationException
    {
        public ErrorCode ErrorCode { get; private set; }

        /// <summary> Creates a new instance of PlayerIOError. </summary>
        /// <param name="errorCode"> The code of the error that happened. </param>
        /// <param name="message"> The error explained by words. </param>
        public PlayerIOError(ErrorCode errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}