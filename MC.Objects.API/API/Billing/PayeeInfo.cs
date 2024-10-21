//Copyright © 2023 Mandala Consulting, LLC All rights reserved.
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