namespace jaror;

public class Jarmu
{
    public int Ora { get; set; }
    public int Perc { get; set; }
    public int Mperc { get; set; }
    public string Rendszam { get; set; }
    public string OsszAdat { get; set; }
    public int Time() => (Ora * 60 + Perc) * 60 + Mperc;
    public string OOPPMM() => $"{Ora:D2}:{Perc:D2}:{Mperc:D2}";
}
public class Program
{
    static List<Jarmu> list = new List<Jarmu>();
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Feladat1();
        Feladat2();
        Feladat3();
        Feladat4();
        Feladat5();
        Feladat6();
        Feladat7();

        Console.ReadKey();
    }
    private static void Feladat1()
    {
        StreamReader sr = new StreamReader(@"jarmu.txt");

        while (!sr.EndOfStream)
        {
            string[] line = sr.ReadLine().Split(' ');
            Jarmu uj = new Jarmu();

            uj.Ora = int.Parse(line[0]);
            uj.Perc = int.Parse(line[1]);
            uj.Mperc = int.Parse(line[2]);
            uj.Rendszam = line[3];
            uj.OsszAdat = String.Join(" ", line);

            list.Add(uj);
        }

        sr.Close();
    }
    private static void Feladat2()
    {
        Console.WriteLine("2. feladat");

        int kezd = list.Min(x => x.Ora);
        int veg = list.Max(x => x.Ora) + 1;
        int ossz = veg - kezd;

        Console.WriteLine($"Aznap {ossz} óra hosszat dolgoztak az ellenőrzést végzők.");
        Console.WriteLine();
    }
    private static void Feladat3()
    {
        Console.WriteLine("3. feladat");

        var csoport = list.GroupBy(x => x.Ora).ToList();

        foreach (var group in csoport)
        {
            Console.WriteLine($"{group.Key} óra: {group.First().Rendszam}!");
        }

        Console.WriteLine();
    }
    private static void Feladat4()
    {
        Console.WriteLine("4. feladat");

        int b = 0, k = 0, m = 0;

        foreach (var item in list)
        {
            if (item.Rendszam.StartsWith("B")) b++;
            else if (item.Rendszam.StartsWith("K")) k++;
            else if (item.Rendszam.StartsWith("M")) m++;
        }

        Console.WriteLine($"Busz: {b}, kamion: {k}, motor: {m}.");
        Console.WriteLine();
    }
    private static void Feladat5()
    {
        Console.WriteLine("5. feladat");

        string kezd = "", veg = "";
        int maxIdo = 0;

        for (int i = 0; i < list.Count; i += 2)
        {
            if (list[i + 1].Time() - list[i].Time() > maxIdo)
            {
                maxIdo = list[i + 1].Time() - list[i].Time();
                kezd = list[i].OOPPMM();
                veg = list[i + 1].OOPPMM();
            }
        }

        Console.WriteLine($"{kezd} - {veg}");
        Console.WriteLine();
    }
    private static void Feladat6()
    {
        Console.WriteLine("6. feladat");

        Console.Write("Adjon meg egy rendszámot: ");
        char[] bSzam = Console.ReadLine().ToCharArray();

        foreach (var item in list)
        {
            char[] rendszam = item.Rendszam.ToCharArray();
            bool megfelelo = true;

            for (int i = 0; i < rendszam.Length; i++)
            {
                if (bSzam[i] != '*' && rendszam[i] != bSzam[i]) megfelelo = false;
            }

            if (megfelelo) Console.WriteLine(item.Rendszam);
        }

        Console.WriteLine();
    }
    private static void Feladat7()
    {
        Console.WriteLine("7. feladat");

        StreamWriter sw = new StreamWriter(@"vizsgalt.txt");
        int vizsgalt = 0;

        sw.WriteLine(list[vizsgalt].OsszAdat);

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Time() > list[vizsgalt].Time() + 300)
            {
                vizsgalt = i;
                sw.WriteLine(list[vizsgalt].OsszAdat);
            }
        }

        Console.WriteLine("A kiírás befejeződött");
        sw.Close();
    }
}
