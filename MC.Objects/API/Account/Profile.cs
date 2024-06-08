//Copyright © 2023 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields

using System.Collections.Generic;
using MC.Objects.Billing;

namespace MC.Objects.Account
{
    public class Profile : Contact
    {
        public Profile()
        {
        }

        public Profile(Dictionary<string, object> accountSetting, System.DateTime dob, string firstName, string lastName, string middleName, ulong number) : base(dob, firstName, lastName, middleName, number)
        {
            this.accountSettings = accountSettings;
        }

        /// <summary>
        /// This will be served as a json essnetially meta data
        /// </summary>
        public Dictionary<string, object> accountSettings {
            get; set;
        }
      
    }

}
