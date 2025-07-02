// Copyright © Mandala Consulting, LLC., 2024. All Rights Reserved. Created by Alexander Fields https://www.alexanderfields.me on 2024-01-16 19:19:01
// Edited by Alexander Fields https://www.alexanderfields.me 2025-07-02 11:48:25
//Created by Alexander Fields

namespace MandalaConsulting.Objects.Account
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
