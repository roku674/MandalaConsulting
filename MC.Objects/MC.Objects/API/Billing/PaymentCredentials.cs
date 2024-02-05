//Copyright © 2023 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields

namespace MAS.Objects.Billing
{
    public class PaymentCredentials
    {
        public PaymentCredentials()
        {
        }

        public PaymentCredentials(PaymentType paymentType)
        {
            PaymentType = paymentType;
        }

        public CreditCard CreditCard
        {
            get; set;
        }

        public PayeeInfo PayeeInfo
        {
            get; set;
        }

        public PaymentType PaymentType
        {
            get; set;
        }
    }
}