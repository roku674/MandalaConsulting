//Copyright © 2023 Mandala Consulting, LLC All rights reserved.

using System.Runtime.Serialization;

namespace MAS.Objects.Billing
{
    public class PayeeInfo
    {
        public PayeeInfo()
        {
        }

        public PayeeInfo(string clientId, string email, string merchantId)
        {
            ClientId = clientId;
            Email = email;
            MerchantId = merchantId;
        }

        [DataMember(Name = "client_id")]
        public string ClientId;

        [DataMember(Name = "email_address")]
        public string Email;

        [DataMember(Name = "merchant_id")]
        public string MerchantId;

    }
}