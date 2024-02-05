//Copyright © 2023 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields

namespace MAS.Objects.Billing
{
    public enum PaymentType : int
    {
        None = 0,
        PayPalToken = 1,
        MasterCard = 2,
        Visa = 3,
        ACH = 4
    }
}