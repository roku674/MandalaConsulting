//Copyright © 2023 Perilous Games, Ltd. All rights reserved.

namespace MAS.Objects.Account
{
    public class Credentials
    {
        public Credentials()
        {
        }

        public Credentials(string email, string password, string token)
        {
            this.email = email ?? throw new ArgumentNullException(nameof(email));
            this.password = password ?? throw new ArgumentNullException(nameof(password));
            this.token = token;
        }

        public string email {
            get; set;
        }
        public string password {
            get; set;
        }
        public string token { get; set; }
    }
}
