//Copyright © 2023 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields

using Newtonsoft.Json;

namespace Google.Integrations
{
    //* JsonProperty(PropertyName = "name") attribute used here
    //* to map json property name with object property name
    //* while deseralizing a json into object
    public partial class UserProfile
    {
        public UserProfile()
        {
        }

        public UserProfile(string id, string email, bool verifiedEmail, string name, string givenName, string familyName, string link, string picture, string gender, string locale, string hd)
        {
            this.id = id;
            this.email = email;
            this.verifiedEmail = verifiedEmail;
            this.name = name;
            this.givenName = givenName;
            this.familyName = familyName;
            this.link = link;
            this.picture = picture;
            this.gender = gender;
            this.locale = locale;
            this.hd = hd;
        }

        [JsonProperty("id")]
        public string id
        {
            get; set;
        }

        [JsonProperty("email")]
        public string email
        {
            get; set;
        }

        [JsonProperty("verified_email")]
        public bool verifiedEmail
        {
            get; set;
        }

        [JsonProperty("name")]
        public string name
        {
            get; set;
        }

        [JsonProperty("given_name")]
        public string givenName
        {
            get; set;
        }

        [JsonProperty("family_name")]
        public string familyName
        {
            get; set;
        }

        [JsonProperty("link")]
        public string link
        {
            get; set;
        }

        [JsonProperty("picture")]
        public string picture
        {
            get; set;
        }

        [JsonProperty("gender")]
        public string gender
        {
            get; set;
        }

        [JsonProperty("locale")]
        public string locale
        {
            get; set;
        }
        [JsonProperty("hd")]
        public string hd { get; set; }
    }
}