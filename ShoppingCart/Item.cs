using ShoppingCart.PricingModels;

namespace ShoppingCart
{
    public class Item
    {
        public float Price { get; set; }
        public IPricingModel PricingModel { get; set; }
    }
}
