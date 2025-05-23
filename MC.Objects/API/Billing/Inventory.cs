﻿//Copyright © 2023 Mandala Consulting, LLC All rights reserved.
//Created by Alexander Fields

namespace MandalaConsulting.Objects.Billing
{
    public class Inventory
    {
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

        public decimal CostPerItem
        {
            get; set;
        }

        public int DaysPerReorder
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public string InventoryValue
        {
            get; set;
        }

        public bool ItemDiscontinued
        {
            get; set;
        }

        public string ItemNo
        {
            get; set;
        }

        public int ItemReorderQuantity
        {
            get; set;
        }

        public string Manufacturer
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string Reorder
        {
            get; set;
        }

        public int ReorderLevel
        {
            get; set;
        }

        public int StockQuantity
        {
            get; set;
        }

        public string Supplier
        {
            get; set;
        }

        public string UID
        {
            get; set;
        }
    }
}