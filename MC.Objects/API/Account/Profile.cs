// Copyright © Mandala Consulting, LLC., 2024. All Rights Reserved. Created by Alexander Fields https://www.alexanderfields.me on 2024-01-16 19:19:01
// Edited by Alexander Fields https://www.alexanderfields.me 2025-07-02 11:48:25
//Created by Alexander Fields
using MandalaConsulting.Objects.Billing;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Linq;

namespace MandalaConsulting.Objects.Account
{
    [BsonIgnoreExtraElements]
    public class Profile : Contact
    {
        public Profile()
        { }

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
        public JObject accountSettings { get; set; }
    }
}
