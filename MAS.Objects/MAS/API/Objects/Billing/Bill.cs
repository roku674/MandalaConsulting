//Copyright © 2023 Mandala Consulting, LLC All rights reserved.

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MAS.Objects.Billing
{
    public class Bill
    {
        public Bill()
        {
        }

        public Bill(Address addressBilling, List<string> orders, PaymentCredentials paymentTypes, List<Purchase> purchases, List<Purchase> refunds, List<Subscription> subscriptions)
        {
            this.AdressBilling = addressBilling ?? new Address();
            this.Orders = orders ?? new List<string>();
            this.PaymentTypesOnFile = paymentTypes ?? new PaymentCredentials();
            this.Purchases = purchases ?? new List<Purchase>();
            this.Refunds = refunds ?? new List<Purchase>();
            this.Subscriptions = subscriptions ?? new List<Subscription>();
        }

        /// <summary>
        /// Keep a billign address on file
        /// </summary>
        [DataMember]
        public Address AdressBilling {
            get; set;
        }

        [DataMember]
        public List<string> Orders {
            get; set;
        }

        /// <summary>
        /// Credit cards, billing, etc
        /// </summary>
        [DataMember]
        public PaymentCredentials PaymentTypesOnFile {
            get; set;
        }

        /// <summary>
        /// All purchases made
        /// </summary>
        [DataMember]
        public List<Purchase> Purchases {
            get; set;
        }

        /// <summary>
        /// Purchases that have been refunded
        /// </summary>
        [DataMember]
        public List<Purchase> Refunds {
            get; set;
        }

        /// <summary>
        /// Subscriptions
        /// </summary>
        [DataMember]
        public List<Subscription> Subscriptions {
            get; set;
        }
        public dynamic GetPrimaryPayment()
        {
            if (PaymentTypesOnFile.CreditCard != null)
            {
                return PaymentTypesOnFile.CreditCard;
            }
            else if (PaymentTypesOnFile.PayeeInfo != null)
            {
                return PaymentTypesOnFile.PayeeInfo;
            }
            else
                return null;
        }
    }
}