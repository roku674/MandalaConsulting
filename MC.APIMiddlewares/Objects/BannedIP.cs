// Copyright © Mandala Consulting, LLC., 2024. All Rights Reserved. Created by Alexander Fields https://www.alexanderfields.me on 2024-10-13 01:57:25
// Edited by Alexander Fields https://www.alexanderfields.me 2025-07-02 11:48:25
//Created by Alexander Fields

namespace MandalaConsulting.APIMiddleware.Objects
{
    /// <summary>
    /// Represents a banned IP address with IPv4 and IPv6 information.
    /// </summary>
    public class BannedIP
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BannedIP"/> class.
        /// </summary>
        public BannedIP()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BannedIP"/> class with full details.
        /// </summary>
        /// <param name="id">The unique identifier for the banned IP record.</param>
        /// <param name="ipv4">The IPv4 address.</param>
        /// <param name="ipv6">The IPv6 address.</param>
        /// <param name="reason">The reason for banning the IP.</param>
        public BannedIP(string id, string ipv4, string ipv6, string reason)
        {
            this._id = id;
            this.ipv4 = ipv4;
            this.ipv6 = ipv6;
            this.reason = reason;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BannedIP"/> class with IP addresses only.
        /// </summary>
        /// <param name="ipv4">The IPv4 address.</param>
        /// <param name="ipv6">The IPv6 address.</param>
        public BannedIP(string ipv4, string ipv6)
        {
            this.ipv4 = ipv4;
            this.ipv6 = ipv6;
        }

        /// <summary>
        /// Gets or sets the unique identifier for the banned IP record.
        /// </summary>
        public string _id { get; set; }
        /// <summary>
        /// Gets or sets the IPv4 address.
        /// </summary>
        public string ipv4 { get; set; }
        /// <summary>
        /// Gets or sets the IPv6 address.
        /// </summary>
        public string ipv6 { get; set; }
        /// <summary>
        /// Gets or sets the reason for banning the IP.
        /// </summary>
        public string reason { get; set; }
    }
}
