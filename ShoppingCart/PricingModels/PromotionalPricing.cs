using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.PricingModels
{
    public class PromotionalPricing : IPricingModel
    {
        public SortedDictionary<int, int> QuantityPromoQuantity { get; set; }
        public PromotionalPricing(SortedDictionary<int, int> promoPricing)
        {
            QuantityPromoQuantity = promoPricing;
        }

        public double CalculateAmount(float price, int quantity)
        {
            var promoApplied = false;
            var billAmount = 0.00d;

            // sort dictionary by descending
            foreach (var quantityPromoQuantityPair in QuantityPromoQuantity.Reverse())
            {
                if (quantity > quantityPromoQuantityPair.Key)
                {
                    var remaining = quantity;
                    while (remaining / quantityPromoQuantityPair.Key > 0)
                    {
                        // Calculate billAmount
                        billAmount += price * quantityPromoQuantityPair.Key;

                        // Get remaining items
                        remaining -= quantityPromoQuantityPair.Key;

                        // Remove the free item from remaining quantity
                        remaining -= quantityPromoQuantityPair.Value;
                    }

                    // Calculate pricing for remaining items
                    billAmount += price * remaining;
                    promoApplied = true;
                    break;
                }
            }
            if (!promoApplied)
                billAmount = price * quantity;
            return billAmount;
        }
    }
}
