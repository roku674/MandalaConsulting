// Copyright © Mandala Consulting, LLC., 2024. All Rights Reserved. Created by Alexander Fields https://www.alexanderfields.me on 2024-01-16 19:19:01
// Edited by Alexander Fields https://www.alexanderfields.me 2025-07-02 11:48:25
//Created by Alexander Fields

using System.Runtime.Serialization;

namespace MandalaConsulting.Objects.Billing
{
    [System.Serializable]
    /// <summary>
    /// Represents a subscription purchase with recurring billing details.
    /// Inherits from Purchase to include base purchase information.
    /// </summary>
    public class Subscription : Purchase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Subscription"/> class.
        /// </summary>
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
        /// <summary>
        /// Initializes a new instance of the <see cref="Subscription"/> class with specified properties.
        /// </summary>
        /// <param name="billing">The billing address (required).</param>
        /// <param name="shipping">The shipping address (defaults to billing address if null).</param>
        /// <param name="billedMonthly">Whether the subscription is billed monthly.</param>
        /// <param name="billedYearly">Whether the subscription is billed yearly.</param>
        /// <param name="company">The company providing the subscription (required).</param>
        /// <param name="donation">Any additional donation amount.</param>
        /// <param name="isRefund">Whether this is a refund transaction.</param>
        /// <param name="isActive">Whether the subscription is currently active.</param>
        /// <param name="isRecurring">Whether the subscription automatically renews.</param>
        /// <param name="item">The subscribed product or service (required).</param>
        /// <param name="paymentType">The type of payment used.</param>
        /// <param name="purchaser">The subscriber's contact information (required).</param>
        /// <param name="renewalTime">The next renewal date (defaults to current time).</param>
        /// <param name="taxLocation">The tax jurisdiction.</param>
        /// <param name="taxPercentage">The tax rate percentage.</param>
        /// <param name="time">The subscription start time (defaults to current time).</param>
        /// <param name="tip">Any additional tip amount.</param>
        /// <param name="typeOfSubscription">The type or tier of subscription.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when required parameters are null.</exception>
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

        [DataMember]
        /// <summary>
        /// Gets or sets a value indicating whether the subscription is currently active.
        /// </summary>
        public bool isActive
        {
            get; set;
        }

        /// <summary>
        /// </summary>
        [DataMember]
        /// <summary>
        /// Gets or sets a value indicating whether the subscription is billed monthly.
        /// </summary>
        public bool isBilledMonthly
        {
            get; set;
        }

        /// <summary>
        /// </summary>
        [DataMember]
        /// <summary>
        /// Gets or sets a value indicating whether the subscription is billed yearly.
        /// </summary>
        public bool isBilledYearly
        {
            get; set;
        }

        [DataMember]
        /// <summary>
        /// Gets or sets a value indicating whether the subscription automatically renews.
        /// </summary>
        public bool isRecurring
        {
            get; set;
        }

        [DataMember]
        /// <summary>
        /// Gets or sets the next renewal date for the subscription.
        /// </summary>
        public System.DateTime renewalTime
        {
            get; set;
        }

        [DataMember]
        /// <summary>
        /// Gets or sets the type or tier of subscription.
        /// </summary>
        public string typeOfSubscription
        {
            get; set;
        }
    }
}
