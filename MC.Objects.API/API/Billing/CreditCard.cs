//Copyright © 2023 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields

namespace MandalaConsulting.Objects.API.Billing
{
    public class CreditCard
    {
        public CreditCard(string cardNumber, string cardHolderName, System.DateTime expirationDate, int cvv)
        {
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            ExpirationDate = expirationDate;
            CVV = cvv;
        }

        public CreditCard()
        {
        }

        public string CardHolderName
        {
            get; set;
        }

        public string CardNumber
        {
            get; set;
        }

        public int CVV
        {
            get; set;
        }

        public System.DateTime ExpirationDate
        {
            get; set;
        }

        public bool IsValid()
        {
            return System.DateTime.Now < ExpirationDate;
        }

        // Add other methods as needed, e.g. for validating card numbers, checking card types, etc.
    }
}