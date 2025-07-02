// Copyright © Mandala Consulting, LLC., 2024. All Rights Reserved. Created by Alexander Fields https://www.alexanderfields.me on 2024-01-16 19:19:01
// Edited by Alexander Fields https://www.alexanderfields.me 2025-07-02 11:48:25
//Created by Alexander Fields

using System.Runtime.Serialization;

namespace MandalaConsulting.Objects.Billing
{
    [System.Serializable]
    public class Contact
    {
        public Contact()
        {
        }

        /// <summary>
        /// Construct their name
        /// </summary>
        /// <param name="firstName">!nullable</param>
        /// <param name="lastName">!nullable</param>
        /// <param name="middleName"></param>
        /// <param name="number"></param>
        public Contact(System.DateTime dob, string firstName, string lastName, string middleName, ulong number)
        {
            this.dob = dob != default ? dob : new System.DateTime(1900, 1, 1);
            this.firstName = firstName ?? "";
            this.lastName = lastName ?? "";
            this.middleName = middleName;
            this.number = number;
        }

        [DataMember]
        public System.DateTime dob
        {
            get; set;
        }

        [DataMember]
        public string firstName
        {
            get; set;
        }

        [DataMember]
        public string lastName
        {
            get; set;
        }

        [DataMember]
        public string middleName
        {
            get; set;
        }

        /// <summary>
        /// This is their phone number if they have one
        /// </summary>
        [DataMember]
        public ulong number
        {
            get; set;
        }
    }
}
