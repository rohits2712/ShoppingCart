namespace ShoppingCart.PricingModels
{
    public class BasicPricingModel : IPricingModel
    {
        public double CalculateAmount(float price, int quantity)
        {
            return price * quantity;
        }
    }
}
