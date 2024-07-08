using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        public class PenzValtas
        {
            public static void Main(string[] args)
            {
                // összeg bekérés
                Console.Write("Adja meg a felváltandó összeget (forintban): ");
                int felvaltandoOsszeg = int.Parse(Console.ReadLine());

                // bankjegyek
                int[] bankjegyek = { 20000, 10000, 5000, 2000, 1000, 500, 200, 100, 50, 20, 10, 5 };

                // példányosítás
                int[] bankjegyDarabok = new int[bankjegyek.Length];
                int osszesBankjegyDarab = 0;

                // pénz átváltás
                int maradekOsszeg = felvaltandoOsszeg;
                for (int i = 0; i < bankjegyek.Length; i++)
                {
                    int bankjegyDarab = maradekOsszeg / bankjegyek[i];
                    maradekOsszeg %= bankjegyek[i];
                    bankjegyDarabok[i] = bankjegyDarab;
                    osszesBankjegyDarab += bankjegyDarab;
                }

                // ha sikeres-
                if (maradekOsszeg == 0)
                {
                    Console.WriteLine("A váltás sikeres!");
                    Console.WriteLine("A szükséges bankjegyek:");
                    for (int i = 0; i < bankjegyek.Length; i++)
                    {
                        if (bankjegyDarabok[i] > 0)
                        {
                            Console.WriteLine($"{bankjegyek[i]} Ft: {bankjegyDarabok[i]} db");
                        }
                    }
                    Console.WriteLine($"Összesen: {osszesBankjegyDarab} db bankjegy");
                }
                // ha nem sikeres-
                else
                {
                    Console.WriteLine("A megadott összeg nem váltható fel a forgalomban lévő bankjegyekkel.");
                }
                Console.ReadKey();
            }
        }
    }
}
