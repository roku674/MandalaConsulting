﻿//Copyright © 2023 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields

namespace MandalaConsulting.Objects.Billing
{
    public class Sale
    {
        public Sale(string UID, System.DateTime date, string menuItem, int soldQuantity, decimal totalSale)
        {
            this.UID = UID;
            Date = date;
            MenuItem = menuItem;
            SoldQuantity = soldQuantity;
            TotalSale = totalSale;
        }

        public Sale()
        {
        }

        public System.DateTime Date
        {
            get; set;
        }

        public string MenuItem
        {
            get; set;
        }

        public int SoldQuantity
        {
            get; set;
        }

        public decimal TotalSale
        {
            get; set;
        }

        public string UID
        {
            get; set;
        }
    }
}