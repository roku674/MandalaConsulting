//Copyright © 2023 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields

namespace MandalaConsulting.Objects
{
    public class ResponseData
    {
        public ResponseData()
        {
        }

        public ResponseData(string message, object data, object error)
        {
            this.message = message;
            Data = data;
            Error = error;
        }

        public object Data { get; set; }
        public object Error { get; set; }
        public string message { get; set; }
    }
}