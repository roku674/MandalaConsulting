// Copyright © Mandala Consulting, LLC., 2024. All Rights Reserved. Created by Alexander Fields https://www.alexanderfields.me on 2024-01-16 19:19:01
// Edited by Alexander Fields https://www.alexanderfields.me 2025-07-02 11:48:25
//Created by Alexander Fields

using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MandalaConsulting.Objects.Account
{
    [System.Serializable]
    /// <summary>
    /// Represents a user account with authentication and profile information.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class with specified properties.
        /// </summary>
        /// <param name="banned">Date when the ban is lifted.</param>
        /// <param name="createdDate">Date when the user account was created.</param>
        /// <param name="email">User's email address used for login.</param>
        /// <param name="emailConfirmed">Whether the email has been confirmed.</param>
        /// <param name="ipAddresses">List of IP addresses associated with this user.</param>
        /// <param name="lastActiveDate">Date of last user activity.</param>
        /// <param name="password">User's password.</param>
        /// <param name="profile">User's profile information.</param>
        /// <param name="role">User's role in the system.</param>
        /// <param name="timeOnline">Total time user has spent online.</param>
        /// <param name="userId">Unique identifier for the user.</param>
        /// <param name="username">User's display name.</param>
        /// <param name="token">Authentication token.</param>
        /// <param name="tokenExpiration">Expiration date of the authentication token.</param>
        public User(
            System.DateTime banned,
            System.DateTime createdDate,
            string email,
            bool emailConfirmed,
            List<IPInfo> ipAddresses,
            System.DateTime lastActiveDate,
            string password,
            Profile profile,
            string role,
            System.TimeSpan timeOnline,
            string userId,
            string username,
            string token,
            System.DateTime tokenExpiration
        )
        {
            this.banned = banned != default ? banned : new System.DateTime(1900, 1, 1);
            this.createdDate = createdDate != default ? createdDate : System.DateTime.Now;
            this.email = email;
            this.emailConfirmed = emailConfirmed;
            this.IPAdresses = ipAddresses ?? new List<IPInfo>();
            this.lastActiveDate = lastActiveDate != default ? lastActiveDate : System.DateTime.Now;
            this.password = password ?? throw new System.ArgumentNullException(nameof(password));
            this.Profile = profile ?? new Profile();
            this.role = role;
            this.timeOnline = timeOnline;
            this.userId = userId;
            this.username = username;
            this.token = token;
            this.tokenExpiration = tokenExpiration;
            //this.GoogleObjects = googleObjects;
        }

        /// <summary>
        /// Datetime in which the ban is lifted (set this in the past if they're not banned)
        /// </summary>
        [DataMember]
        public System.DateTime banned { get; set; }

        [DataMember]
        /// <summary>
        /// Gets or sets the date when the user account was created.
        /// </summary>
        public System.DateTime createdDate { get; set; }

        /// <summary>
        /// email serves as their login name
        /// </summary>
        [DataMember]
        public string email { get; set; }

        [DataMember]
        /// <summary>
        /// Gets or sets a value indicating whether the user's email has been confirmed.
        /// </summary>
        public bool emailConfirmed { get; set; }

        [DataMember]
        /// <summary>
        /// Gets or sets the list of IP addresses associated with this user's account.
        /// </summary>
        public List<IPInfo> IPAdresses { get; set; }

        [DataMember]
        /// <summary>
        /// Gets or sets the date of the user's last activity.
        /// </summary>
        public System.DateTime lastActiveDate { get; set; }

        /// <summary>
        /// This will most likely be a big json
        /// </summary>
        [DataMember]
        public string meta { get; set; }

        [DataMember]
        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        public string password { get; set; }

        [DataMember]
        /// <summary>
        /// Gets or sets the user's profile information.
        /// </summary>
        public Profile Profile { get; set; }

        [DataMember]
        /// <summary>
        /// Gets or sets the user's role in the system.
        /// </summary>
        public string role { get; set; }

        [DataMember]
        /// <summary>
        /// Gets or sets the total time the user has spent online.
        /// </summary>
        public System.TimeSpan timeOnline { get; set; }

        [DataMember]
        /// <summary>
        /// Gets or sets the user's authentication token.
        /// </summary>
        public string token { get; set; }

        [DataMember]
        /// <summary>
        /// Gets or sets the expiration date of the authentication token.
        /// </summary>
        public System.DateTime tokenExpiration { get; set; }

        /// <summary>
        /// unique userId generated when creating account
        /// </summary>
        [BsonId]
        [DataMember]
        public string userId { get; set; }

        [DataMember]
        /// <summary>
        /// Gets or sets the user's display name.
        /// </summary>
        public string username { get; set; }
    }
}
