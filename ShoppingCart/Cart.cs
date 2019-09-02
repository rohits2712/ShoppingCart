using System;
using System.Collections.Generic;

namespace ShoppingCart
{
    public class Cart
    {
        public Dictionary<char, int> SelectedItems { get; set; }
        private IInventory _inventory;
        public Cart(IInventory inventory)
        {
            SelectedItems = new Dictionary<char, int>();
            _inventory = inventory;
        }
        public void Add(char code, int count)
        {
            if (count == 0)
                return;

            if (!_inventory.GetAvailableItems().ContainsKey(code))
                throw new InvalidOperationException("Item not found in inventory.");

            if (SelectedItems.ContainsKey(code))
                SelectedItems[code] = SelectedItems[code] + count;
            else
                SelectedItems.Add(code, count);
        }

        public double CheckOut()
        {
            double billAmount = 0.00f;

            foreach (var itemCountPair in SelectedItems)
            {
                var inventoryItem = _inventory.GetAvailableItems()[itemCountPair.Key];
                billAmount += inventoryItem.PricingModel.CalculateAmount(inventoryItem.Price, itemCountPair.Value);
            }

            // Round to the nearest full number
            return Math.Round(billAmount);
        }
    }
}
