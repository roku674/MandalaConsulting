//Copyright © 2023 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MandalaConsulting.Optimization.Logging
{
    /// <summary>
    /// Log Message Object for easier logging
    /// </summary>
    public class LogMessage
    {
        /// <summary>
        /// Extracts the actual method name from async state machine types
        /// </summary>
        /// <param name="stackTrace">The stack trace to analyze</param>
        /// <returns>The formatted method name</returns>
        private static string GetMethodNameFromStackTrace(StackTrace stackTrace)
        {
            // Walk up the stack to find the first non-framework method
            for (int i = 1; i < stackTrace.FrameCount; i++)
            {
                StackFrame frame = stackTrace.GetFrame(i);
                if (frame == null) continue;

                System.Reflection.MethodBase method = frame.GetMethod();
                if (method == null) continue;

                string declaringTypeName = method.DeclaringType?.FullName ?? "Unknown";
                string methodName = method.Name;

                // Skip LogMessage constructors
                if (method.DeclaringType?.Name == "LogMessage") continue;

                // Check if this is an async state machine generated method
                if (methodName == "MoveNext" && declaringTypeName.Contains("+<") && declaringTypeName.Contains(">d"))
                {
                    // Extract the actual method name from the compiler-generated type name
                    // Format: Namespace.Class+<MethodName>d__XX
                    int startIndex = declaringTypeName.IndexOf("+<") + 2;
                    int endIndex = declaringTypeName.IndexOf(">d", startIndex);
                    
                    if (startIndex > 1 && endIndex > startIndex)
                    {
                        string actualMethodName = declaringTypeName.Substring(startIndex, endIndex - startIndex);
                        // Get the actual class name (before the +)
                        string actualClassName = declaringTypeName.Substring(0, declaringTypeName.IndexOf("+<"));
                        return $"{actualClassName}.{actualMethodName}";
                    }
                }

                // Check if this is a lambda or anonymous method  
                if (methodName.Contains("<") && methodName.Contains(">"))
                {
                    // This is a lambda/anonymous method, keep looking up the stack
                    continue;
                }

                // For regular methods, return the full name
                return $"{declaringTypeName}.{methodName}";
            }

            // Fallback to the first frame method
            StackFrame fallbackFrame = stackTrace.GetFrame(1);
            if (fallbackFrame != null)
            {
                System.Reflection.MethodBase method = fallbackFrame.GetMethod();
                if (method != null)
                {
                    return $"{method.DeclaringType?.FullName ?? "Unknown"}.{method.Name}";
                }
            }

            return "Unknown";
        }

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
            localOperationName = GetMethodNameFromStackTrace(stackTrace);

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
            localOperationName = GetMethodNameFromStackTrace(stackTrace);

            this.messageType = messageType;
            this.message = message;
            LogAdded?.Invoke(this, new LogMessageEventArgs(this));
        }

        // Define the delegate and event
        public delegate void LogAddedEventHandler(object sender, LogMessageEventArgs e);

        public static event LogAddedEventHandler LogAdded;

        /// <summary>
        /// This is the program that is running
        /// </summary>
        public static string MessageSourceSetter { get; set; } = "You didn't set the name of your program!";

        public long id { get; set; }
        public string localOperationName { get; set; }
        public string message { get; set; }
        public string messageSource { get; set; }
        public MessageType messageType { get; set; }
        public System.DateTime timeStamp { get; set; }

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
    }

    public class LogMessageEventArgs : System.EventArgs
    {
        public LogMessageEventArgs(LogMessage logMessage)
        {
            log = logMessage;
        }

        public LogMessage log { get; }
    }
}