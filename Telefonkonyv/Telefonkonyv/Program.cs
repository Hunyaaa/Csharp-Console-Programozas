using System;
using System.Collections.Generic;
using System.IO;

class Szemely
{
    public string Nev { get; set; }
    public string Cim { get; set; }
    public string ApaNev { get; set; }
    public string AnyaNev { get; set; }
    public long TelefonSzam { get; set; }
    public string Nem { get; set; }
    public string Email { get; set; }
    public string SzemelyiSzam { get; set; }
}

class Program
{
    static List<Szemely> telefonkonyv = new List<Szemely>();

    static void Main()
    {
        if (File.Exists("projekt"))
        {
            Beolvas();
        }
        Menu();
    }

    static void Beolvas()
    {
        using (StreamReader sr = new StreamReader("projekt"))
        {
            string sor;
            while ((sor = sr.ReadLine()) != null)
            {
                var adatok = sor.Split(',');
                Szemely szemely = new Szemely
                {
                    Nev = adatok[0],
                    Cim = adatok[1],
                    ApaNev = adatok[2],
                    AnyaNev = adatok[3],
                    TelefonSzam = long.Parse(adatok[4]),
                    Nem = adatok[5],
                    Email = adatok[6],
                    SzemelyiSzam = adatok[7]
                };
                telefonkonyv.Add(szemely);
            }
        }
    }

    static void Mentes()
    {
        using (StreamWriter sw = new StreamWriter("projekt"))
        {
            foreach (var szemely in telefonkonyv)
            {
                sw.WriteLine($"{szemely.Nev},{szemely.Cim},{szemely.ApaNev},{szemely.AnyaNev},{szemely.TelefonSzam},{szemely.Nem},{szemely.Email},{szemely.SzemelyiSzam}");
            }
        }
    }

    static void Menu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("********** ÜDVÖZÖLJÜK A TELEFONKÖNYVBEN **********");
            Console.WriteLine("\n1. Új hozzáadása\n2. Listázás\n3. Kilépés\n4. Módosítás\n5. Keresés\n6. Törlés");
            var valasztas = Console.ReadKey().KeyChar;

