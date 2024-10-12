//Copyright © 2023 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields
using MandalaConsulting.Objects.API.Billing;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Linq;

namespace MandalaConsulting.Objects.API.Account
{
    [BsonIgnoreExtraElements]
    public class Profile : Contact
    {
        public Profile() { }

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
        /// This will be served as a json essnetially meta data
        /// </summary>
        public JObject accountSettings { get; set; }
    }
}
