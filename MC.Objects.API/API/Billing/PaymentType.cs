// Copyright Mandala Consulting, LLC., 2024. All Rights Reserved. Created by Alexander Fields https://www.alexanderfields.me on 2024-01-16 19:19:01
// Edited by Alexander Fields https://www.alexanderfields.me 2024-10-12 14:24:21
//Created by Alexander Fields

namespace MandalaConsulting.Objects.API.Billing
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
