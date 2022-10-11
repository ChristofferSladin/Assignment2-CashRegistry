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
        public Product FindProductFromProductId(List<Product> allProducts, string productId)
        {
            foreach (var product in allProducts)
            {
                if (product.ProductId.ToLower() == productId.ToLower()) return product;
            }
            return null;
        }

        public void RegisterProducts(List<Product> allProducts)
        {
            Product product;
            while (true)
            {
                Console.WriteLine($"<Produkt ID> <ANTAL>");

                var selectionOfprod = Console.ReadLine();

                if (selectionOfprod == null)
                {
                    Console.WriteLine("Felaktigt Produkt ID eller Antal");
                }
                var reslut = selectionOfprod.Split(' ');

                reslut[0] = selectionOfprod.Substring(0, 3);
                reslut[1] = selectionOfprod.Substring(4);

                product = FindProductFromProductId(allProducts, reslut[0]);

                if (product == null)
                {
                    Console.WriteLine("Invalid Product ID");
                }
                else
                {
                    // HÄR ÄR VI!!!!!
                    var reciptDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    var fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

                    var sumToti = 0m;
                    var prodSum = product.Price* Convert.ToDecimal(reslut[1]);

                    foreach (var row in File.ReadLines(fileName))
                    {
                        sumToti += prodSum;
                    }

                    var line = $"{product.ProductName}: {Convert.ToInt32(reslut[1])} * {product.Price} = {prodSum}";
                    File.AppendAllText(fileName, line + Environment.NewLine);
                    Console.Clear();

                    Console.WriteLine("\nFör att fortsätta lägga till varor klicka valfri tangent");
                    Console.WriteLine("För att avsluta (N) = AVSLUTA");

                    var sel = Console.ReadLine();
                    if (sel == null)
                    {
                        continue;
                    }
                    if (sel == "N")
                    {
                        Console.Clear();
                        Console.WriteLine("PAY");
                        
                        File.AppendAllText(fileName, $"  Total: {sumToti} " + Environment.NewLine);
                        File.AppendAllText(fileName, $"-----------{reciptDate}-----------" + Environment.NewLine);

                        break;
                    }
                }
            }
        }
        public List<Product> ReadProductsFromFile()
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
            }
            return result;
        }
    }
}
