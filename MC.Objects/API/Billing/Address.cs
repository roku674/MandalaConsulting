//Copyright © 2023 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields

using System.Runtime.Serialization;

namespace MandalaConsulting.Objects.Billing
{
    [System.Serializable]
    public class Address
    {
        public Address()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="city">!nullable</param>
        /// <param name="country">!nullable</param>
        /// <param name="name"></param>
        /// <param name="contact">!nullable</param>
        /// <param name="state">!nullable</param>
        /// <param name="street">!nullable</param>
        /// <param name="zip"></param>
        public Address(string city, string country, string name, Contact contact, string state, string street, int zip)
        {
            this.city = city ?? throw new System.ArgumentNullException(nameof(city));
            this.country = country ?? throw new System.ArgumentNullException(nameof(country));
            this.name = name;
            this.contact = contact ?? throw new System.ArgumentNullException(nameof(contact));
            this.state = state ?? throw new System.ArgumentNullException(nameof(state));
            this.street = street ?? throw new System.ArgumentNullException(nameof(street));
            this.zip = zip;
        }

        /// <summary>
        /// Do they put on for their...
        /// </summary>
        [DataMember]
        public string city {
            get; set;
        }

        /// <summary>
        /// Their Country/State (not to be confused with individual states in U.S.)
        /// </summary>
        [DataMember]
        public string country {
            get; set;
        }

        /// <summary>
        /// This isn't required if sending to a person
        /// </summary>
        [DataMember]
        public string name {
            get; set;
        }

        [DataMember]
        public Contact contact {
            get; set;
        }

        /// <summary>
        /// State/Province
        /// </summary>
        [DataMember]
        public string state {
            get; set;
        }

        /// <summary>
        /// street address
        /// </summary>
        [DataMember]
        public string street {
            get; set;
        }

        /// <summary>
        /// zip code
        /// </summary>
        [DataMember]
        public int zip {
            get;
        }
    }
}