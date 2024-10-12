//Copyright © 2023 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields

using Google.Integrations;

namespace MandalaConsulting.Objects.Google
{
    public class GoogleObjects
    {
        public GoogleObjects()
        {

        }

        public GoogleObjects(GoogleTokenInfo googleTokenInfo, UserProfile userProfile)
        {
            GoogleTokenInfo = googleTokenInfo ?? new GoogleTokenInfo();
            UserProfile = userProfile ?? new UserProfile();
        }

        public GoogleTokenInfo GoogleTokenInfo { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}
