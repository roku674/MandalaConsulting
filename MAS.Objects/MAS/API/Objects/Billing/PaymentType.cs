//Copyright © 2023 Perilous Games, Ltd. All rights reserved.

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