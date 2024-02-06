//Copyright © 2023 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields

using System.Runtime.Serialization;

namespace MC.Objects.Billing
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