// Copyright © Mandala Consulting, LLC., 2024. All Rights Reserved. Created by Alexander Fields https://www.alexanderfields.me on 2024-01-16 19:19:01
// Edited by Alexander Fields https://www.alexanderfields.me 2025-07-02 11:48:25
//Created by Alexander Fields
using MandalaConsulting.Objects.Billing;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Linq;

namespace MandalaConsulting.Objects.Account
{
    [BsonIgnoreExtraElements]
    /// <summary>
    /// Represents a user profile with additional account settings, inheriting basic contact information.
    /// </summary>
    public class Profile : Contact
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Profile"/> class.
        /// </summary>
        public Profile()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Profile"/> class with specified properties.
        /// </summary>
        /// <param name="accountSetting">JSON object containing user account settings.</param>
        /// <param name="dob">Date of birth.</param>
        /// <param name="firstName">First name.</param>
        /// <param name="lastName">Last name.</param>
        /// <param name="middleName">Middle name.</param>
        /// <param name="number">Contact number.</param>
        public Profile(
            JObject accountSetting,
            System.DateTime dob,
            string firstName,
            string lastName,
            string middleName,
            ulong number
        )
            : base(dob, firstName, lastName, middleName, number)
        {
            this.accountSettings = accountSetting;
        }

        [BsonSerializer(typeof(JObjectSerializer))]
        /// <summary>
        /// Gets or sets the JSON object containing user account settings.
        /// </summary>
        public JObject accountSettings { get; set; }
    }
}
