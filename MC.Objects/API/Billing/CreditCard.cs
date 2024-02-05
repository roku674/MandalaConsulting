//Copyright © 2023 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields

namespace MAS.Objects.Billing
{
    public class CreditCard
    {
        public string CardNumber {
            get; set;
        }

        public string CardHolderName {
            get; set;
        }

        public System.DateTime ExpirationDate {
            get; set;
        }

        public int CVV {
            get; set;
        }

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

        public bool IsValid()
        {
            return System.DateTime.Now < ExpirationDate;
        }

        // Add other methods as needed, e.g. for validating card numbers, checking card types, etc.
    }
}