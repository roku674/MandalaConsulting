// Copyright Mandala Consulting, LLC., 2024. All Rights Reserved. Created by Alexander Fields https://www.alexanderfields.me on 2024-10-12 14:24:21
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
