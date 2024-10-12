
//Copyright © 2023 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields
using System.Diagnostics;

namespace MandalaConsulting.Optimization.Logging
{
    /// <summary>
    /// Log Message Object for easier logging
    /// </summary>
    public class LogMessage
    {
        // Define the delegate and event
        public delegate void LogAddedEventHandler(object sender, LogMessageEventArgs e);
        public static event LogAddedEventHandler LogAdded;

        public long id { get; set; }
        public System.DateTime timeStamp { get; set; }
        /// <summary>
        /// This is the program that is running
        /// </summary>
        public static string MessageSourceSetter { get; set; } = "You didn't set the name of your program!";
        public string messageSource { get; set; }
        public string localOperationName { get; set; }
        public MessageType messageType { get; set; }
        public string message { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public LogMessage()
        {
        }

        /// <summary>
        /// Partial Constructor
        /// </summary>
        /// <param name="localOperationName"></param>
        /// <param name="messageType"></param>
        /// <param name="message"></param>
        public LogMessage(MessageType messageType, string message)
        {
            timeStamp = System.DateTime.Now;
            messageSource = MessageSourceSetter;

            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);

            if (stackFrame.GetMethod()?.DeclaringType?.Name == "LogMessage")
            {
                stackFrame = stackTrace.GetFrame(2);
            }

            if (stackFrame == null)
            {
                stackFrame = stackTrace.GetFrame(1);
            }

            System.Reflection.MethodBase method = stackFrame?.GetMethod();
            localOperationName = $"{method.DeclaringType?.FullName}.{method.Name}";

            this.messageType = messageType;
            this.message = message;
            LogAdded?.Invoke(this, new LogMessageEventArgs(this));
        }

        /// <summary>
        /// Full Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="timeStamp"></param>
        /// <param name="localOperationName"></param>
        /// <param name="messageType"></param>
        /// <param name="message"></param>
        public LogMessage(int id, MessageType messageType, string message)
        {
            this.id = id;
            timeStamp = System.DateTime.Now;
            messageSource = MessageSourceSetter;

            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);

            if (stackFrame.GetMethod()?.DeclaringType?.Name == "LogMessage")
            {
                stackFrame = stackTrace.GetFrame(2);
            }

            if (stackFrame == null)
            {
                stackFrame = stackTrace.GetFrame(1);
            }

            System.Reflection.MethodBase method = stackFrame?.GetMethod();
            localOperationName = $"{method.DeclaringType?.FullName}.{method.Name}";

            this.messageType = messageType;
            this.message = message;
            LogAdded?.Invoke(this, new LogMessageEventArgs(this));
        }

        /// <summary>
        /// Static method to create a critical log message
        /// </summary>
        /// <param name="message">The critical message</param>
        /// <returns>A LogMessage instance with MessageType.Critical</returns>
        public static LogMessage Critical(string message)
        {
            return new LogMessage(MessageType.Critical, message);
        }

        /// <summary>
        /// Static method to create a critical log message
        /// </summary>
        /// <param name="id">id of the log message</param>
        /// <param name="message">The critical message</param>
        /// <returns>A LogMessage instance with MessageType.Critical</returns>
        public static LogMessage Critical(int id, string message)
        {
            return new LogMessage(id, MessageType.Critical, message);
        }

        /// <summary>
        /// Static method to create an error log message
        /// </summary>
        /// <param name="message">The error message</param>
        /// <returns>A LogMessage instance with MessageType.Error</returns>
        public static LogMessage Error(string message)
        {
            return new LogMessage(MessageType.Error, message);
        }

        /// <summary>
        /// Static method to create an error log message with an ID
        /// </summary>
        /// <param name="id">ID of the log message</param>
        /// <param name="message">The error message</param>
        /// <returns>A LogMessage instance with MessageType.Error</returns>
        public static LogMessage Error(int id, string message)
        {
            return new LogMessage(id, MessageType.Error, message);
        }

        /// <summary>
        /// Static method to create an informational log message
        /// </summary>
        /// <param name="message">The informational message</param>
        /// <returns>A LogMessage instance with MessageType.Informational</returns>
        public static LogMessage Informational(string message)
        {
            return new LogMessage(MessageType.Informational, message);
        }

        /// <summary>
        /// Static method to create an informational log message
        /// </summary>
        /// <param name="id">ID of the log message</param>
        /// <param name="message">The informational message</param>
        /// <returns>A LogMessage instance with MessageType.Informational</returns>
        public static LogMessage Informational(int id, string message)
        {
            return new LogMessage(id, MessageType.Informational, message);
        }

        /// <summary>
        /// Static method to create a message log message
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>A LogMessage instance with MessageType.Message</returns>
        public static LogMessage Message(string message)
        {
            return new LogMessage(MessageType.Message, message);
        }

        /// <summary>
        /// Static method to create a message log message
        /// </summary>
        /// <param name="id">ID of the log message</param>
        /// <param name="message">The message</param>
        /// <returns>A LogMessage instance with MessageType.Message</returns>
        public static LogMessage Message(int id, string message)
        {
            return new LogMessage(id, MessageType.Message, message);
        }

        /// <summary>
        /// Static method to create a success log message
        /// </summary>
        /// <param name="message">The success message</param>
        /// <returns>A LogMessage instance with MessageType.Success</returns>
        public static LogMessage Success(string message)
        {
            return new LogMessage(MessageType.Success, message);
        }

        /// <summary>
        /// Static method to create a success log message with an ID
        /// </summary>
        /// <param name="id">ID of the log message</param>
        /// <param name="message">The success message</param>
        /// <returns>A LogMessage instance with MessageType.Success</returns>
        public static LogMessage Success(int id, string message)
        {
            return new LogMessage(id, MessageType.Success, message);
        }

        /// <summary>
        /// Static method to create a warning log message
        /// </summary>
        /// <param name="message">The warning message</param>
        /// <returns>A LogMessage instance with MessageType.Warning</returns>    
        public static LogMessage Warning(string message)
        {
            return new LogMessage(MessageType.Warning, message);
        }

        /// <summary>
        /// Static method to create a warning log message
        /// </summary>
        /// <param name="id">ID of the log message</param>
        /// <param name="message">The warning message</param>
        /// <returns>A LogMessage instance with MessageType.Warning</returns>    
        public static LogMessage Warning(int id, string message)
        {
            return new LogMessage(id, MessageType.Warning, message);
        }

        /// <summary>
        /// Static method to create a celebrate log message
        /// </summary>
        /// <param name="message">The celebrate message</param>
        /// <returns>A LogMessage instance with MessageType.Celebrate</returns>
        public static LogMessage Celebrate(string message)
        {
            return new LogMessage(MessageType.Celebrate, message);
        }

        /// <summary>
        /// Static method to create a celebrate log message
        /// </summary>
        /// <param name="id">ID of the log message</param>
        /// <param name="message">The celebrate message</param>
        /// <returns>A LogMessage instance with MessageType.Celebrate</returns>
        public static LogMessage Celebrate(int id, string message)
        {
            return new LogMessage(id, MessageType.Celebrate, message);
        }
    }

    public class LogMessageEventArgs : System.EventArgs
    {
        public LogMessage log { get; }

        public LogMessageEventArgs(LogMessage logMessage)
        {
            log = logMessage;
        }
    }
}
