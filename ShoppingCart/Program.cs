using System;

namespace ShoppingCart
{
    class Program
    {
        static void Main(string[] args)
        {
            var cart = new Cart(Inventory.Instance());
            cart.Add('A', 7);
            cart.Add('B', 7);
            cart.Add('C', 4);
            Console.WriteLine(cart.CheckOut());
            Console.ReadLine();
        }
    }
}
