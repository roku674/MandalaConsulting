﻿//Copyright © 2023 Perilous Games, Ltd. All rights reserved.

using MAS.Objects.Billing;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;

namespace MAS.Objects.Account
{
    [System.Serializable]
    public class User
    {
        public User()
        {
        }

        public User(System.DateTime banned, Bill bill, System.DateTime createdDate, string email, bool emailConfirmed, List<IPInfo> ipAddresses, System.DateTime lastActiveDate, string password, long perilousPoints, Profile profile, string role, System.TimeSpan timeOnline, string userId, string token, System.DateTime tokenExpiration)
        {
            this.banned = banned != default ? banned : new System.DateTime(1900, 1, 1);
            this.Bill = bill;
            this.createdDate = createdDate != default ? createdDate : System.DateTime.Now;
            this.email = email;
            this.emailConfirmed = emailConfirmed;
            this.IPAdresses = ipAddresses ?? new List<IPInfo>();
            this.lastActiveDate = lastActiveDate != default ? lastActiveDate : System.DateTime.Now;
            this.password = password ?? throw new System.ArgumentNullException(nameof(password));
            this.perilousPoints = perilousPoints;
            this.Profile = profile ?? new Profile();
            this.role = role;
            this.timeOnline = timeOnline;
            this.userId = userId;
            this.token = token;
            this.tokenExpiration = tokenExpiration;
        }

        /// <summary>
        /// Datetime in which the ban is lifted (set this in the past if they're not banned)
        /// </summary>
        [DataMember]
        public System.DateTime banned
        {
            get; set;
        }

        [DataMember]
        public Bill Bill
        {
            get; set;
        }

        [DataMember]
        public System.DateTime createdDate
        {
            get; set;
        }

        /// <summary>
        /// email serves as their login name
        /// </summary>
        [DataMember]
        public string email
        {
            get; set;
        }

        [DataMember]
        public bool emailConfirmed
        {
            get; set;
        }

        [DataMember]
        public List<IPInfo> IPAdresses
        {
            get; set;
        }

        [DataMember]
        public System.DateTime lastActiveDate
        {
            get; set;
        }

        [DataMember]
        public string password
        {
            get; set;
        }

        [DataMember]
        public long perilousPoints
        {
            get; set;
        }

        [DataMember]
        public Profile Profile
        {
            get; set;
        }

        /// <summary>
        /// This will most likely be a big json
        /// </summary>
        [DataMember]
        public string meta
        {
            get; set;
        }

        [DataMember]
        public string role
        {
            get; set;
        }

        [DataMember]
        public System.TimeSpan timeOnline
        {
            get; set;
        }

        [DataMember]
        public string token { get; set; }

        [DataMember]
        public System.DateTime tokenExpiration { get; set; }

        /// <summary>
        /// unique userId generated when creating account
        /// </summary>
        [BsonId]
        [DataMember]
        public string userId
        {
            get; set;
        }

        /// <summary>
        /// This is the google user
        /// </summary>
        [DataMember]
        public Google.Integrations.UserProfile UserProfile
        {
            get; set;
        }
    }
}