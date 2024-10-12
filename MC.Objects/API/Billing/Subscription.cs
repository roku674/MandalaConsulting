//Copyright © 2023 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields

using System.Runtime.Serialization;

namespace MandalaConsulting.Objects.Billing
{
    [System.Serializable]
    public class Subscription : Purchase
    {
        public Subscription()
        {
        }

        /// <summary>
        /// Subscription Constructor
        /// </summary>
        /// <param name="billing">!nullable</param>
        /// <param name="shipping">if null will assign billing addy</param>
        /// <param name="company">
        /// !nullable | This is the company the client has purchased from
        /// </param>
        /// <param name="donation"></param>
        /// <param name="isRefund"></param>
        /// <param name="item">!nullable</param>
        /// <param name="purchaser">!nullable</param>
        /// <param name="paymentType"></param>
        /// <param name="taxAmount"></param>
        /// <param name="taxLocation"></param>
        /// <param name="taxPercentage"></param>
        /// <param name="time">if null will assign now</param>
        /// <param name="tip"></param>
        /// <param name="total"></param>
        public Subscription(Address billing, Address shipping, bool billedMonthly, bool billedYearly, string company, decimal donation, bool isRefund, bool isActive, bool isRecurring, Product item,
            PaymentType paymentType, Contact purchaser, System.DateTime renewalTime, string taxLocation, decimal taxPercentage, System.DateTime time, decimal tip, string typeOfSubscription)
        {
            AddressBilling = billing ?? throw new System.ArgumentNullException(nameof(billing));
            AddressShipping = shipping != null ? shipping : billing;
            this.company = company ?? throw new System.ArgumentNullException(nameof(company));
            this.isBilledMonthly = billedMonthly;
            this.isBilledYearly = billedYearly;
            this.donation = donation;
            transactionId = System.Guid.NewGuid().ToString();
            this.isRefund = isRefund;
            this.isActive = isActive;
            this.isRecurring = isRecurring;
            this.Item = item ?? throw new System.ArgumentNullException(nameof(item));
            this.paymentType = paymentType;
            this.Purchaser = purchaser ?? throw new System.ArgumentNullException(nameof(purchaser));
            this.renewalTime = renewalTime != default ? renewalTime : System.DateTime.Now;
            this.taxLocation = taxLocation;
            this.taxPercentage = taxPercentage <= 0 ? this.taxPercentage = 1 : this.taxPercentage = taxPercentage;
            decimal weightedPrice = (Item.Cost + (this.Item.Price * this.Item.QuantitySold));
            decimal taxAmount = weightedPrice * (this.taxPercentage / 100);
            this.time = time != default ? time : System.DateTime.Now;
            this.tip = tip;
            this.total = weightedPrice + this.taxAmount + this.tip + this.donation;
            this.typeOfSubscription = typeOfSubscription;
        }

        /// <summary>
        /// </summary>
        [DataMember]
        public bool isBilledMonthly {
            get; set;
        }

        /// <summary>
        /// </summary>
        [DataMember]
        public bool isBilledYearly {
            get; set;
        }

        [DataMember]
        public bool isActive {
            get; set;
        }

        [DataMember]
        public bool isRecurring {
            get; set;
        }

        [DataMember]
        public string typeOfSubscription {
            get; set;
        }

        [DataMember]
        public System.DateTime renewalTime {
            get; set;
        }
    }
}
