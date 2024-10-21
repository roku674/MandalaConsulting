//Copyright © 2023 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields

namespace MandalaConsulting.Objects.API.Account
{
    public class Credentials
    {
        public Credentials()
        {
        }

        public Credentials(string email, string password, string token, string googleToken)
        {
            this.email = email ?? throw new System.ArgumentNullException(nameof(email));
            this.password = password ?? throw new System.ArgumentNullException(nameof(password));
            this.token = token;
            this.googleToken = token;
        }

        public string email { get; set; }
        public string googleToken { get; set; }
        public string password { get; set; }
        public string token { get; set; }
    }
}