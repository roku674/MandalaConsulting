using Newtonsoft.Json;

/*{
    "azp": "",
    "aud": "",
    "sub": "",
    "scope": "https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/userinfo.profile openid",
    "exp": "",
    "expires_in": "",
    "email": "",
    "email_verified": "",
    "access_type": ""
}*/

namespace Google.Integrations
{
    public class GmailToken
    {
        [JsonProperty("access_token")]
        public string accessToken {
            get; set;
        }

        [JsonProperty("token_type")]
        public string tokenType {
            get; set;
        }

        [JsonProperty("expires_in")]
        public long expiresIn {
            get; set;
        }

        [JsonProperty("id_token")]
        public string idToken {
            get; set;
        }
    }
}