// Copyright © Mandala Consulting, LLC., 2024. All Rights Reserved. Created by Alexander Fields https://www.alexanderfields.me on 2024-02-03 13:40:00
// Edited by Alexander Fields https://www.alexanderfields.me 2025-07-02 11:48:25
//Created by Alexander Fields

using Google.Integrations;

namespace MandalaConsulting.Objects.Google
{
    /// <summary>
    /// Container class for Google API integration objects.
    /// </summary>
    public class GoogleObjects
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleObjects"/> class.
        /// </summary>
        public GoogleObjects()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleObjects"/> class with specified properties.
        /// </summary>
        /// <param name="googleTokenInfo">Google token information.</param>
        /// <param name="userProfile">User's Google profile information.</param>
        public GoogleObjects(GoogleTokenInfo googleTokenInfo, UserProfile userProfile)
        {
            GoogleTokenInfo = googleTokenInfo ?? new GoogleTokenInfo();
            UserProfile = userProfile ?? new UserProfile();
        }

        /// <summary>
        /// Gets or sets the Google token information.
        /// </summary>
        public GoogleTokenInfo GoogleTokenInfo { get; set; }

        /// <summary>
        /// Gets or sets the user's Google profile information.
        /// </summary>
        public UserProfile UserProfile { get; set; }
    }
}
