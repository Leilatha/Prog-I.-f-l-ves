using System;

namespace durakkartya
{
    class Pakli
    {
        static Random rnd = new Random();

        Kartya[] kartyak;
        string[] színek = { "Kőr", "Pikk", "Káró", "Treff"};
        string[] számok = { "Hatos", "Hetes", "Nyolcas", "Kilences", "Tizes", "Bubi", "Dáma", "Király", "Ász"};
        int kartyaDB;

        public Kartya[] Kartyak { get { return kartyak; } set { kartyak = value; } }        
        public int KartyaDB { get { return kartyaDB; } }
        public string[] Színek { get { return színek; } set { színek = value; } }
        public string[] Számok { get { return számok; } set { számok = value; } }

        public Pakli()
        {
            kartyaDB = 0;
            kartyak = new Kartya[36];
            for (int i = 0; i < színek.Length; i++)
            {
                for (int j = 0; j < számok.Length; j++)
                {
                    kartyak[kartyaDB] = new Kartya(színek[i], számok[j], j + 1);
                    kartyaDB++;
                }
            }
            Keveres(kartyak);
        }       
        public Pakli(Kartya[] betöltött)
        {
            kartyaDB = 0;
            kartyak = betöltött;
        }
        private void Kartyacsere(int x, int y)
        {
            Kartya seged = kartyak[x];
            kartyak[x] = kartyak[y];
            kartyak[y] = seged;
        }
        private void Keveres(Kartya[] kartyak)
        {
            int maxkeveres = 250;
            for (int i = 0; i < maxkeveres; i++)
            {
                Kartyacsere(rnd.Next(0,kartyak.Length), rnd.Next(0, kartyak.Length));
            }
        }
    }

    class Kartya
    {
        string szín;
        string szám;
        int erősség;

        
        public string Szín { get { return szín; } set { szín = value; } }
        public string Szám { get { return szám; } set { szám = value; } }
        public int Erősség { get { return erősség; } set { erősség = value; } }

        //Konstruktor
        public Kartya(string szín, string szám, int erősség)
        {
            this.szín = szín;
            this.szám = szám;
            this.erősség = erősség;
        }        
        public string Kiiratas()
        {
            string s = "";
            if (szín == "Kőr")
            {
                s += "\u2665";
            }
            else if (szín == "Pikk")
            {
                s += "\u2660";
            }
            else if (szín == "Káró")
            {
                s += "\u2666";
            }
            else
            {
                s += "\u2663";
            }

            s += " " + szám;
            return s;
        }
        public string SzinKiiratas()
        {
            string s = "";
            if (szín == "Kőr")
            {
                s += "\u2665";
            }
            else if (szín == "Pikk")
            {
                s += "\u2660";
            }
            else if (szín == "Káró")
            {
                s += "\u2666";
            }
            else
            {
                s += "\u2663";
            }           
            return s;
        }

    }
}
