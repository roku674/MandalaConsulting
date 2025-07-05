// Copyright © Mandala Consulting, LLC., 2024. All Rights Reserved. Created by Alexander Fields https://www.alexanderfields.me on 2024-01-16 19:19:01
// Edited by Alexander Fields https://www.alexanderfields.me 2025-07-02 11:48:25
//Created by Alexander Fields

namespace MandalaConsulting.Objects.Billing
{
    /// <summary>
    /// Represents a credit card with payment information.
    /// </summary>
    public class CreditCard
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreditCard"/> class with specified properties.
        /// </summary>
        /// <param name="cardNumber">The credit card number.</param>
        /// <param name="cardHolderName">The name of the card holder.</param>
        /// <param name="expirationDate">The card's expiration date.</param>
        /// <param name="cvv">The card's security code (CVV).</param>
        public CreditCard(string cardNumber, string cardHolderName, System.DateTime expirationDate, int cvv)
        {
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            ExpirationDate = expirationDate;
            CVV = cvv;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreditCard"/> class.
        /// </summary>
        public CreditCard()
        {
        }

        /// <summary>
        /// Gets or sets the name of the card holder.
        /// </summary>
        public string CardHolderName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the credit card number.
        /// </summary>
        public string CardNumber
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the card's security code (CVV).
        /// </summary>
        public int CVV
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the card's expiration date.
        /// </summary>
        public System.DateTime ExpirationDate
        {
            get; set;
        }

        /// <summary>
        /// Checks if the credit card is still valid based on its expiration date.
        /// </summary>
        /// <returns>True if the card has not expired, false otherwise.</returns>
        public bool IsValid()
        {
            return System.DateTime.Now < ExpirationDate;
        }

        // Add other methods as needed, e.g. for validating card numbers, checking card types, etc.
    }
}
