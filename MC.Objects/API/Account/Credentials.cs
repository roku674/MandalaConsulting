// Copyright © Mandala Consulting, LLC., 2024. All Rights Reserved. Created by Alexander Fields https://www.alexanderfields.me on 2024-01-16 19:19:01
// Edited by Alexander Fields https://www.alexanderfields.me 2025-07-02 11:48:25
//Created by Alexander Fields

namespace MandalaConsulting.Objects.Account
{
    /// <summary>
    /// Represents user authentication credentials.
    /// </summary>
    public class Credentials
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Credentials"/> class.
        /// </summary>
        public Credentials()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Credentials"/> class with specified properties.
        /// </summary>
        /// <param name="email">User's email address.</param>
        /// <param name="password">User's password.</param>
        /// <param name="token">Authentication token.</param>
        /// <param name="googleToken">Google authentication token.</param>
        public Credentials(string email, string password, string token, string googleToken)
        {
            this.email = email ?? throw new System.ArgumentNullException(nameof(email));
            this.password = password ?? throw new System.ArgumentNullException(nameof(password));
            this.token = token;
            this.googleToken = token;
        }

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// Gets or sets the Google authentication token.
        /// </summary>
        public string googleToken { get; set; }
        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// Gets or sets the authentication token.
        /// </summary>
        public string token { get; set; }
    }
}
