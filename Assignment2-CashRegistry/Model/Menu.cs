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
                Console.WriteLine("1. Ny kund\n0. Avsluta");
                Console.WriteLine("Ange val");

                var sel = Console.ReadLine();
                Console.Clear();
                if (sel == "0" || sel == "1") return int.Parse(sel);
            }
        }

        public int ShowAdminMenu()
        {
            return 0;
        }

    }
}