            switch (valasztas)
            {
                case '1':
                    UjHozzaadasa();
                    break;
                case '2':
                    Listazas();
                    break;
                case '3':
                    return;
                case '4':
                    Modositas();
                    break;
                case '5':
                    Kereses();
                    break;
                case '6':
                    Torles();
                    break;
                default:
                    Console.WriteLine("\nÉrvénytelen választás, próbálja újra!");
                    break;
            }
        }
    }

    static void UjHozzaadasa()
    {
        Szemely szemely = new Szemely();

        Console.WriteLine("\nAdja meg a nevet:");
        szemely.Nev = Console.ReadLine();
        Console.WriteLine("Adja meg a címet:");
        szemely.Cim = Console.ReadLine();
        Console.WriteLine("Adja meg az apja nevét:");
        szemely.ApaNev = Console.ReadLine();
        Console.WriteLine("Adja meg az anyja nevét:");
        szemely.AnyaNev = Console.ReadLine();
        Console.WriteLine("Adja meg a telefonszámot:");
        szemely.TelefonSzam = long.Parse(Console.ReadLine());
        Console.WriteLine("Adja meg a nemet:");
        szemely.Nem = Console.ReadLine();
        Console.WriteLine("Adja meg az e-mailt:");
        szemely.Email = Console.ReadLine();
        Console.WriteLine("Adja meg a személyi számot:");
        szemely.SzemelyiSzam = Console.ReadLine();

        telefonkonyv.Add(szemely);
        Mentes();
        Console.WriteLine("\nRekord mentve. Nyomjon meg egy gombot a folytatáshoz.");
        Console.ReadKey();
    }

    static void Listazas()
    {
        Console.Clear();
        foreach (var szemely in telefonkonyv)
        {
            Console.WriteLine($"\nNév: {szemely.Nev}\nCím: {szemely.Cim}\nApja neve: {szemely.ApaNev}\nAnyja neve: {szemely.AnyaNev}\nTelefonszám: {szemely.TelefonSzam}\nNem: {szemely.Nem}\nE-mail: {szemely.Email}\nSzemélyi szám: {szemely.SzemelyiSzam}");
        }
        Console.WriteLine("\nNyomjon meg egy gombot a folytatáshoz.");
        Console.ReadKey();
    }

    static void Kereses()
    {
        Console.WriteLine("\nAdja meg a keresett személy nevét:");
        string nev = Console.ReadLine();
        var talalat = telefonkonyv.Find(s => s.Nev.Equals(nev, StringComparison.OrdinalIgnoreCase));

        if (talalat != null)
        {
            Console.WriteLine($"\nNév: {talalat.Nev}\nCím: {talalat.Cim}\nApja neve: {talalat.ApaNev}\nAnyja neve: {talalat.AnyaNev}\nTelefonszám: {talalat.TelefonSzam}\nNem: {talalat.Nem}\nE-mail: {talalat.Email}\nSzemélyi szám: {talalat.SzemelyiSzam}");
        }
        else
        {
            Console.WriteLine("Nincs ilyen név a telefonkönyvben.");
        }

        Console.WriteLine("\nNyomjon meg egy gombot a folytatáshoz.");
        Console.ReadKey();
    }

    static void Modositas()
    {
        Console.WriteLine("\nAdja meg a módosítandó személy nevét:");
        string nev = Console.ReadLine();
        var talalat = telefonkonyv.Find(s => s.Nev.Equals(nev, StringComparison.OrdinalIgnoreCase));

        if (talalat != null)
        {
            Console.WriteLine("Adja meg az új adatokat (hagyja üresen, ha nem akarja módosítani):");

            Console.WriteLine($"Név ({talalat.Nev}):");
            string ujNev = Console.ReadLine();
            if (!string.IsNullOrEmpty(ujNev)) talalat.Nev = ujNev;

            Console.WriteLine($"Cím ({talalat.Cim}):");
            string ujCim = Console.ReadLine();
            if (!string.IsNullOrEmpty(ujCim)) talalat.Cim = ujCim;

            Console.WriteLine($"Apja neve ({talalat.ApaNev}):");
            string ujApaNev = Console.ReadLine();
            if (!string.IsNullOrEmpty(ujApaNev)) talalat.ApaNev = ujApaNev;

            Console.WriteLine($"Anyja neve ({talalat.AnyaNev}):");
            string ujAnyaNev = Console.ReadLine();
            if (!string.IsNullOrEmpty(ujAnyaNev)) talalat.AnyaNev = ujAnyaNev;

            Console.WriteLine($"Telefonszám ({talalat.TelefonSzam}):");
            string ujTelefonSzam = Console.ReadLine();
            if (!string.IsNullOrEmpty(ujTelefonSzam)) talalat.TelefonSzam = long.Parse(ujTelefonSzam);

            Console.WriteLine($"Nem ({talalat.Nem}):");
            string ujNem = Console.ReadLine();
            if (!string.IsNullOrEmpty(ujNem)) talalat.Nem = ujNem;

            Console.WriteLine($"E-mail ({talalat.Email}):");
            string ujEmail = Console.ReadLine();
            if (!string.IsNullOrEmpty(ujEmail)) talalat.Email = ujEmail;

            Console.WriteLine($"Személyi szám ({talalat.SzemelyiSzam}):");
            string ujSzemelyiSzam = Console.ReadLine();
            if (!string.IsNullOrEmpty(ujSzemelyiSzam)) talalat.SzemelyiSzam = ujSzemelyiSzam;

            Mentes();
            Console.WriteLine("\nRekord módosítva. Nyomjon meg egy gombot a folytatáshoz.");
        }
        else
        {
            Console.WriteLine("Nincs ilyen név a telefonkönyvben.");
        }

        Console.ReadKey();
    }

    static void Torles()
    {
        Console.WriteLine("\nAdja meg a törlendő személy nevét:");
        string nev = Console.ReadLine();
        var talalat = telefonkonyv.RemoveAll(s => s.Nev.Equals(nev, StringComparison.OrdinalIgnoreCase));

        if (talalat > 0)
        {
            Mentes();
            Console.WriteLine("Rekord törölve.");
        }
        else
        {
            Console.WriteLine("Nincs ilyen név a telefonkönyvben.");
        }

        Console.WriteLine("\nNyomjon meg");
    }
}