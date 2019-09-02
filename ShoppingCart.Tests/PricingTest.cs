using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ShoppingCart.PricingModels;
using System;
using System.Collections.Generic;

namespace ShoppingCart.Tests
{
    [TestClass]
    public class PricingTest
    {
        IInventory _inventory; 

        [TestInitialize]
        public void TestInit()
        {
            var availableItems = new Dictionary<char, Item>();
            // For PromotionalPricing, pass a dictionary containing pairs of item & free items given as promo
            availableItems.Add('X', new Item()
            {
                PricingModel = new PromotionalPricing(new SortedDictionary<int, int>
                                                                {
                                                                    { 3, 1 }
                                                                }),
                Price = 35.30f
            });

            // For FlatPriceOnQuantity, pass a dictionary containing pairs of item & free items given as promo
            availableItems.Add('Y', new Item()
            {
                PricingModel = new FlatPriceOnQuantity(new SortedDictionary<int, float>
                                                {
                                                    { 5, 1000f }
                                                }),
                Price = 250.00f
            });

            // For basic pricing (q * p)
            availableItems.Add('Z', new Item() { PricingModel = new BasicPricingModel(), Price = 12.50f });

            _inventory = Substitute.For<IInventory>();
            _inventory.GetAvailableItems().ReturnsForAnyArgs(availableItems);
        }

        [TestMethod]
        public void Test_FlatPrice_FivePieces_NoPromo()
        {
            var cart = new Cart(_inventory);
            cart.Add('Z', 5);
            Assert.AreEqual(Math.Round(12.50f * 5), cart.CheckOut());
        }

        [TestMethod]
        public void Test_FlatPrice_TenPieces_NoPromo()
        {
            var cart = new Cart(_inventory);
            cart.Add('Z', 10);
            Assert.AreEqual(Math.Round(12.50f * 10), cart.CheckOut());
        }

        [TestMethod]
        public void Test_PromotionalPricing_ThreePieces_NoPromo()
        {
            var cart = new Cart(_inventory);
            cart.Add('X', 3);
            Assert.AreEqual(Math.Round(35.30f * 3), cart.CheckOut());
        }

        [TestMethod]
        public void Test_PromotionalPricing_FourPieces_OneFree()
        {
            var cart = new Cart(_inventory);
            cart.Add('X', 4);
            Assert.AreEqual(Math.Round(35.30f * 3), cart.CheckOut());
        }

        [TestMethod]
        public void Test_PromotionalPricing_EightPieces_TwoFree()
        {
            var cart = new Cart(_inventory);
            cart.Add('X', 8);
            Assert.AreEqual(Math.Round(35.30f * 6), cart.CheckOut());
        }

        [TestMethod]
        public void Test_FlatPriceOnQuantity_FourPieces_NoPromo()
        {
            var cart = new Cart(_inventory);
            cart.Add('Y', 4);
            Assert.AreEqual(Math.Round(250.00f * 4), cart.CheckOut());
        }

        [TestMethod]
        public void Test_FlatPriceOnQuantity_FivePieces_Amount1000()
        {
            var cart = new Cart(_inventory);
            cart.Add('Y', 5);
            Assert.AreEqual(1000, cart.CheckOut());
        }

        [TestMethod]
        public void Test_FlatPriceOnQuantity_EightPieces_Amount1750()
        {
            var cart = new Cart(_inventory);
            cart.Add('Y', 8);
            Assert.AreEqual(1750, cart.CheckOut());
        }

        [TestMethod]
        public void Test_FlatPriceOnQuantity_TenPieces_Amount2000()
        {
            var cart = new Cart(_inventory);
            cart.Add('Y', 10);
            Assert.AreEqual(2000, cart.CheckOut());
        }

        [TestMethod]
        public void Test_AllPricings_NoPromo()
        {
            var cart = new Cart(_inventory);
            cart.Add('X', 3);
            cart.Add('Y', 4);
            cart.Add('Z', 5);
            Assert.AreEqual(
                Math.Round(35.30f * 3) + Math.Round(250.00f * 4) + Math.Round(12.50f * 5)
                , cart.CheckOut()
                );
        }

        [TestMethod]
        public void Test_AllPricings_WithPromo()
        {
            var cart = new Cart(_inventory);
            cart.Add('X', 4);
            cart.Add('Y', 5);
            cart.Add('Z', 5);
            Assert.AreEqual(
                Math.Round(35.30f * 3) + 1000 + Math.Round(12.50f * 5)
                , cart.CheckOut()
                );
        }

        [TestMethod]
        public void Test_AllPricings_WithPromo2()
        {
            var cart = new Cart(_inventory);
            cart.Add('X', 8);
            cart.Add('Y', 10);
            cart.Add('Z', 10);
            Assert.AreEqual(
                Math.Round(35.30f * 6) + 2000 + Math.Round(12.50f * 10)
                , cart.CheckOut()
                );
        }
    }
}
