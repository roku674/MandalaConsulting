// Copyright © Mandala Consulting, LLC., 2024. All Rights Reserved. Created by Alexander Fields https://www.alexanderfields.me on 2024-01-16 19:19:01
// Edited by Alexander Fields https://www.alexanderfields.me 2025-07-02 11:48:25
//Created by Alexander Fields

using System.Runtime.Serialization;

namespace MandalaConsulting.Objects.API.Account
{
    public class IPInfo
    {
        public IPInfo()
        {
        }

        public IPInfo(System.DateTime firstLogin, string ipAddress, System.DateTime lastLogin, uint loginCount, System.TimeSpan timeOnline)
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
