//Copyright © 2023 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MC.Objects.Billing
{
    [System.Serializable]
    public class Product
    {
        public Product()
        {
            this.QuantitySold = 1;
            this.Currency = "USD";
        }

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cost">Cost to produce product</param>
        /// <param name="currency">if null defaults to USD</param>
        /// <param name="price"></param>
        /// <param name="quantitySold"></param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Product(string name, decimal cost, string currency, decimal price, string productId, int quantitySold)
        {
            this.Cost = cost;
            this.Currency = currency ?? "USD";
            this.Price = price;
            this.ProductId = productId;
            this.QuantitySold = (quantitySold == 0) ? 1 : quantitySold;
            this.Name = name ?? throw new System.ArgumentNullException(nameof(name));
        }

        /// <summary>
        /// Cost to produce/acquire
        /// </summary>
        [DataMember]
        public decimal Cost {
            get; set;
        }

        // </summary>
        [DataMember]
        public string Currency {
            get; set;
        }

        /// <summary>
        /// Amount of money the purchase costs to customers
        /// </summary>
        [DataMember]
        public decimal Price {
            get; set;
        }

        [DataMember]
        public string ProductId {
            get; set;
        }

        [DataMember]
        public int QuantitySold {
            get; set;
        }

        /// <summary>
        /// Amount of money the purchase costs
        /// </summary>
        [DataMember]
        public string Name {
            get; set;
        }

        public static List<Product> GetTotalQuantitySold(List<Product> products, List<Sale> sales)
        {
            Dictionary<string, int> quantities = new Dictionary<string, int>();

            foreach (Sale sale in sales)
            {
                if (quantities.ContainsKey(sale.MenuItem))
                {
                    quantities[sale.MenuItem] += sale.SoldQuantity;
                }
                else
                {
                    quantities.Add(sale.MenuItem, sale.SoldQuantity);
                }
            }

            foreach (Product product in products)
            {
                if (quantities.TryGetValue(product.Name, out int soldQuantity))
                {
                    product.QuantitySold = soldQuantity;
                }
                else
                {
                    product.QuantitySold = 0;
                }
            }

            return products;
        }
    }
}