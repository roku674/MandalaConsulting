// Copyright Mandala Consulting, LLC., 2024. All Rights Reserved. Created by Alexander Fields https://www.alexanderfields.me on 2024-02-03 13:40:00
// Edited by Alexander Fields https://www.alexanderfields.me 2024-10-12 14:24:21
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
