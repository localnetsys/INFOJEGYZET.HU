using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Timers;
using System.Threading.Tasks;

namespace hiányzások
{
    class Program
    {
        struct tanulok
        {
            public string nev;
            public string osztály;
            public int nap1;
            public int napU;
            public int Lóg;
        }
        static void Main(string[] args)
        {
            List<tanulok> LISTAtan = new List<tanulok>();
            string[] txt = File.ReadAllLines(@"szeptember.csv", Encoding.UTF8);   //ansi =UTF7

            for (int s = 1; s < txt.Count(); s++)
            {
                if (txt[s].Length > 0)
                {
                   string[] so = txt[s].Split(';');
                   tanulok adat = new tanulok();

                    adat.nev = so[0];
                    adat.osztály = so[1];
                    adat.nap1 = Convert.ToInt32(so[2]);
                    adat.napU = Convert.ToInt32(so[3]);
                    adat.Lóg = Convert.ToInt32(so[4]);
                    LISTAtan.Add(adat);
                }
            }
            bool találat = false;
            int t;
            string nevek;
            int cÖsszz = 0;
            string[] nevsor = new string[LISTAtan.Count];
            Console.WriteLine("1.Feladat :A beolvasott OSZTÁLY NÉVSOR :\n");
            for (t = 0; t < LISTAtan.Count; t++)            //névsor tagolása 20 név ig-után
            {

                cÖsszz += LISTAtan[t].Lóg;

                if (t == 20) { Console.WriteLine("\t"); }
                Console.Write(LISTAtan[t].nev + ("\t"));
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\n2.Feladat: A Lógások száma  szeptemberben: " + cÖsszz + " óra");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n3.Feladat: Kerestett nap 1 és 30 között ?:");
        
            int nap = Convert.ToInt32(Console.ReadLine());
            Console.Write("Tanulo Neve ?:"); Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.Green; string TANNEVE = Console.ReadLine();
            
            Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;
            string oszt = "";//osztály azonosító változó kiíráshoz
            bool szeptH = false; bool masikN = false;//szeptemberi és másik nap eldöntő változója
            List<string> MásokL = new List<string>();
            List<string> OsztalyokL = new List<string>() { LISTAtan[0].osztály };

            for (int x = 0; x < LISTAtan.Count(); x++)
            {
                if (OsztalyokL.Contains(LISTAtan[x].osztály) == false) { OsztalyokL.Add(LISTAtan[x].osztály); }
                if ((LISTAtan[x].nap1 == nap) && (LISTAtan[x].nev == TANNEVE)) { találat = true; }
                if (LISTAtan[x].nev == TANNEVE) { oszt = LISTAtan[x].osztály; }  //osztály azonosító megjelölése
                if ((LISTAtan[x].nap1 != nap) && (LISTAtan[x].nev == TANNEVE) && (LISTAtan[x].nap1 > 0)) { masikN = true; }
                if ((LISTAtan[x].nap1 == nap) && (LISTAtan[x].nev != TANNEVE)) { MásokL.Add("Szeptember "+nap+".nap "+LISTAtan[x].nev+" ("+LISTAtan[x].osztály+")")  ;szeptH = true; }
               
            }
            if (találat == true) { Console.WriteLine("\n4.Feladat: "+ TANNEVE + " a  " + nap + ".napon, a " + oszt + ".-osztályból lógott!"); }
            else { Console.WriteLine("\n4.Feladat: "+TANNEVE + " a  " + nap + ".napon, " + " NEM lógott a " + oszt + ".-osztályból. "); }

            if (masikN== true){ Console.WriteLine("\n"+TANNEVE + " Szeptemberben ,amúgy lógott egy másik napon."); }
            else { Console.WriteLine("\n"+TANNEVE + " Másik Szeptemberi napon, nem lógott. "); }

            if (szeptH == false) { Console.WriteLine("\n5.Feladat: A " + nap + ". nap szeptemberben nem lógott senki."); }
            else { Console.WriteLine("\n5.Feladat: Más lógók az adott napon:\n"); foreach (string m in MásokL) Console.WriteLine(m); }

            Console.ForegroundColor = ConsoleColor.Gray; Console.WriteLine("\n6:Feladat: Az osztályonkénti hiányzások összesítve osztályonként :\n");
            List<string> osztalyokSzam = new List<string>();// Osztályok legenerlása listába
            int c = 0;
          /*  for (int sz = 1; sz < 17; sz++)
            {
                if (sz < 9)
                { osztalyokSzam.Add(sz + "a"); }   //1a tól 8b .ig (0-15)bekerülnek a "osztályokszám" Listába
                else
                { c++; osztalyokSzam.Add(c + "b"); } 
            }
          */
             int g; int[] osztperlógas = new int[LISTAtan.Count];string[] osztLóg = new string[LISTAtan.Count];
            for (g = 0; g <OsztalyokL.Count(); g++)
            {
                foreach(tanulok e in LISTAtan)
                {
                    if (e.osztály == OsztalyokL[g])
                    {
                        osztperlógas[g] += e.Lóg;

                    }

                }

                Console.WriteLine(OsztalyokL[g] + ". osztályból " + " lógások  " + " " + osztperlógas[g]+" óra");
                osztLóg[g] = OsztalyokL[g] +".osztályból " + " lógások  " + " " + osztperlógas[g] + " óra";
            }
            File.WriteAllLines("logas.csv", osztLóg,Encoding.UTF8);
            Console.WriteLine("Összesítés kiírva : logas.csv File.ba és beolvasva: ");
            Console.ReadKey();
            string[] beolvas = File.ReadAllLines("logas.csv", Encoding.UTF8);   //ansi =UTF7
            foreach (string be in beolvas)
            { if (be.Length > 0) { Console.WriteLine(be); } }
                Console.ReadKey();

         }
       }
    }

