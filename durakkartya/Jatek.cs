using System;
using System.IO;

namespace durakkartya
{
    class Jatek
    {
        //Adataink:                         pakliindex
        Pakli pakli; // Kör Nyolc, Káró Bubi, Treff 9, Pikk 8, Kör Ász,......
        Kartya adu;
        //Pakli egeto; // ahová kerülnek a leütött kártyapárosok
        Jatekos[] jatekosok;
        Jatekos nyertes;
        int pakliindex; // a pakli legfelső kártyájára mutató index        
        int kezdo;
        int tamadoind;
        int vedoind;
        int emberindx;
        string menu;
        static Random rnd = new Random();

        //Tulajdonságok

        //Konstruktor
        public Jatek(int jatekosszam)
        {
            //Játékosok létrehozása és a kártyák kiosztása nekik
            jatekosok = new Jatekos[jatekosszam];
            pakli = new Pakli();
            pakliindex = 0;
            int ember = rnd.Next(0, jatekosszam);
            for (int i = 0; i < jatekosszam; i++)
            {
                jatekosok[i] = new Jatekos(jatekosszam);
                for (int j = 0; j < jatekosok[i].Jatekoskartyai.Length; j++)
                {
                    jatekosok[i].Jatekoskartyai[j] = pakli.Kartyak[pakliindex];
                    pakliindex++;
                }
                if (i == ember)
                {
                    jatekosok[i].Aie = false;
                }
                else
                {
                    jatekosok[i].Aie = true;
                }
            }
            adu = pakli.Kartyak[pakli.Kartyak.Length - 1];
        }

