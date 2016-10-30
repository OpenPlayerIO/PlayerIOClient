using System;
using System.Collections.Generic;
using System.Reflection;
using PlayerIOClient.Error;
using PlayerIOClient.Helpers;
using PlayerIOClient.Messages.ErrorLog;

namespace PlayerIOClient
{
    public class ErrorLog
    {
        private readonly HttpChannel _channel;
        private readonly string Source = Assembly.GetExecutingAssembly().GetName().Name;

        internal ErrorLog(HttpChannel channel)
        {
            _channel = channel;
        }

        /// <summary>
        /// Write an entry to the game's error log. In development the errors are just written to the
        /// console, in production they're written to a database and browseable from the admin panel
        /// </summary>
        /// <param name="error">
        /// A short string describing the error without details. Example 'Unhandled exception'
        /// </param>
        public void WriteError(string error)
        {
            this.WriteError(error, "", "", null);
        }

        /// <summary>
        /// Write an entry to the game's error log. In development the error are just written to the
        /// console, in production they're written to a database and browseable from the admin panel
        /// </summary>
        /// <param name="error">
        /// A short string describing the error without details. Example 'Unhandled exception'
        /// </param>
        /// <param name="exception"> The exception that caused the error </param>
        public void WriteError(string error, Exception exception)
        {
            this.WriteError(error, exception.Message, exception.StackTrace, null);
        }

        /// <summary>
        /// Write an entry to the games error log. In development the error are just written to the
        /// console, in production they're written to a database and browseable from the admin panel
        /// </summary>
        /// <param name="error">
        /// A short string describing the error without details. Example 'Object not set to instance
        /// of an object'
        /// </param>
        /// <param name="details">
        /// Describe the error in more detail if you have it. Example 'couldn't find the user 'bob'
        /// in the current game'
        /// </param>
        /// <param name="stacktrace"> The stacktrace (if available) of the error </param>
        /// <param name="extraData">
        /// Any extra data you'd like to associate with the error log entry
        /// </param>
        public void WriteError(string error, string details, string stacktrace, Dictionary<string, string> extraData)
        {
            _channel.Request<WriteErrorArgs, WriteErrorOutput, PlayerIOError>(50,
                new WriteErrorArgs {
                    Source = Source,
                    Error = error,
                    Details = details,
                    Stacktrace = stacktrace
                });
        }
    }
}