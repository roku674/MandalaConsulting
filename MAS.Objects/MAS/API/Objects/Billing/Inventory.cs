//Copyright © 2023 Perilous Games, Ltd. All rights reserved.
//Created by Alexander Fields

namespace MAS.Objects.Billing
{
    public class Inventory
    {
        public string UID
        {
            get; set;
        }

        public string Reorder
        {
            get; set;
        }

        public string ItemNo
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string Manufacturer
        {
            get; set;
        }

        public string Supplier
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public decimal CostPerItem
        {
            get; set;
        }

        public int StockQuantity
        {
            get; set;
        }

        public string InventoryValue
        {
            get; set;
        }

        public int ReorderLevel
        {
            get; set;
        }

        public int DaysPerReorder
        {
            get; set;
        }

        public int ItemReorderQuantity
        {
            get; set;
        }

        public bool ItemDiscontinued
        {
            get; set;
        }

        public Inventory(string UID, string reorder, string itemNo, string name, string manufacturer, string supplier, string description, decimal costPerItem,
                    int stockQuantity, string inventoryValue, int reorderLevel, int daysPerReorder, int itemReorderQuantity,
                    bool itemDiscontinued)
        {
            this.UID = UID;
            Reorder = reorder;
            ItemNo = itemNo;
            Name = name;
            Manufacturer = manufacturer;
            Supplier = supplier;
            Description = description;
            CostPerItem = costPerItem;
            StockQuantity = stockQuantity;
            InventoryValue = inventoryValue;
            ReorderLevel = reorderLevel;
            DaysPerReorder = daysPerReorder;
            ItemReorderQuantity = itemReorderQuantity;
            ItemDiscontinued = itemDiscontinued;
        }

        public Inventory()
        {
        }
    }
}