using Assignment2_CashRegistry.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2_CashRegistry
{
    public class Application
    {
        public void Run()
        {
            var allProducts = new List<Product>();
            allProducts = ReadProductsFromFile();

            var menu = new Menu();
            while (true)
            {
                var sel = menu.ShowMainMenu();
                if (sel == 0)
                    break;
                if (sel == 1)
                    RegisterProducts(allProducts);
                if (sel == 2)
                    menu.ShowAdminMenu();
            }
        }
        private Product FindProductFromProductId(List<Product> allProducts, string productId)
        {
            foreach (var product in allProducts)
            {
                if (product.ProductId.ToLower() == productId.ToLower()) return product;
            }
            return null;
        }
        private void RegisterProducts(List<Product> allProducts)
        {
            Product product;
            while (true)
            {
                Console.WriteLine("Ange produkt ID");
                var productId = Console.ReadLine();

                product = FindProductFromProductId(allProducts, productId);

                if (product == null)
                {
                    Console.WriteLine("Invalid Product ID");
                }
                else
                    break;
            }
            Console.Write($"Ange hur mycket av {product.ProductName} du har köpt: ");
            var countOfSpecificProduct = int.Parse(Console.ReadLine());
            var sumOfSpecificProduct = countOfSpecificProduct * product.Price;
            
            //////// SAVE TO RECIPT
            ///

            Console.WriteLine($"Product: {product.ProductName} {product.PriceType} {product.Price} * {countOfSpecificProduct} = {sumOfSpecificProduct}kr");

            var fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            var line = $"{product.ProductName} {countOfSpecificProduct} * {product.Price} = {sumOfSpecificProduct}kr";

            File.AppendAllText(fileName, line + Environment.NewLine);
        }
        private List<Product> ReadProductsFromFile()
        {
            var result = new List<Product>();

            foreach (var line in File.ReadLines("Products.txt"))
            {
                var parts = line.Split(';');

                var product = new Product
                {
                    ProductId = parts[0],
                    ProductName = parts[1],
                    PriceType = parts[2],
                    Price = Convert.ToDecimal(parts[3])
                };
                result.Add(product);

                //var product = new Product();
                //var name = parts[0];
                //var priceType = parts[1];
                //var price = decimal.Parse(parts[2]);
            }
            return result;
        }
    }
}
