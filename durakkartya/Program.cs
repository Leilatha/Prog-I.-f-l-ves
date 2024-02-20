using System;

namespace durakkartya
{
    class Program
    {
        static void Main(string[] args)
        {            
            Console.WindowHeight = 35;
            Console.WindowWidth = 120;
            string v = "";
            string w = "";
            while (w != "y")
            {
                //MENÜ
                while (v != "1" && v != "2" && v != "3" && v != "4")
                {
                    Console.Clear();
                    Console.CursorLeft = 37;
                    Console.CursorTop = 10;
                    Console.WriteLine("Üdvözöllek a Merci féle Durák kártyajátékban!");
                    Console.CursorTop = 13;
                    Console.CursorLeft = 52;                    
                    Console.WriteLine("1. Új játék");
                    Console.CursorLeft = 52;                    
                    Console.WriteLine("2. Betöltés");
                    Console.CursorLeft = 52;                    
                    Console.WriteLine("3. Játékszabály");
                    Console.CursorLeft = 52;                    
                    Console.WriteLine("4. Kilépés");
                    Console.CursorTop = 19;
                    Console.CursorLeft = 27;
                    Console.WriteLine("A megfelelő menüpont kiválasztásához használd a menüpont sorszámát!");
                    Console.CursorTop = 21;
                    Console.CursorLeft = 57;                    
                    v = Console.ReadLine();
                }
                //Új játék
                if (v == "1")
                {
                    while (w != "2" && w != "3" && w != "4" && w != "5" && w != "v")
                    {
                        Console.Clear();
                        Console.CursorLeft = 39;
                        Console.CursorTop = 10;
                        Console.WriteLine("Hány játékos módban szeretnéd játszani?");
                        Console.CursorLeft = 42;
                        Console.WriteLine("(Lehetséges módok: 2, 3, 4, 5)");
                        Console.CursorLeft = 58;
                        Console.CursorTop = 13;
                        Console.CursorTop = 30;
                        Console.CursorLeft = 25;
                        Console.WriteLine("Ha vissza akarsz térni a menübe, nyomd meg a 'v'-t majd egy 'ENTER'-t");
                        Console.CursorLeft = 60;
                        w = Console.ReadLine();
                    }
                    if (w == "v")
                    {
                        v = "";
                        w = "";
                    }
                    else
                    {
                        Jatek uj = new Jatek(int.Parse(w));
                        uj.Parti();

                        Console.CursorTop = 30;
                        Console.CursorLeft = 25;
                        Console.WriteLine("Ha vissza akarsz térni a menübe, nyomd meg a 'v'-t majd egy 'ENTER'-t");
                        Console.CursorLeft = 60;
                        w = Console.ReadLine();
                        if (w == "v")
                        {
                            v = "";
                            w = "";
                        }
                    }
                }
                //Mentett állások
                else if (v == "2")
                {
                    Console.Clear();
                    Console.CursorLeft = 39;
                    Console.CursorTop = 10;
                    Console.WriteLine("Melyik mentett állást szeretnéd betölteni?");
                    Console.CursorLeft = 40;
                    Console.WriteLine("Írd be a megfelelő mentés nevét(asd.txt)!");

                    //Lehetséges állások:
                    Jatek uj = new Jatek(2);
                    string[] fájlLista = System.IO.Directory.GetFiles(@".\", "*.txt");
                    Console.CursorTop = 13;
                    for (int i = 0; i < fájlLista.Length; i++)
                    {
                        Console.CursorLeft = 55;
                        Console.WriteLine(fájlLista[i]);
                    }

                    Console.CursorLeft = 25;
                    Console.CursorTop = 30;
                    Console.WriteLine("Ha inkább vissza akarsz térni a menübe, nyomd meg a 'v'-t majd egy 'ENTER'-t");
                    Console.CursorLeft = 60;
                    w = Console.ReadLine();
                    if (w == "v")
                    {
                        v = "";
                    }
                    else
                    {
                        uj.Betöltés(w);
                        uj.Parti();
                        Console.CursorLeft = 25;
                        Console.CursorTop = 30;
                        Console.WriteLine("Ha inkább vissza akarsz térni a menübe, nyomd meg a 'v'-t majd egy 'ENTER'-t");
                        Console.CursorLeft = 60;
                        w = Console.ReadLine();
                        if (w == "v")
                        {
                            v = "";
                        }
                    }
                }
                //Játékszabály
                else if (v == "3")
                {
                    Console.Clear();
                    Console.CursorLeft = 52;
                    Console.CursorTop = 3;
                    Console.WriteLine("Játékszabály");
                    Console.CursorLeft = 18;
                    Console.CursorTop = 7;
                    Console.WriteLine("Kártya: Francia kártya");
                    Console.CursorLeft = 18;
                    Console.WriteLine("Játék Típusa: Lapfogyasztó");
                    Console.CursorLeft = 18;
                    Console.WriteLine("Játékosok száma: 2,3,4,5");
                    Console.CursorLeft = 18;
                    Console.WriteLine("A játék célja: Lerakni a kézben lévő lapokat. A játék vesztese 'durák' lesz,");
                    Console.CursorLeft = 18;
                    Console.WriteLine("akinek utolsóként marad kártya a kezében.");
                    Console.CursorLeft = 18;
                    Console.WriteLine("A lapok rangsora a következő: Hatos, Hetes, Nyolcas, Kilences, Tizes, Bubi, Dáma, Király, Ász.");
                    Console.CursorLeft = 18;
                    Console.WriteLine("A játék menete: Az osztó - a játékosok számának függvényében - 4-4 vagy 6-6");
                    Console.CursorLeft = 18;
                    Console.WriteLine("lapot ad mindenkinek, köztük magának is. A megmaradt lapokat leteszi pakliként");
                    Console.CursorLeft = 18;
                    Console.WriteLine("az asztalra, a legalsó kártyát felfordítva meghívja az adu színét.");
                    Console.CursorLeft = 18;
                    Console.WriteLine("A haladási irányt jobbra tartva az első játékos támadja a jobbon mellette ülő");
                    Console.CursorLeft = 18;
                    Console.WriteLine("játékost, akinek meg kell ütnie a kijátszott lapot egy azonos színű, rangban");
                    Console.CursorLeft = 18;
                    Console.WriteLine("a következő lappal, vagy egy adu színű lappal. Itt kétféle lehetséges kimenet van:");
                    Console.CursorLeft = 18;
                    Console.WriteLine("1.) Ha meg tudja ütni a lapot, a két kártya bekerül az égetőbe, és az eddig védekező");
                    Console.CursorLeft = 18;
                    Console.WriteLine("játékos válik a támadóvá, és megtámadja a jobbra mellette ülő játékost egy");
                    Console.CursorLeft = 18;
                    Console.WriteLine("általa kijátszott lappal.");
                    Console.CursorLeft = 18;
                    Console.WriteLine("2.) Ha a játékos nem tudja megütni a támadó lapját, akkor azt fel kell húznia a saját");
                    Console.CursorLeft = 18;
                    Console.WriteLine("lapjai közé és rögtön ezután ő maga válik a támadóvá, és kijátszva egy lapot,");
                    Console.CursorLeft = 18;
                    Console.WriteLine("megtámadja a jobbra mellette ülő játékost.");
                    Console.CursorLeft = 18;
                    Console.WriteLine("Ha lement a kör, akkor az osztó, mindenkinek oszt egy lapot a játékosoknak a");
                    Console.CursorLeft = 18;
                    Console.WriteLine("lefordított pakliból és újabb kör indul.");
                    Console.CursorLeft = 18;
                    Console.WriteLine("A játéknak a vesztese az a játékos lesz, akinek a legvégén maradnak csak lapjai.");


                    Console.CursorLeft = 28;
                    Console.CursorTop = 32;
                    Console.WriteLine("Ha vissza akarsz térni a menübe, nyomd meg a 'v'-t majd egy 'ENTER'-t");
                    Console.CursorLeft = 60;
                    Console.CursorTop = 33;
                    w = Console.ReadLine();
                    if (w == "v")
                    {
                        v = "";
                        w = "";
                    }
                }
                //Kilépés
                else
                {
                    Console.Clear();
                    Console.CursorLeft = 45;
                    Console.CursorTop = 14;
                    Console.WriteLine("Biztos, hogy ki akarsz lépni?");
                    Console.CursorLeft = 56;
                    Console.WriteLine("(y/n)");
                    Console.CursorLeft = 58;
                    Console.CursorTop = 17;
                    w = Console.ReadLine();
                    if (w == "n")
                    {
                        v = "";
                    }
                }
            }

        }
    }
}