        //Függvények
        public void Parti()
        {
            //Megtudni hanyas indexen áll a játékos
            emberindx = 0;
            for (int i = 0; i < jatekosok.Length; i++)
            {
                if (jatekosok[i].Aie == false)
                {
                    emberindx = i;
                }
            }

            //Felkészítés
            kezdo = rnd.Next(0, jatekosok.Length);
            tamadoind = kezdo;
            vedoind = tamadoind + 1;
            Kiiratas();
            Console.CursorLeft = 48;
            Console.CursorTop = 29;
            if (kezdo == emberindx)
            {
                Console.WriteLine("Te kezdesz!");
                System.Threading.Thread.Sleep(3500);
                Kiiratas();
            }
            else
            {
                Console.WriteLine($"A kezdő: {kezdo + 1}. játékos");
                System.Threading.Thread.Sleep(3500);
                Kiiratas();
            }
            //A JÁTÉK
            while (!Vege() && menu != "2")
            {
                if (tamadoind >= jatekosok.Length)
                {
                    tamadoind = 0;
                }
                if (vedoind >= jatekosok.Length)
                {
                    vedoind = 0;
                }

                Kiiratas();
                //TÁMADÁSOK-VÉDÉSEK
                if (tamadoind != emberindx)//ai támad
                {
                    if (tamadoind != emberindx && vedoind != emberindx)//ai védekezik
                    {
                        Console.CursorLeft = 40;
                        Console.CursorTop = 29;
                        Console.WriteLine($"A(z) {tamadoind + 1}. játékos megtámadta a(z) {vedoind + 1}. játékost.");
                        Kartya tamado = jatekosok[tamadoind].Tamadas(adu.Szín);
                        Console.CursorLeft = 47;
                        Console.CursorTop = 16;
                        Console.WriteLine($"A támadó lap: {tamado.Kiiratas()}");
                        System.Threading.Thread.Sleep(3500);
                        Kartya vedo = jatekosok[vedoind].Vedekezes(adu.Szín, tamado);
                        Console.CursorLeft = 47;
                        Console.CursorTop = 17;
                        if (vedo == null)
                        {
                            Console.WriteLine($"A védő lap: -");
                        }
                        else
                        {
                            Console.WriteLine($"A védő lap: {vedo.Kiiratas()}");
                        }
                        Console.CursorLeft = 40;
                        Console.CursorTop = 29;
                        Utes(tamado, vedo, adu);
                    }
                    if (tamadoind != emberindx && vedoind == emberindx)//Játékos védekezik
                    {
                        Kartya tamado = jatekosok[tamadoind].Tamadas(adu.Szín);
                        System.Threading.Thread.Sleep(1000);
                        Console.CursorLeft = 40;
                        Console.CursorTop = 29;
                        Console.WriteLine($"A(z) {tamadoind + 1}. játékos megtámadott téged.");
                        Console.CursorLeft = 47;
                        Console.CursorTop = 16;
                        Console.WriteLine($"A támadó lap: {tamado.Kiiratas()}");
                        string s = "";
                        if (jatekosok[vedoind].LehetségesLapok(adu.Szín, tamado))
                        {
                            s = Felhasznalofelulet("vedekezes", tamado);
                        }
                        if (s != "2")
                        {
                            Kartya vedo = jatekosok[vedoind].Tevedekezel(adu.Szín, tamado, s);
                            Console.CursorLeft = 47;
                            Console.CursorTop = 17;
                            if (vedo == null)
                            {
                                Console.WriteLine($"A védő lap: -");
                            }
                            else
                            {
                                Console.WriteLine($"A védő lap: {vedo.Kiiratas()}");
                            }
                            Console.CursorLeft = 40;
                            Console.CursorTop = 29;
                            Utes(tamado, vedo, adu);
                        }
                    }
                }
                if (tamadoind == emberindx && vedoind != emberindx)//Játékos támad és ai védekezik
                {
                    Console.CursorLeft = 47;
                    Console.CursorTop = 29;
                    Console.WriteLine("Te támadsz");
                    string s = Felhasznalofelulet("tamadas", null);
                    if (s != "2")
                    {
                        Kartya tamado = jatekosok[tamadoind].Tetamadsz(s);
                        Console.CursorLeft = 47;
                        Console.CursorTop = 29;
                        Console.WriteLine($"Megtámadtad a(z) {vedoind + 1}. játékost.");
                        Console.CursorLeft = 47;
                        Console.CursorTop = 16;
                        Console.WriteLine($"A támadó lap: {tamado.Kiiratas()}");
                        System.Threading.Thread.Sleep(3500);
                        Kartya vedo = jatekosok[vedoind].Vedekezes(adu.Szín, tamado);
                        Console.CursorLeft = 47;
                        Console.CursorTop = 17;
                        if (vedo == null)
                        {
                            Console.WriteLine($"A védő lap: -");
                        }
                        else
                        {
                            Console.WriteLine($"A védő lap: {vedo.Kiiratas()}");
                        }
                        Console.CursorLeft = 40;
                        Console.CursorTop = 29;
                        Utes(tamado, vedo, adu);
                    }
                }
                if (menu != "2")
                {
                    System.Threading.Thread.Sleep(3500);
                }                
                Kiiratas();
                //NYERÉSEK VIZSGÁLATA
                if (jatekosok[tamadoind].Jatekoskartyai.Length == 0)
                {
                    Nyeres(tamadoind);
                    if (tamadoind != jatekosok.Length) // hogyha a nyertes támadó volt és nem a tömb végén volt
                    {
                        tamadoind--;
                        vedoind--;
                    }
                }
                if (vedoind == -1)//Ha a támadó az utolsó helyen volt és a védő az elsőn, és mindketten egyszerre nyertek
                {
                    vedoind = 0;
                    if (jatekosok[vedoind].Jatekoskartyai.Length == 0)
                    {
                        Nyeres(vedoind);
                        if (vedoind >= jatekosok.Length)// hogyha a nyertes védő volt és a tömb végén volt
                        {
                            tamadoind = -1;
                            vedoind = 0;
                        }
                    }
                    vedoind--;
                }
                else
                {
                    if (jatekosok[vedoind].Jatekoskartyai.Length == 0)
                    {
                        Nyeres(vedoind);
                        if (vedoind >= jatekosok.Length)// hogyha a nyertes védő volt és a tömb végén volt
                        {
                            tamadoind = -1;
                            vedoind = 0;
                        }
                    }
                }
                //OSZTÁS
                if (vedoind == kezdo && pakliindex < pakli.Kartyak.Length && !Vege())
                {
                    Osztas();
                }
                Kiiratas();
                tamadoind++;
                vedoind++;
            }

            //VÉGE
            Console.Clear();
            Console.CursorTop = 15;
            if (menu == "2")
            {
                Console.CursorLeft = 49;
                Console.Write("Feladtad a játékot!");
            }
            else if (nyertes.Aie == true)
            {
                Console.CursorLeft = 42;
                Console.Write("Te lettél a Durák! Vesztettél!");
            }
            else
            {
                Console.CursorLeft = 50;
                Console.Write("Gratulálok! Nyertél!");
            }
            System.Threading.Thread.Sleep(3500);
        }
        private void Utes(Kartya tamado, Kartya vedo, Kartya adu)
        {
            if (vedo == null)
            {
                //A Jatekos részben már meg van írva a támadólap felhúzása
                Console.CursorLeft = 35;
                if (vedoind != emberindx)
                {
                    Console.WriteLine($"A(z) {vedoind + 1}. játékosnak nem sikerült megvédenie magát, ezért felhúzta a támadólapot.");
                }
                else
                {

                    Console.WriteLine("Nem sikerült megvédened magad, ezért fel kell húznod a támadólapot.");
                }
                return;
            }
            if (tamado.Szín == vedo.Szín || vedo.Szín == adu.Szín)
            {                
                if (vedoind != emberindx)
                {
                    Console.WriteLine($"A(z) {vedoind + 1}. játékosnak sikerült megvédenie magát.");
                }
                else
                {
                    Kiiratas();
                    Console.WriteLine("Sikerült megvédened magad!");
                }
            }
        }
        private void Osztas()
        {
            int i = 0;
            while (pakliindex < pakli.Kartyak.Length && i < jatekosok.Length)
            {
                Kartya[] uj = new Kartya[jatekosok[i].Jatekoskartyai.Length + 1];
                for (int j = 0; j < jatekosok[i].Jatekoskartyai.Length; j++)
                {
                    uj[j] = jatekosok[i].Jatekoskartyai[j];
                }
                uj[uj.Length - 1] = pakli.Kartyak[pakliindex];
                pakliindex++;
                jatekosok[i].Jatekoskartyai = uj;
                i++;
            }
        }
        private void Nyeres(int index)
        {
            if (jatekosok[index].Aie == false)
            {
                if (index >= jatekosok.Length)
                {
                    kezdo = jatekosok.Length - 1;
                }
                else
                {
                    kezdo--;
                }
            }
            if (jatekosok[index].Aie == true && jatekosok.Length > 2)
            {
                Jatekostorles(index);
                if (kezdo == index && index >= jatekosok.Length)
                {
                    kezdo = jatekosok.Length - 1;
                }
                else if (index > kezdo)
                {

                }
                else
                {
                    kezdo--;
                }
                Console.CursorLeft = 55;
                Console.CursorTop = 29;
                Console.WriteLine($"A(z) {index + 1}. játékos nyert!");
                System.Threading.Thread.Sleep(3500);
                EmberKeres();
                if (kezdo < 0)
                {
                    kezdo = 0;
                }
            }
        }
        private void EmberKeres()
        {
            for (int i = 0; i < jatekosok.Length; i++)
            {
                if (jatekosok[i].Aie == false)
                {
                    emberindx = i;
                }
            }
        }
        private void Jatekostorles(int index)
        {
            Jatekos[] seged = new Jatekos[jatekosok.Length - 1];
            int db = 0;
            for (int i = 0; i < jatekosok.Length; i++)
            {
                if (jatekosok[i] != jatekosok[index])
                {
                    seged[db] = jatekosok[i];
                    db++;
                }
            }
            jatekosok = seged;
        }
        private void Kiiratas()
        {
            Console.Clear();
            Console.CursorLeft = 49;
            Console.CursorTop = 2;
            Console.WriteLine("1. Mentés || 2. Feladás");
            Console.CursorLeft = 24;
            Console.WriteLine("(Amikor te következel, a megfelelő sorszámmal tudsz menüpontot választani!)");


            Console.CursorLeft = 4;
            Console.CursorTop = 9;
            Console.WriteLine($"Az osztás a(z) {kezdo + 1}.");
            Console.CursorLeft = 4;
            Console.WriteLine("játékosnál történik!");


            Console.CursorLeft = 4;
            Console.CursorTop = 16;
            if (pakliindex == pakli.Kartyak.Length)
            {
                Console.WriteLine($"Az adu: {adu.SzinKiiratas()}");
            }
            else
            {
                Console.WriteLine($"Az adu: {adu.Kiiratas()}");
            }


            Console.CursorLeft = 4;
            Console.WriteLine("A pakli megmaradt");
            Console.CursorLeft = 4;
            Console.WriteLine($"lapjainak száma: {pakli.Kartyak.Length - pakliindex}");


            Console.CursorLeft = 59;
            Console.CursorTop = 16;



            Console.CursorLeft = 4;
            Console.CursorTop = 31;
            Console.WriteLine("A kártyáid:");
            Console.CursorLeft = 4;
            for (int i = 0; i < jatekosok[emberindx].Jatekoskartyai.Length; i++)
            {
                Console.Write(jatekosok[emberindx].Jatekoskartyai[i].Kiiratas() + " ");
            }

            Console.CursorTop = 12;
            for (int i = 0; i < jatekosok.Length; i++)
            {
                if (tamadoind == i)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                if (vedoind == i)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                if (jatekosok[i].Aie == true)
                {
                    Console.CursorLeft = 85;
                    Console.WriteLine($"{i + 1}. játékos lapjainak száma: {jatekosok[i].Jatekoskartyai.Length}");
                }
                else
                {
                    Console.CursorLeft = 85;
                    Console.WriteLine($"{i + 1}. játékos : Te");
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.CursorTop = 30;
            Console.CursorLeft = 65;
        }
        private bool Hulyevedelem(string s, Kartya tamado)
        {
            if (!s.Contains(" "))
            {
                if (s == "1" || s == "2")
                {
                    return true;
                }
                return false;
            }
            else
            {
                string[] val = s.Split(' ');
                if (val.Length == 2)
                {
                    if (val[0] == "Treff" || val[0] == "Káró" || val[0] == "Kőr" || val[0] == "Pikk")
                    {
                        if (val[1] == "Hatos" || val[1] == "Hetes" || val[1] == "Nyolcas" || val[1] == "Kilences"
                            || val[1] == "Tizes" || val[1] == "Bubi" || val[1] == "Dáma" || val[1] == "Király" || val[1] == "Ász")
                        {
                            if (tamado == null)
                            {
                                if (jatekosok[emberindx].BenneVanE(val[0], val[1]) == false)
                                {
                                    Console.CursorLeft = 65;
                                    Console.CursorTop = 33;
                                    Console.WriteLine("Csak a paklid lapjai közül választhatsz!");
                                    System.Threading.Thread.Sleep(1500);
                                    return false;
                                }
                                else
                                {
                                    return true;
                                }
                                //return true;
                            }
                            
                            else if (jatekosok[emberindx].Eldöntés(val[0], val[1], tamado, adu.Szín) == true)
                            {
                                return true;
                            }                            
                            else
                            {
                                Console.CursorLeft = 62;
                                Console.CursorTop = 33;
                                Console.WriteLine("Csak a kiválasztott kártyák közül védekezhetsz!");
                                System.Threading.Thread.Sleep(1500);
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }        
        private string Felhasznalofelulet(string s, Kartya tamado)
        {
            menu = "";
            string valsztott = "";
            if (s == "tamadas")
            {
                Console.CursorLeft = 77;
                Console.CursorTop = 31;
                Console.WriteLine("Mivel szeretnél támadni? (Pl. Treff Bubi)");
                Console.CursorTop = 32;
                Console.CursorLeft = 93;
                valsztott = Console.ReadLine();
                while (!Hulyevedelem(valsztott, tamado))
                {
                    Kiiratas();
                    Console.CursorLeft = 77;
                    Console.CursorTop = 31;
                    Console.WriteLine("Mivel szeretnél támadni? (Pl. Treff Bubi)");
                    Console.CursorLeft = 85;
                    Console.CursorTop = 33;
                    Console.WriteLine("Nem megfelelő formátum!");
                    Console.CursorTop = 32;
                    Console.CursorLeft = 93;
                    valsztott = Console.ReadLine();
                    menu = valsztott;
                }
                menu = valsztott;
                // menüt nyomtak
                if (menu == "1")
                {
                    //Mentés funkció

                    Console.Clear();
                    Console.CursorLeft = 47;
                    Console.CursorTop = 10;
                    Console.WriteLine("Milyen néven kívánsz menteni?");
                    Console.CursorLeft = 55;
                    Console.CursorTop = 15;
                    Mentes(Console.ReadLine());

                    Kiiratas();
                    //Támadás
                    Console.CursorLeft = 77;
                    Console.CursorTop = 31;
                    Console.WriteLine("Mivel szeretnél támadni? (Pl. Treff Bubi)");
                    Console.CursorTop = 32;
                    Console.CursorLeft = 93;
                    valsztott = Console.ReadLine();
                    while (!Hulyevedelem(valsztott, tamado))
                    {
                        Kiiratas();
                        Console.CursorLeft = 77;
                        Console.CursorTop = 31;
                        Console.WriteLine("Mivel szeretnél támadni? (Pl. Treff Bubi)");
                        Console.CursorLeft = 85;
                        Console.CursorTop = 33;
                        Console.WriteLine("Nem megfelelő formátum!");
                        Console.CursorTop = 32;
                        Console.CursorLeft = 93;
                        valsztott = Console.ReadLine();
                        menu = valsztott;
                    }
                }
            }
            else if (s == "vedekezes")
            {
                //Bekérjük a megfelelő lapot
                
                jatekosok[vedoind].LehetségesLapok(adu.Szín, tamado);
                Console.CursorLeft = 85;
                Console.CursorTop = 31;
                valsztott = Console.ReadLine();
                while (!Hulyevedelem(valsztott, tamado))
                {
                    Kiiratas();
                    Console.CursorLeft = 40;
                    Console.CursorTop = 29;
                    Console.WriteLine($"A(z) {tamadoind + 1}. játékos megtámadott téged.");

                    Console.CursorLeft = 47;
                    Console.CursorTop = 16;
                    Console.WriteLine($"A támadó lap: {tamado.Kiiratas()}");
                    jatekosok[vedoind].LehetségesLapok(adu.Szín, tamado);

                    Console.CursorLeft = 85;
                    Console.CursorTop = 30;
                    Console.WriteLine("Nem megfelelő formátum!");
                    Console.CursorTop = 31;
                    Console.CursorLeft = 85;
                    valsztott = Console.ReadLine();
                    menu = valsztott;
                }
                menu = valsztott;
                // menüt nyomtak
                if (menu == "1")
                {
                    //Mentés funkció
                    Console.Clear();
                    Console.CursorLeft = 47;
                    Console.CursorTop = 10;
                    Console.WriteLine("Milyen néven kívánsz menteni?");
                    Console.CursorLeft = 55;
                    Console.CursorTop = 15;
                    Mentes(Console.ReadLine());


                    Kiiratas();
                    //védekezés
                    Console.CursorLeft = 40;
                    Console.CursorTop = 29;
                    Console.WriteLine($"A(z) {tamadoind + 1}. játékos megtámadott téged.");

                    Console.CursorLeft = 47;
                    Console.CursorTop = 16;
                    Console.WriteLine($"A támadó lap: {tamado.Kiiratas()}");
                    jatekosok[vedoind].LehetségesLapok(adu.Szín, tamado);
                    Console.CursorLeft = 85;
                    Console.CursorTop = 31;
                    valsztott = Console.ReadLine();
                    while (!Hulyevedelem(valsztott, tamado))
                    {
                        Kiiratas();
                        Console.CursorLeft = 40;
                        Console.CursorTop = 29;
                        Console.WriteLine($"A(z) {tamadoind + 1}. játékos megtámadott téged.");

                        Console.CursorLeft = 47;
                        Console.CursorTop = 16;
                        Console.WriteLine($"A támadó lap: {tamado.Kiiratas()}");
                        jatekosok[vedoind].LehetségesLapok(adu.Szín, tamado);

                        Console.CursorLeft = 85;
                        Console.CursorTop = 30;
                        Console.WriteLine("Nem megfelelő formátum!");
                        Console.CursorTop = 31;
                        Console.CursorLeft = 85;
                        valsztott = Console.ReadLine();
                        menu = valsztott;
                    }
                }
            }
            return valsztott;
        }
        private void Mentes(string v)
        {
            string s = "";
            // első sor
            s += pakli.Kartyak.Length + "|" + jatekosok.Length + "|" + adu.Szín + " " + adu.Szám + " " + adu.Erősség + "|" + pakliindex + "|" + kezdo + "|" + tamadoind + "|" + vedoind + "|" + emberindx + "\n";
            //pakli kártyái
            for (int i = 0; i < pakli.Kartyak.Length; i++)
            {
                if (i == pakli.Kartyak.Length - 1)
                {
                    s += pakli.Kartyak[i].Szín + " " + pakli.Kartyak[i].Szám + " " + pakli.Kartyak[i].Erősség;
                }
                else
                {
                    s += pakli.Kartyak[i].Szín + " " + pakli.Kartyak[i].Szám + " " + pakli.Kartyak[i].Erősség + ",";
                }
            }
            s += "\n";
            //játékosok és kártyáik
            for (int i = 0; i < jatekosok.Length; i++)
            {
                s += jatekosok[i].Aie + "|";
                for (int j = 0; j < jatekosok[i].Jatekoskartyai.Length; j++)
                {
                    if (j == jatekosok[i].Jatekoskartyai.Length - 1)
                    {
                        s += jatekosok[i].Jatekoskartyai[j].Szín + " " + jatekosok[i].Jatekoskartyai[j].Szám + " " + jatekosok[i].Jatekoskartyai[j].Erősség;
                    }
                    else
                    {
                        s += jatekosok[i].Jatekoskartyai[j].Szín + " " + jatekosok[i].Jatekoskartyai[j].Szám + " " + jatekosok[i].Jatekoskartyai[j].Erősség + ",";
                    }
                }
                s += "\n";
            }

            File.WriteAllText(v + ".txt", s);
        }
        public void Betöltés(string v)
        {
            //első sor
            string[] adatsor = File.ReadAllLines(v+".txt");
            string[] sor = adatsor[0].Split('|');
            int paklihossz = int.Parse(sor[0]);
            int jatekoshossz = int.Parse(sor[1]);
            string[] adutt = sor[2].Split(' ');

            this.pakliindex = int.Parse(sor[3]);
            this.kezdo = int.Parse(sor[4]);
            this.tamadoind = int.Parse(sor[5]);
            this.vedoind = int.Parse(sor[6]);
            this.emberindx = int.Parse(sor[7]);
            this.adu = new Kartya(adutt[0], adutt[1], int.Parse(adutt[2]));
            this.menu = "";            

            //A pakli kártyái
            Kartya[] vmi = new Kartya[paklihossz];
            sor = adatsor[1].Split(',');
            for (int i = 0; i < paklihossz; i++)
            {
                string[] kartya = sor[i].Split(' ');
                Kartya seged = new Kartya(kartya[0], kartya[1], int.Parse(kartya[2]));
                vmi[i] = seged;
            }
            this.pakli = new Pakli(vmi);

            //A játékosok és kártyáik
            Jatekos[] aktuálsiJátékos = new Jatekos[jatekoshossz];
            int db = 0;
            for (int i = 2; i < adatsor.Length; i++)
            {
                //egy játékos
                sor = adatsor[i].Split('|');
                bool aie = bool.Parse(sor[0]);
                string[] játékosKártyái = sor[1].Split(',');
                Jatekos jatekos = new Jatekos(játékosKártyái.Length, aie);
                //játékos kártyái
                for (int j = 0; j < játékosKártyái.Length; j++)
                {
                    string[] kartya = játékosKártyái[j].Split(' ');
                    jatekos.Jatekoskartyai[j] = new Kartya(kartya[0], kartya[1], int.Parse(kartya[2]));
                }
                aktuálsiJátékos[db] = jatekos;
                db++;
            }
            jatekosok = aktuálsiJátékos;

        }
        private bool Vege()
        {
            bool vége = false;
            if (jatekosok.Length == 2)
            {
                for (int i = 0; i < jatekosok.Length; i++)
                {
                    if (jatekosok[i].Aie == false && jatekosok[i].Jatekoskartyai.Length == 0) //ember és nincs több kártyája
                    {
                        vége = true;
                        nyertes = jatekosok[i];
                    }
                    if (jatekosok[i].Aie == true && jatekosok[i].Jatekoskartyai.Length == 0) //ember és nincs több kártyája
                    {
                        vége = true;
                        nyertes = jatekosok[i];
                    }
                }
            }
            else
            {
                // ha 2-nél többen vannak
                if (jatekosok[emberindx].Jatekoskartyai.Length == 0)
                {
                    vége = true;
                    nyertes = jatekosok[emberindx];
                }
            }
            return vége;
        }
    }

}
