//Copyright © 2023 Monotonous Automation Solutions All rights reserved.

using System.Runtime.Serialization;

namespace MAS.Objects.Account
{
    public class IPInfo
    {
        public IPInfo()
        {
        }

        public IPInfo(System.DateTime firstLogin, string ipAddress, DateTime lastLogin, uint loginCount, TimeSpan timeOnline)
        {
            this.firstLogin = firstLogin;
            this.ipAddress = ipAddress;
            this.lastLogin = lastLogin;
            this.loginCount = loginCount;
            this.timeOnline = default;
        }

        [DataMember]
        public System.DateTime firstLogin
        {
            get; set;
        }

        [DataMember]
        public string ipAddress
        {
            get; set;
        }

        [DataMember]
        public System.DateTime lastLogin
        {
            get; set;
        }

        /// <summary>
        /// amount of times the ip as logged in
        /// </summary>
        [DataMember]
        public ulong loginCount
        {
            get; set;
        }

        [DataMember]
        public System.TimeSpan timeOnline
        {
            get; set;
        }
    }
}