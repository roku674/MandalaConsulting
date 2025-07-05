// Copyright © Mandala Consulting, LLC., 2024. All Rights Reserved. Created by Alexander Fields https://www.alexanderfields.me on 2024-10-12 14:24:21
// Edited by Alexander Fields https://www.alexanderfields.me 2025-07-02 11:48:25
//Created by Alexander Fields

namespace MandalaConsulting.Objects
{
    /// <summary>
    /// Represents a standardized response format for API operations.
    /// </summary>
    public class ResponseData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseData"/> class.
        /// </summary>
        public ResponseData()
        {
            Timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'");
            Success = Error == null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseData"/> class with specified properties.
        /// </summary>
        /// <param name="message">The response message.</param>
        /// <param name="data">The response data.</param>
        /// <param name="error">Any error information.</param>
        public ResponseData(string message, object data, object error)
        {
            this.message = message;
            Data = data;
            Error = error;
            Timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'");
            Success = error == null;
        }

        /// <summary>
        /// Gets or sets the response data payload.
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// Gets or sets any error information.
        /// </summary>
        public object Error { get; set; }
        /// <summary>
        /// Gets or sets the response message.
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// Gets or sets whether the response represents a successful operation.
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Gets or sets the timestamp when the response was created in ISO 8601 format.
        /// </summary>
        public string Timestamp { get; set; }
    }
}
