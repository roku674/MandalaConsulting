// Copyright © Mandala Consulting, LLC., 2024. All Rights Reserved. Created by Alexander Fields https://www.alexanderfields.me on 2024-01-16 19:19:01
// Edited by Alexander Fields https://www.alexanderfields.me 2025-07-02 11:48:25
//Created by Alexander Fields

namespace MandalaConsulting.Objects.Billing
{
    /// <summary>
    /// Defines the supported payment methods.
    /// </summary>
    public enum PaymentType : int
    {
        /// <summary>
        /// No payment type specified.
        /// </summary>
        None = 0,
        /// <summary>
        /// PayPal payment token.
        /// </summary>
        PayPalToken = 1,
        /// <summary>
        /// MasterCard credit card.
        /// </summary>
        MasterCard = 2,
        /// <summary>
        /// Visa credit card.
        /// </summary>
        Visa = 3,
        /// <summary>
        /// ACH (Automated Clearing House) bank transfer.
        /// </summary>
        ACH = 4
    }
}
