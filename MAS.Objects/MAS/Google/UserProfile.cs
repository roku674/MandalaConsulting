using Newtonsoft.Json;

namespace Google.Integrations
{
    //* JsonProperty(PropertyName = "name") attribute used here
    //* to map json property name with object property name
    //* while deseralizing a json into object
    public partial class UserProfile
    {
        [JsonProperty("id")]
        public string id {
            get; set;
        }

        [JsonProperty("email")]
        public string email {
            get; set;
        }

        [JsonProperty("verified_email")]
        public bool verifiedEmail {
            get; set;
        }

        [JsonProperty("name")]
        public string name {
            get; set;
        }

        [JsonProperty("given_name")]
        public string givenName {
            get; set;
        }

        [JsonProperty("family_name")]
        public string familyName {
            get; set;
        }

        [JsonProperty("link")]
        public string link {
            get; set;
        }

        [JsonProperty("picture")]
        public string picture {
            get; set;
        }

        [JsonProperty("gender")]
        public string gender {
            get; set;
        }

        [JsonProperty("locale")]
        public string locale {
            get; set;
        }
    }
}