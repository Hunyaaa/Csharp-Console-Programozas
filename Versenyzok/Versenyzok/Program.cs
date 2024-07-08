using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Versenyzok
{
    internal class Versenyzok
    {
        class Versenyzo
        {
            public string nev;
            public string szak;
            public int rajtszam;
            public int pontszam;

            public Versenyzo(string sor)
            {
                string[] r = sor.Split(';');
                this.nev = r[0];
                this.szak = r[1];
                this.rajtszam = Int32.Parse(r[2]);
                this.pontszam = Int32.Parse(r[3]);
            }
        }
        static void Main(string[] args)
        {
            int maxpont = 500;
            Random rnd = new Random();
            StreamWriter sw = new StreamWriter("d:\\Dalverseny.txt");

            Console.WriteLine("A szóköz kulcsszóval állítható meg a program"); ;
            int kilepes = 0;
            while (kilepes != 1)
            {
                Console.Write(" Add meg a neved: ");
                string nev = Console.ReadLine();

                Console.Write(" Add meg a szakod: ");
                string szak = Console.ReadLine();
                if (nev == " ")
                {
                    kilepes += 1;
                    break;
                }
                Console.WriteLine($"Neve: {nev} szakmája: {szak}");
                sw.WriteLine($"{nev};{szak};{rnd.Next(0, 600)};{rnd.Next(0, maxpont)}");
            }
            sw.Close();

            List<Versenyzo> list = new List<Versenyzo>();
            StreamReader sr = new StreamReader("D:\\Dalverseny.txt");
            while (!sr.EndOfStream)
            {
                string sor = sr.ReadLine();
                Versenyzo o = new Versenyzo(sor);
                list.Add(o);
            }

            int legnagyobb = 0;
            for (int i = 0; i < list.Count(); i++)
            {
                if (list[i].pontszam > legnagyobb)
                {
                    legnagyobb = i;
                }

            }
            Console.WriteLine($"A legnagyobb pontszámot {list[legnagyobb].nev} szerezte, szakmája: {list[legnagyobb].szak} rajtszáma: {list[legnagyobb].rajtszam}, pontszáma: {list[legnagyobb].pontszam}");
            Console.ReadKey();
        }
    }
}