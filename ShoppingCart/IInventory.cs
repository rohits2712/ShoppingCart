using System.Collections.Generic;

namespace ShoppingCart
{
    public interface IInventory
    {
        Dictionary<char, Item> GetAvailableItems();
    }
}