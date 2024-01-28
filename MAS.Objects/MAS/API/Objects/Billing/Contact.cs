//Copyright © 2023 Monotonous Automation Solutions All rights reserved.

using System.Runtime.Serialization;

namespace MAS.Objects.Billing
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
        public System.DateTime dob {
            get; set;
        }

        [DataMember]
        public string firstName {
            get; set;
        }

        [DataMember]
        public string lastName {
            get; set;
        }

        [DataMember]
        public string middleName {
            get; set;
        }

        /// <summary>
        /// This is their phone number if they have one
        /// </summary>
        [DataMember]
        public ulong number {
            get; set;
        }
    }
}