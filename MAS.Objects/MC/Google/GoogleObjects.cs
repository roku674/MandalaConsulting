//Copyright © 2023 Mandala Consulting, LLC All rights reserved.

using Google.Integrations;

namespace MAS.Objects.Google
{
    public class GoogleObjects
    {
        public GoogleObjects()
        {

        }

        public GoogleObjects(GoogleTokenInfo googleTokenInfo, UserProfile userProfile)
        {
            GoogleTokenInfo = googleTokenInfo;
            UserProfile = userProfile;
        }

        public GoogleTokenInfo GoogleTokenInfo { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}
