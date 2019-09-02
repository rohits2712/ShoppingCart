using ShoppingCart.PricingModels;
using System.Collections.Generic;

namespace ShoppingCart
{
    public class Inventory : IInventory
    {
        private Dictionary<char, Item> AvailableItems { get; set; }

        private static readonly object PadLock = new object();
        private static volatile Inventory _Inventory;
        public static Inventory Instance()
        {
            if (_Inventory != null) return _Inventory;
            lock (PadLock)
            {
                if (_Inventory == null) _Inventory = new Inventory();
            }
            return _Inventory;
        }

        private Inventory()
        {
            InitializeInventory();
        }

        private void InitializeInventory()
        {
            AvailableItems = new Dictionary<char, Item>();
            // For PromotionalPricing, pass a dictionary containing pairs of item & free items given as promo
            AvailableItems.Add('A', new Item()
            {
                PricingModel = new PromotionalPricing(new SortedDictionary<int, int>
                                                                {
                                                                    { 2, 1 }
                                                                }),
                Price = 59.90f
            });

            // For FlatPriceOnQuantity, pass a dictionary containing pairs of item & free items given as promo
            AvailableItems.Add('B', new Item()
            {
                PricingModel = new FlatPriceOnQuantity(new SortedDictionary<int, float>
                                                {
                                                    { 3, 999f }
                                                }),
                Price = 399.00f
            });

            // For basic pricing (q * p)
            AvailableItems.Add('C', new Item() { PricingModel = new BasicPricingModel(), Price = 19.54f });
        }

        public Dictionary<char, Item> GetAvailableItems()
        {
            return AvailableItems;
        }
    }
}
