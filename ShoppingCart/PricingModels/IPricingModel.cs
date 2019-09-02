namespace ShoppingCart.PricingModels
{
    public interface IPricingModel
    {
        double CalculateAmount(float price, int quantity);
    }
}