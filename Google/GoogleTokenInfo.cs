// Copyright Mandala Consulting, LLC., 2024. All Rights Reserved. Created by Alexander Fields https://www.alexanderfields.me on 2024-02-03 13:40:00
// Edited by Alexander Fields https://www.alexanderfields.me 2024-10-12 14:24:21
//Created by Alexander Fields

using Newtonsoft.Json;

namespace Google.Integrations
{
    public class GoogleTokenInfo
    {
        [JsonProperty("access_token")]
        public string accessToken { get; set; }

        [JsonProperty("token_type")]
        public string tokenType { get; set; }

        [JsonProperty("expires_in")]
        public long expiresIn { get; set; }

        [JsonProperty("id_token")]
        public string idToken { get; set; }

        [JsonProperty("azp")]
        public string azp { get; set; }

        [JsonProperty("aud")]
        public string aud { get; set; }

        [JsonProperty("sub")]
        public string sub { get; set; }

        [JsonProperty("scope")]
        public string scope { get; set; }

        [JsonProperty("exp")]
        public long exp { get; set; }

        [JsonProperty("expires_in")]
        public long expires_in { get; set; }

        [JsonProperty("email")]
        public string email { get; set; }

        [JsonProperty("email_verified")]
        public string emailVerified { get; set; }

        [JsonProperty("access_type")]
        public string accessType { get; set; }

        // Parameterless constructor
        public GoogleTokenInfo()
        {
        }

        // Constructor with all properties
        public GoogleTokenInfo(string azp, string aud, string sub, string scope, long exp, long expires_in, string email, string emailVerified, string accessType)
        {
            this.azp = azp;
            this.aud = aud;
            this.sub = sub;
            this.scope = scope;
            this.exp = exp;
            this.expires_in = expires_in;
            this.email = email;
            this.emailVerified = emailVerified;
            this.accessType = accessType;
        }
    }
}
