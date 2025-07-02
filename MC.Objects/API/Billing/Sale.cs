// Copyright Mandala Consulting, LLC., 2024. All Rights Reserved. Created by Alexander Fields https://www.alexanderfields.me on 2024-01-16 19:19:01
// Edited by Alexander Fields https://www.alexanderfields.me 2024-11-17 17:30:13
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
