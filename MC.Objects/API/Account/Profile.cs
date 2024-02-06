//Copyright © 2023 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields

using MC.Objects.Billing;

namespace MC.Objects.Account
{
    public class Profile : Contact
    {
        public Profile()
        {
        }

        public Profile(string accountSetting, System.DateTime dob, string firstName, string lastName, string middleName, ulong number) : base(dob, firstName, lastName, middleName, number)
        {
            this.accountSettings = accountSettings ?? string.Empty;
        }

        /// <summary>
        /// This will be served as a json essnetially meta data
        /// </summary>
        public string accountSettings {
            get; set;
        }
      
    }

}
