// Copyright © Mandala Consulting, LLC., 2024. All Rights Reserved. Created by Alexander Fields https://www.alexanderfields.me on 2024-01-16 19:19:01
// Edited by Alexander Fields https://www.alexanderfields.me 2025-07-02 11:48:25
//Created by Alexander Fields

using System.Runtime.Serialization;

namespace MandalaConsulting.Objects.API.Billing
{
    public class PayeeInfo
    {
        [DataMember(Name = "client_id")]
        public string ClientId;

        [DataMember(Name = "email_address")]
        public string Email;

        [DataMember(Name = "merchant_id")]
        public string MerchantId;

        public PayeeInfo()
        {
        }

        public PayeeInfo(string clientId, string email, string merchantId)
        {
            ClientId = clientId;
            Email = email;
            MerchantId = merchantId;
        }
    }
}
