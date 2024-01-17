﻿namespace APIBilling.Objects.MAS.API.Objects
{
    public class BannedIP
    {
        public BannedIP()
        {
        }

        public BannedIP(string id, string ipv4, string ipv6)
        {
            this._id = id;
            this.ipv4 = ipv4;
            this.ipv6 = ipv6;
        }

        public BannedIP(string ipv4, string ipv6)
        {
            this.ipv4 = ipv4;
            this.ipv6 = ipv6;
        }

        public string _id { get; set; }
        public string ipv4 { get; set; }
        public string ipv6 { get; set; }
    }
}
