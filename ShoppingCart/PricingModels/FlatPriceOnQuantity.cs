using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.PricingModels
{
    public class FlatPriceOnQuantity : IPricingModel
    {
        public SortedDictionary<int, float> QuantityPrice { get; set; }
        public FlatPriceOnQuantity(SortedDictionary<int, float> quantityPrice)
        {
            QuantityPrice = quantityPrice;
        }

        public double CalculateAmount(float price, int quantity)
        {
            var billAmount = 0.00d;
            var promoApplied = false;
            // sort dictionary by descending
            foreach (var quantityPromoQuantityPair in QuantityPrice.Reverse())
            {
                if (quantity >= quantityPromoQuantityPair.Key)
                {
                    // Calculate billAmount for promo items
                    billAmount += (quantity / quantityPromoQuantityPair.Key) * quantityPromoQuantityPair.Value;

                    // Get remaining items
                    var remaining = quantity % quantityPromoQuantityPair.Key;

                    // Calculate pricing for remaining items
                    billAmount += price * remaining;
                    promoApplied = true;
                    break;
                }
            }
            if(!promoApplied)
                billAmount = price * quantity;
            return billAmount;
        }
    }
}
