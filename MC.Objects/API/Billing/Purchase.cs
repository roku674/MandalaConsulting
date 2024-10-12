//Copyright © 2023 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields

using System.Runtime.Serialization;

namespace MandalaConsulting.Objects.Billing
{
    [System.Serializable]
    public class Purchase
    {
        public Purchase()
        {
            this.transactionId = System.Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Purchase Constructor
        /// </summary>
        /// <param name="billing">!nullable</param>
        /// <param name="shipping">
        /// if null will assign billing addy : For all intents and purposes this is the recipient
        /// </param>
        /// <param name="company">
        /// !nullable | This is the company the client has purchased from
        /// </param>
        /// <param name="discount"></param>
        /// <param name="discountPerc"></param>
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
        public Purchase(Address billing, Address shipping, string company, decimal discount, decimal discountPerc, decimal donation, bool isRefund, Product item,
            PaymentType paymentType, Contact purchaser, string taxLocation, decimal taxPercentage, System.DateTime time, decimal tip)
        {
            AddressBilling = billing ?? throw new System.ArgumentNullException(nameof(billing));
            AddressShipping = shipping ?? billing;
            this.company = company ?? throw new System.ArgumentNullException(nameof(company));
            this.discount = discount;
            this.discountPerc = discountPerc <= 0 ? this.discountPerc = 1 : this.discountPerc = discountPerc;
            this.donation = donation;
            transactionId = System.Guid.NewGuid().ToString();
            this.isRefund = isRefund;
            this.Item = item ?? throw new System.ArgumentNullException(nameof(item));
            this.paymentType = paymentType;
            this.Purchaser = purchaser ?? throw new System.ArgumentNullException(nameof(purchaser));
            this.taxLocation = taxLocation;
            this.taxPercentage = taxPercentage <= 0 ? this.taxPercentage = 1 : this.taxPercentage = taxPercentage;
            decimal weightedPrice = (Item.Cost + (this.Item.Price * this.Item.QuantitySold));
            this.taxAmount = weightedPrice * (this.taxPercentage / 100);
            this.time = time != default ? time : System.DateTime.Now;
            this.tip = tip;
            this.total = weightedPrice + this.taxAmount + this.tip + this.donation;
        }

        [DataMember]
        public Address AddressBilling {
            get; set;
        }

        /// <summary>
        /// For all intents and purposes this is the recipient
        /// </summary>
        [DataMember]
        public Address AddressShipping {
            get; set;
        }

        [DataMember]
        public string company {
            get; set;
        }

        /// <summary>
        /// Flat discount rate
        /// </summary>
        [DataMember]
        public decimal discount {
            get; set;
        }

        /// <summary>
        /// Percentage of a discount
        /// </summary>
        [DataMember]
        public decimal discountPerc {
            get; set;
        }

        [DataMember]
        public decimal donation {
            get; set;
        }

        [DataMember]
        public bool isGift {
            get; set;
        }

        [DataMember]
        public bool isRefund {
            get; set;
        }

        /// <summary>
        /// This is the name of the object(s) or service(s) they just purchased
        /// </summary>
        [DataMember]
        public Product Item {
            get; set;
        }

        [DataMember]
        public PaymentType paymentType {
            get; set;
        }

        [DataMember]
        public Contact Purchaser {
            get; set;
        }

        /// <summary>
        /// Amount of money that went to taxes
        /// </summary>
        [DataMember]
        public decimal taxAmount {
            get; set;
        }

        /// <summary>
        /// State/Province for tax purposes
        /// </summary>
        [DataMember]
        public string taxLocation {
            get; set;
        }

        /// <summary>
        /// Sale's tax percentage
        /// </summary>
        [DataMember]
        public decimal taxPercentage {
            get; set;
        }

        /// <summary>
        /// Time of purchase
        /// </summary>
        [DataMember]
        public System.DateTime time {
            get; set;
        }

        [DataMember]
        public decimal tip {
            get; set;
        }

        /// <summary>
        /// Total amount after cost, taxes and possibly tip?, donation?
        /// </summary>
        [DataMember]
        public decimal total {
            get; set;
        }

        [DataMember]
        public string transactionId {
            get; set;
        }
    }
}