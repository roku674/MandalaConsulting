//Copyright © 2023 Monotonous Automation Solutions All rights reserved.
//Created by Alexander Fields

namespace MAS.Objects.Billing
{
    public class Sale
    {
        public string UID
        {
            get; set;
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
    }
}