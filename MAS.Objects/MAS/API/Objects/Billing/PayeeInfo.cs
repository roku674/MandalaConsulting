//Copyright © 2023 Perilous Games, Ltd. All rights reserved.

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

        [DataMember(Name = "client_id", EmitDefaultValue = false)]
        public string ClientId;

        [DataMember(Name = "email_address", EmitDefaultValue = false)]
        public string Email;

        [DataMember(Name = "merchant_id", EmitDefaultValue = false)]
        public string MerchantId;

    }
}