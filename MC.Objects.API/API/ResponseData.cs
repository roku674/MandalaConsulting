// Copyright © Mandala Consulting, LLC., 2024. All Rights Reserved. Created by Alexander Fields https://www.alexanderfields.me on 2024-10-12 14:24:21
// Edited by Alexander Fields https://www.alexanderfields.me 2025-07-02 11:48:25
//Created by Alexander Fields

namespace MandalaConsulting.Objects.API
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
