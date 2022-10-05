using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2_CashRegistry.Model
{
    internal class Menu
    {
        public int ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine("-- KASSA --");
                Console.WriteLine("1. Ny kund\n2. Admin meny\n0. Avsluta");
                Console.WriteLine("Ange val");

                var sel = Console.ReadLine();
                Console.Clear();
                if (sel == "0" || sel == "1" || sel == "2") return int.Parse(sel);
            }
        }

        public void ShowAdminMenu()
        {
            while (true)
            {
                Console.WriteLine("Ange meny val");
                Console.WriteLine("1. Add product\n2. Delete product\n3. Change price on specific product\n4. Avsluta Admin meny");
                var sel = Console.ReadLine();

                if (sel == "1")
                {
                    AddProductToFile();
                    break;
                }
                if (sel == "2")
                {

                }
                if (sel == "3")
                {

                }
                if (sel == "4")
                {
                    break;
                }
                else
                    Console.WriteLine("---------- !OBS! -----------");
                Console.WriteLine("Ange ett val mellan 1 - 4\n");
            }
        }

        private void AddProductToFile()
        {
            Console.WriteLine("Ange den nya produktens ID");
            var pId = Console.ReadLine();
            Console.WriteLine("Ange den nya produktens namn");
            var pName = Console.ReadLine();

            string[] lines = File.ReadAllLines("Products.txt");
            bool productIdExists = false;
            bool run = true;

            // VALIDERING FUNGERAR EJ, OM PRODUKT ID OCH NAMN PÅ VARAN FINNS SKA MAN INTE KUNNA LÄGG TILL DE I TEXT FILEN

            for (int i = 0; i < lines.Length; i++)
            {
                if (pId != lines[i] && pName != lines[i])
                {
                    productIdExists = true;
                    run = false;
                }
                else if (pId == lines[i] && pName != lines[i])
                {
                    Console.WriteLine("Ange ett annat Produkt ID");
                }
                else if (pId != lines[i] && pName == lines[i])
                {
                    Console.WriteLine("Varan finns redan, gå tillbaka till Admin menyn för att ändra");
                }
            }

            Console.WriteLine("ange den nya produktens pris typ");
            var pPriceType = Console.ReadLine();
            Console.WriteLine("Ange den nya produktens pris");
            var pPrice = Convert.ToDecimal(Console.ReadLine());

            var product = new Product()
            {
                ProductId = pId,
                ProductName = pName,
                PriceType = pPriceType,
                Price = Convert.ToDecimal(pPrice)
            };

            var newProduct = $"{product.ProductId};{product.ProductName};{product.PriceType};{product.Price}";

            // FUNGERAR ATT LÄGGA TILL NYA PRODUKTER I TEXTFILEN, MÅSTE AVSLUTA PROGRAMMET FÖR ATT UPPDATERA TEXTFILEN MEN PRODUCTS

            if (productIdExists)
            {
                File.AppendAllText("Products.txt", newProduct + Environment.NewLine);
            }



        }
    }
}
