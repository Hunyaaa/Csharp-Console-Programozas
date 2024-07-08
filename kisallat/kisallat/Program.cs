using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kisallat
{

    class Allat
    {
        public string Nev;
        public int SzuletesiEv;
        public int SzepsegPont;
        public int ViselkedesPont;
        public double Pontszam;

        public Allat(string nev, int szuletesiEv)
        {
            Nev = nev;
            SzuletesiEv = szuletesiEv;
        }

        public Allat(string nev, int szuletesiEv, int szepsegPont, int viselkedesPont, double pontszam)
        {
            Nev = nev;
            SzuletesiEv = szuletesiEv;
            SzepsegPont = szepsegPont;
            ViselkedesPont = viselkedesPont;
            Pontszam = pontszam;
        }

        public void Pontozas(int aktualisEv, int maxKor, int szepsegPont, int viselkedesPont)
        {
            SzepsegPont = szepsegPont;
            ViselkedesPont = viselkedesPont;
            int kor = aktualisEv - SzuletesiEv;

            if (kor > maxKor)
            {
                Pontszam = 0;
            }
            else
            {
                Pontszam = (maxKor - kor) * SzepsegPont + kor * ViselkedesPont;
            }
        }

        public override string ToString()
        {
            return $"Állat neve: {Nev}, Pontszám: {Pontszam}";
        }
    }

    class Program
    {
        static void Main()
        {
            List<Allat> allatok = new List<Allat>();
            Random random = new Random();

            Console.Write("Adja meg az aktuális évet: ");
            int aktualisEv = int.Parse(Console.ReadLine());

            Console.Write("Adja meg a versenyzők korhatárát (maximális kor): ");
            int maxKor = int.Parse(Console.ReadLine());

            while (true)
            {
                Console.Write("Adja meg az állat nevét (vagy nyomjon entert a befejezéshez): ");
                string nev = Console.ReadLine();
                if (string.IsNullOrEmpty(nev))
                {
                    break;
                }

                Console.Write("Adja meg az állat születési évét: ");
                int szuletesiEv = int.Parse(Console.ReadLine());

                Allat allat = new Allat(nev, szuletesiEv);
                int szepsegPont = random.Next(1, 11);  
                int viselkedesPont = random.Next(1, 11);  

                allat.Pontozas(aktualisEv, maxKor, szepsegPont, viselkedesPont);
                Console.WriteLine(allat);

                allatok.Add(allat);
            }

            int allatokSzama = allatok.Count;
            if (allatokSzama > 0)
            {
                double atlagPontszam = 0;
                double maxPontszam = double.MinValue;

                foreach (var allat in allatok)
                {
                    atlagPontszam += allat.Pontszam;
                    if (allat.Pontszam > maxPontszam)
                    {
                        maxPontszam = allat.Pontszam;
                    }
                }

                atlagPontszam /= allatokSzama;

                Console.WriteLine($"Versenyző állatok száma: {allatokSzama}");
                Console.WriteLine($"Átlag pontszám: {atlagPontszam}");
                Console.WriteLine($"Legnagyobb pontszám: {maxPontszam}");
            }
            else
            {
                Console.WriteLine("Nem volt versenyző állat.");
            }
            Console.ReadKey();
        }
    }
}
