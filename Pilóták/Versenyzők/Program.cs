using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Versenyzők
{
        struct elemek
        {
            public string elem1; //nev
            public DateTime elem2;// szül id
            public string elem3;//nemzet
            public string elem4;// rajtsz
        }

        class Program
        {
            static void print(string s) { Console.WriteLine(s.ToString()); }
            static void f(int x) { Console.WriteLine(x + ". Feladat."); }
            static void p() { Console.ReadKey(); }
            static void Main(string[] args)
            {
                string[] betxt = File.ReadAllLines(@"pilotak.csv", Encoding.UTF8);
                List<elemek> L = new List<elemek>();
                int k = 0;
                for (int i = k; i < betxt.Count(); i++)
                {
                    if (i > 0)
                    {
                        string[] adat1 = betxt[i].Split(';');
                        elemek uj = new elemek();
                        uj.elem1 = adat1[0];
                        uj.elem2 = DateTime.Parse(adat1[1]);
                        uj.elem3 = adat1[2];
                        uj.elem4 = adat1[3];
                        L.Add(uj);
                    }
                }
                f(1); f(2);
                print("Adatok beolvasva.");
                f(3);
                print("Az állomámy adattartalma: " + L.Count() + " sor.");
                f(4);
                print("Az állomány utolsó sorában szereplő személy, neve: " + L[L.Count() - 1].elem1);
                f(5);
                DateTime T = new DateTime(1901, 01, 01);//viszonyidő deklarálása
                int lk = Convert.ToInt32(L[0].elem4);//legkisebb rajtszám referencia
                string lkr = "";
                List<string> Rszamok = new List<string>() { L[0].elem4 };  //a típus litába beteszzük a másik lista 0. elemét       
                int[] db = new int[L.Count];// számoló gyűjtő tömb létrehozása töbszöri  rajtszámhoz
                List<string> TRSZ = new List<string>();//töbszöri rajtszámok listája
                for (int j = 0; j < L.Count(); j++)//Rajtszámok kigyűjtése
                { if (Rszamok.Contains(L[j].elem4) == false) { Rszamok.Add(L[j].elem4); } }
                foreach (elemek e in L)
                {
                    if (e.elem2 < T) { Console.WriteLine("\t" + e.elem1 + " (" + e.elem2.ToShortDateString() + ")"); }
                    if ((e.elem4.Length > 0) && (Convert.ToInt32(e.elem4) < lk)) { lk = Convert.ToInt32(e.elem4); lkr = e.elem3; }
                    for (int a = 0; a < Rszamok.Count() - 1; a++)
                    {
                        if (Rszamok[a] == e.elem4)
                        {
                            db[a]++;
                            if (db[a] > 1)//a rajtszám többször  van használva
                            {
                                if ((e.elem4.Length > 0) && (db[a] > 0)) { TRSZ.Add(e.elem4); }
                            }
                        }
                    }
                }
                f(6);print(lkr.ToString());
                f(7);
                for (int d = 0; d < TRSZ.Count(); d++)
                {
                    if (d != TRSZ.Count - 1) { Console.Write(TRSZ[d] + ","); }
                    else { Console.Write(TRSZ[d]); }
                }
                p();
            }
        }
    }
