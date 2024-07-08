using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AllatFyolt
{
    class Allat
    {
        public string Nev { get; private set; }
        public int SzuletesiEv { get; private set; }
        public int RajtSzam { get; private set; }
        public int SzepsegPont { get; private set; }
        public int ViselkedesPont { get; private set; }

        public int PontSzam { get; protected set; }

        public static int AktualisEv { get; set; }
        public static int KorHatar { get; set; }


        public Allat(int rajtSzam, string nev, int szuletesiEv)
        {
            this.RajtSzam = rajtSzam;
            this.Nev = nev;
            this.SzuletesiEv = szuletesiEv;
        }

        public int Kor()
        {
            return AktualisEv - SzuletesiEv;
        }

        public virtual void Pontozas(int szepsegPont, int viselkedesPont)
        {
            this.SzepsegPont = szepsegPont;
            this.ViselkedesPont = viselkedesPont;

            if (Kor() < KorHatar)
            {
                PontSzam = viselkedesPont * Kor() + szepsegPont * (KorHatar - Kor());
            }
            else
            {
                PontSzam = 0;
            }
        }

        public override string ToString()
        {
            return $"{RajtSzam}. {Nev} nevű {this.GetType().Name.ToLower()} - Pontszám: {PontSzam}";
        }
    }

    class Kutya : Allat
    {
        public int GazdaViszonyPont { get; private set; }
        public bool KapottViszonyPontot { get; private set; }

        public Kutya(int rajtSzam, string nev, int szulEv)
            : base(rajtSzam, nev, szulEv)
        {
        }

        public void ViszonyPontozas(int gazdaViszonyPont)
        {
            this.GazdaViszonyPont = gazdaViszonyPont;
            KapottViszonyPontot = true;
        }

        public override void Pontozas(int szepsegPont, int viselkedesPont)
        {
            base.Pontozas(szepsegPont, viselkedesPont);
            if (KapottViszonyPontot)
            {
                this.PontSzam += GazdaViszonyPont;
            }
        }
    }

    class Macska : Allat
    {
        public bool VanMacskaSzallitoDoboz { get; set; }

        public Macska(int rajtSzam, string nev, int szulEv, bool vanMacskaSzallitoDoboz)
            : base(rajtSzam, nev, szulEv)
        {
            this.VanMacskaSzallitoDoboz = vanMacskaSzallitoDoboz;
        }

        public override void Pontozas(int szepsegPont, int viselkedesPont)
        {
            if (VanMacskaSzallitoDoboz)
            {
                base.Pontozas(szepsegPont, viselkedesPont);
            }
        }
    }

    public class Vezerles
    {

        private List<Allat> allatok = new List<Allat>();

        public void Start()
        {
            Allat.AktualisEv = 2015;
            Allat.KorHatar = 10;

            Proba();

            Regisztralas();
            kiir("\nA regisztrált versenyzők\n");
            Verseny();
            kiir("\nA verseny eredménye\n");
        }

        private void Proba()
        {
            Allat allat1, allat2;
            string nev1 = "Pamacs", nev2 = "Bolhazsák";
            int szulEv1 = 2010, szulEv2 = 2011;
            bool vanDoboz = true;
            int rajtSzam = 1;
            int szepsegPont = 5, viselkedesPont = 4, viszonyPont = 6;

            allat1 = new Kutya(rajtSzam, nev1, szulEv1);
            rajtSzam++;
            allat2 = new Macska(rajtSzam, nev2, szulEv2, vanDoboz);

            Console.WriteLine("A regisztrált állatok:");
            Console.WriteLine(allat1);
            Console.WriteLine(allat2);

            allat2.Pontozas(szepsegPont, viselkedesPont);
            if (allat1 is Kutya kutya)
            {
                kutya.ViszonyPontozas(viszonyPont);
            }
            allat1.Pontozas(szepsegPont, viselkedesPont);


            Console.WriteLine("\nA verseny eredménye:");
            Console.WriteLine(allat1);
            Console.WriteLine(allat2);
        }

        private void Regisztralas()
        {
            StreamReader olvasoCsatorna = new StreamReader("Z:\\12_Kozgaz\\DIAKOK\\Hunyadi Máté\\allatok.txt");
            string fajta, nev;
            int rajtSzam = 1, szulEv;
            bool vanDoboz;

            while (!olvasoCsatorna.EndOfStream)
            {
                fajta = olvasoCsatorna.ReadLine();
                nev = olvasoCsatorna.ReadLine();
                szulEv = int.Parse(olvasoCsatorna.ReadLine());

                if (fajta == "kutya")
                {
                    allatok.Add(new Kutya(rajtSzam, nev, szulEv));
                }
                else
                {
                    vanDoboz = bool.Parse(olvasoCsatorna.ReadLine());
                    allatok.Add(new Macska(rajtSzam, nev, szulEv, vanDoboz));
                }

                rajtSzam++;
            }

            olvasoCsatorna.Close();
        }
        private void kiir(string uzenet)
        {
            Console.WriteLine(uzenet);
            foreach (var allat in allatok)
            {
                Console.WriteLine(allat.ToString());
            }
        }

        private void Verseny()
        {
            Random rand = new Random();
            int hatar = 11;

            foreach (Allat item in allatok)
            {
                if (item is Kutya kutya)
                {
                    kutya.ViszonyPontozas(rand.Next(hatar));
                }

                item.Pontozas(rand.Next(hatar), rand.Next(hatar));
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            new Vezerles().Start();
            Console.ReadKey();
        }
    }
}
