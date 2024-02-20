using System;

namespace durakkartya
{
    class Jatekos
    {
        Kartya[] jatekoskartyai;
        Kartya kijatszott;
        bool aie;

        public Kartya[] Jatekoskartyai { get { return jatekoskartyai; } set { jatekoskartyai = value; } }
        public Kartya Kijatszott { get { return kijatszott; } set { kijatszott = value; } }
        public bool Aie { get { return aie; } set { aie = value; } }

        public Jatekos(int jatekosszam)//A lapok kiosztása a játékban
        {
            if (jatekosszam < 4)
            {
                //2,3 főnél 5 lap kerül kiosztásra
                Jatekoskartyai = new Kartya[6];

            }
            else
            {
                //4,5 főnél pedig 3 lap kerül kiosztásra.
                Jatekoskartyai = new Kartya[4];

            }
        }
        public Jatekos(int kartyadb,bool aie)//A lapok kiosztása a játékban
        {
            Jatekoskartyai = new Kartya[kartyadb];
            this.aie = aie;
        }
        public Kartya Tamadas(string Szín)//Az ai kiválaszt egy kártyát amivel támad
        {
            Kartya[] kivalasztott = new Kartya[Jatekoskartyai.Length];
            int db = 0;
            for (int i = 0; i < Jatekoskartyai.Length; i++)
            {
                if (Jatekoskartyai[i].Szín != Szín)
                {
                    kivalasztott[db] = Jatekoskartyai[i];
                    db++;
                }
            }

            if (db != 0)
            {
                // ha van legalább egy olyan lapja, ami nem adu, akkor kiválasztjuk belőlük a legkisebb értékűt, és azzal támad
                int min = 0;
                for (int i = 0; i < db; i++)
                {
                    if (kivalasztott[i].Erősség < kivalasztott[min].Erősség)
                    {
                        min = i;
                    }
                }
                Kartyalerakó(kivalasztott[min]);
                return kivalasztott[min];
            }
            else
            {
                //Ha csak aduja van
                int min = 0;
                for (int i = 0; i < Jatekoskartyai.Length; i++)
                {
                    if (Jatekoskartyai[i].Erősség < Jatekoskartyai[min].Erősség)
                    {
                        min = i;
                    }
                }
                Kartya seged = Jatekoskartyai[min];
                Kartyalerakó(Jatekoskartyai[min]);
                return seged;
            }
        }
        public Kartya Vedekezes(string Szín, Kartya tamado)//Az ai kiválaszt egy kártyát amivel védekezik
        {
            Kartya[] kivalasztott = new Kartya[Jatekoskartyai.Length];
            int db = -1;
            int dbadu = kivalasztott.Length;
            for (int i = 0; i < Jatekoskartyai.Length; i++)
            {
                if (tamado.Szín == Szín)
                {
                    if (Jatekoskartyai[i].Szín == tamado.Szín && tamado.Erősség < Jatekoskartyai[i].Erősség)
                    {
                        db++;
                        kivalasztott[db] = Jatekoskartyai[i];
                    }
                }
                else
                {
                    //megnézzük, hogy van e olyan színű és megfelelő erősségű lapunk amivel tudunk a támadó ellen védekezni                
                    if (Jatekoskartyai[i].Szín == tamado.Szín && tamado.Erősség < Jatekoskartyai[i].Erősség)
                    {
                        db++;
                        kivalasztott[db] = Jatekoskartyai[i];
                    }
                    //megnézzük, hogy van e adu színű lapunk, amivel tudunk védekezni a támadó ellen
                    if (Jatekoskartyai[i].Szín == Szín)
                    {
                        dbadu--;
                        kivalasztott[dbadu] = Jatekoskartyai[i];
                    }
                }
            }

            if (db != -1)
            {
                //A nem adu színű, megfelelő erősségű lapokból kiválasztjuk a legkisebbet, és azzal védekezünk
                int min = 0;
                for (int i = 1; i <= db; i++)
                {
                    if (kivalasztott[i].Erősség < kivalasztott[min].Erősség)
                    {
                        min = i;
                    }
                }
                Kartyalerakó(kivalasztott[min]);
                return kivalasztott[min];
            }

            if (dbadu != kivalasztott.Length)
            {
                //Adu színűből a legkisebbet kiválasztva védekezik
                int min = dbadu;
                for (int i = dbadu + 1; i < kivalasztott.Length; i++)
                {
                    if (kivalasztott[i].Erősség < kivalasztott[min].Erősség)
                    {
                        min = i;
                    }
                }
                Kartyalerakó(kivalasztott[min]);
                return kivalasztott[min];
            }

            //Ha nincs se lapunk se adutunk a védekezéshez
            Kartyafelhúzó(tamado);
            return null;
        }
        public Kartya Tetamadsz(string valsztott)
        {
            string[] val = valsztott.Split(' ');
            Kartya valasztott = null;
            for (int i = 0; i < Jatekoskartyai.Length; i++)
            {
                if (val[0] == Jatekoskartyai[i].Szín && val[1] == Jatekoskartyai[i].Szám)
                {
                    valasztott = Jatekoskartyai[i];
                }
            }
            Kartyalerakó(valasztott);
            return valasztott;

        }
        public Kartya Tevedekezel(string Szín, Kartya tamado, string valsztott)
        {
            //Új tömbbe tesszük a játékosnak azokat a kártyákat amik közül védekezhet:
            Kartya[] kivalasztott = new Kartya[Jatekoskartyai.Length];
            int db = -1;
            for (int i = 0; i < Jatekoskartyai.Length; i++)
            {
                //megnézzük, hogy van e olyan lapunk amivel tudunk a támadó ellen védekezni       
                if (tamado.Szín == Szín)
                {
                    if (Jatekoskartyai[i].Szín == tamado.Szín && tamado.Erősség < Jatekoskartyai[i].Erősség)
                    {
                        db++;
                        kivalasztott[db] = Jatekoskartyai[i];
                    }
                }
                else
                {
                    if (Jatekoskartyai[i].Szín == tamado.Szín && tamado.Erősség < Jatekoskartyai[i].Erősség)
                    {
                        db++;
                        kivalasztott[db] = Jatekoskartyai[i];
                    }
                    if (Jatekoskartyai[i].Szín == Szín)
                    {
                        db++;
                        kivalasztott[db] = Jatekoskartyai[i];
                    }
                }
            }

            if (db != -1)
            {
                string[] val =  valsztott.Split(' ');
                Kartya valasztott = null;
                for (int i = 0; i < Jatekoskartyai.Length; i++)
                {
                    if (val[0] == Jatekoskartyai[i].Szín && val[1] == Jatekoskartyai[i].Szám)
                    {
                        valasztott = Jatekoskartyai[i];
                    }
                }
                Kartyalerakó(valasztott);
                return valasztott;
            }
            else
            {
                Kartyafelhúzó(tamado);
                return null;
            }
        }
        private void Kartyalerakó(Kartya kijatszott)
        {
            Kartya[] seged = new Kartya[Jatekoskartyai.Length - 1];
            int db = 0;
            for (int i = 0; i < Jatekoskartyai.Length; i++)
            {
                if (Jatekoskartyai[i] != kijatszott)
                {
                    seged[db] = Jatekoskartyai[i];
                    db++;
                }
            }
            Jatekoskartyai = seged;
        }
        private void Kartyafelhúzó(Kartya tamado)
        {
            Kartya[] seged = new Kartya[Jatekoskartyai.Length + 1];
            for (int i = 0; i < Jatekoskartyai.Length; i++)
            {
                seged[i] = Jatekoskartyai[i];
            }
            seged[seged.Length - 1] = tamado;

            Jatekoskartyai = seged;
        }
        public bool LehetségesLapok(string Szín, Kartya tamado)
        {
            //Új tömbbe tesszük a játékosnak azokat a kártyákat amik közül védekezhet:
            bool van = false;
            Kartya[] kivalasztott = new Kartya[Jatekoskartyai.Length];
            int db = -1;
            for (int i = 0; i < Jatekoskartyai.Length; i++)
            {
                //megnézzük, hogy van e olyan lapunk amivel tudunk a támadó ellen védekezni       
                if (tamado.Szín == Szín)
                {
                    if (Jatekoskartyai[i].Szín == tamado.Szín && tamado.Erősség < Jatekoskartyai[i].Erősség)
                    {
                        db++;
                        kivalasztott[db] = Jatekoskartyai[i];
                    }
                }
                else
                {
                    if (Jatekoskartyai[i].Szín == tamado.Szín && tamado.Erősség < Jatekoskartyai[i].Erősség)
                    {
                        db++;
                        kivalasztott[db] = Jatekoskartyai[i];
                    }
                    if (Jatekoskartyai[i].Szín == Szín)
                    {
                        db++;
                        kivalasztott[db] = Jatekoskartyai[i];
                    }
                }
            }

            if (db != -1)
            {
                //Kiiratjuk a játékosnak hogy melyikek közül választhat:            
                Console.CursorLeft = 62;
                Console.CursorTop = 31;
                Console.WriteLine("Ezekkel védekezhetsz:");
                Console.CursorLeft = 62;
                for (int i = 0; i <= db; i++)
                {
                    Console.Write(kivalasztott[i].Kiiratas() + " ");
                }
                van = true;
            }
            else
            {
                van = false;
            }
            return van;
        }
        public bool BenneVanE(string szin, string szam)
        {
            Kartya valasztott = new Kartya(szin, szam, 0);
            for (int i = 0; i < Jatekoskartyai.Length; i++)
            {
                if (valasztott.Szín == Jatekoskartyai[i].Szín && valasztott.Szám == Jatekoskartyai[i].Szám)
                {
                    return true;
                }
            }
            return false;
        }
        public bool Eldöntés(string szin, string szam, Kartya tamado, string Szín)
        {
            Kartya seged = new Kartya(szin, szam, 0);
            Kartya[] kivalasztott = new Kartya[Jatekoskartyai.Length];
            int db = -1;
            for (int i = 0; i < Jatekoskartyai.Length; i++)
            {
                //megnézzük, hogy van e olyan lapunk amivel tudunk a támadó ellen védekezni       
                if (tamado.Szín == Szín)
                {
                    if (Jatekoskartyai[i].Szín == tamado.Szín && tamado.Erősség < Jatekoskartyai[i].Erősség)
                    {
                        db++;
                        kivalasztott[db] = Jatekoskartyai[i];
                    }
                }
                else
                {
                    if (Jatekoskartyai[i].Szín == tamado.Szín && tamado.Erősség < Jatekoskartyai[i].Erősség)
                    {
                        db++;
                        kivalasztott[db] = Jatekoskartyai[i];
                    }
                    if (Jatekoskartyai[i].Szín == Szín)
                    {
                        db++;
                        kivalasztott[db] = Jatekoskartyai[i];
                    }
                }
            }

            if (db != -1)
            {
                for (int i = 0; i < kivalasztott.Length; i++)
                {
                    if (kivalasztott[i] != null)
                    {
                        if (kivalasztott[i].Szín == szin && kivalasztott[i].Szám == szam)
                        {
                            return true;
                        }
                    }                    
                }
                return false;
            }
            else
            {
                return false;
            }
            
        }

    }
}
