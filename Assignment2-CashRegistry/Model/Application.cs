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
            var sumToti = 0m;
            while (true)
            {
                Console.WriteLine($"<Produkt ID> <ANTAL>");
                var selectionOfprod = Console.ReadLine();

                if (selectionOfprod.Length == 0)
                {
                    Console.WriteLine("Felaktigt Produkt ID eller Antal");
                    continue;
                }

                var reslut = selectionOfprod.Split(' ');

                if (reslut.Length != 2) 
                {
                    Console.WriteLine("Ange 2 inputs");
                    continue;
                }

                if (reslut[0].Length != 3)
                {
                    Console.WriteLine("Produkt ID måste inehålla 3 tecken");
                    continue;
                }
                if (reslut[1].Length == 0 || reslut[1] == "0")
                {
                    Console.WriteLine("Antal måste vara minst 1");
                    continue;
                }

                reslut[0] = selectionOfprod.Substring(0, 3);
                reslut[1] = selectionOfprod.Substring(4);

                product = FindProductFromProductId(allProducts, reslut[0]);

                if (product == null)
                {
                    Console.WriteLine("Invalid Product ID");
                    continue;
                }
                else
                {
                    var reciptDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    var fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

                    var prodSum = product.Price * Convert.ToDecimal(reslut[1]);
                    sumToti += product.Price * Convert.ToDecimal(reslut[1]);

                    var line = $"{product.ProductName}: {Convert.ToInt32(reslut[1])} * {product.Price} = {prodSum}";
                    File.AppendAllText(fileName, line + Environment.NewLine);
                    Console.Clear();

                    var sel = Console.ReadLine();
                    var pay = sel.ToLower().Trim();

                    if (pay == "pay")
                    {
                        Console.Clear();
                        Console.WriteLine("PAY\n\n");

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