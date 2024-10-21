//Copyright © 2023 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields

namespace MandalaConsulting.APIMiddleware.Objects
{
    public class BannedIP
    {
        public BannedIP()
        {
        }

        public BannedIP(string id, string ipv4, string ipv6, string reason)
        {
            this._id = id;
            this.ipv4 = ipv4;
            this.ipv6 = ipv6;
            this.reason = reason;
        }

        public BannedIP(string ipv4, string ipv6)
        {
            this.ipv4 = ipv4;
            this.ipv6 = ipv6;
        }

        public string _id { get; set; }
        public string ipv4 { get; set; }
        public string ipv6 { get; set; }
        public string reason { get; set; }
    }
}