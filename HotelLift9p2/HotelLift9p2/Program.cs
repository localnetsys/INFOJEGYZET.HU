using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Timers;
using System.Globalization;
using System.Diagnostics;
using System.Threading.Tasks;

namespace hotellift
{
    class Program

    {
        struct liftadat
        {
            public DateTime idö;

            public int kszam;

            public int indE;

            public int érkE;


        }
        static void Main(string[] args)
        {

            List<liftadat> ListAdat = new List<liftadat>();

            string[] txt = File.ReadAllLines("lift.txt");

            for (int s = 0; s < txt.Count(); s++)
            {

                if ((txt[s].Length > 0) && (s <= 1000))

                {
                    string[] so = txt[s].Split(' ');


                    liftadat ujadat = new liftadat();

                    ujadat.kszam = Convert.ToInt32(so[1]);

                    ujadat.indE = Convert.ToInt32(so[2]);
                    ujadat.érkE = Convert.ToInt32(so[3]);
                    ujadat.idö = Convert.ToDateTime(so[0]);
                    ListAdat.Add(ujadat);
                }
            }
            // int sorsz;
            int össz = 0;
            int mentek;
            for (int i = 0; i < ListAdat.Count; i++)

            {
                mentek = (ListAdat[i].indE + ListAdat[i].érkE);
                if (mentek > 0) össz++;
            }

            DateTime maxi = ListAdat[össz - 1].idö;
            DateTime mini = ListAdat[0].idö;

            Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("\n1.Feladat.");
            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("\nOk...!"); Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n2.Feladat."); Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nAz adtokat beolvastam, a lift.txt,file-ból  !  ");
            Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("\n3.Feladat."); Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nA bekért adatok alapján a liftet :  " + össz + " alkalommal használták. ");
            Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("\n4.Feladat."); Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nA vizsgált időszak " + mini + " tol   " + maxi + "ig tartott");
            Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("\n5.Feladat."); Console.ForegroundColor = ConsoleColor.Yellow;
            int ln = 0;
            int kmax = 0;

            for (int i = 0; i < ListAdat.Count; i++)//MAX EMELET SZÁMÍTÁS


            { if (ListAdat[i].érkE > ListAdat[ln].érkE) ln = i; }

            for (int k = 0; k < ListAdat.Count; k++)//MAX KÁRTYASZÁM SZÁMÍTÁS

            { if (ListAdat[k].kszam > ListAdat[kmax].kszam) kmax = k; }


            int maxk = ListAdat[kmax].kszam;
            Console.WriteLine("\n" + "A legnagyobb kártyaszám      " + maxk);
            Console.WriteLine("\n" + "A legmagasabb cél emelet:    " + ListAdat[ln].érkE);
            int hiba = 0;
            int kártya = -1;
            int emelet = -1;

            Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("\n6.Feladat."); Console.ForegroundColor = ConsoleColor.Yellow;
            //UTAZÁS KÁRTYÁVAL EMELETRE
            do
            {

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" ");
                Console.Write("Kérek egy KÁRTYA számot: ");
                try
                {
                    kártya = Convert.ToInt32(Console.ReadLine());

                }

                catch (FormatException)

                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\n" + "ELBASZTAD !!!   NE betüket, hanem SZÁMOT  irájá ,te BUZI !:))) ");
                }
                hiba++;


            }


            while (kártya > maxk || kártya <= 0);

            if (hiba > 1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\n" + "Na látod,megy ez  !:))) " + (hiba - 1) + " esetben rossz értéket adtál !");
            }

            else { };
            int hiba2 = 0;
            do
            {

                try
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(" ");
                    Console.Write("\n" + "Kérek EMELET számot: ");
                    emelet = Convert.ToInt32(Console.ReadLine());

                }


                catch (FormatException)

                {
                    hiba++; hiba2++;

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\n" + "ELBASZTAD !!!   NE szöveget, hanem SZÁMOT irájá ,te BUZI !:))) ");
                }

            }
            while (emelet <= 0 || emelet > 5);

            if (hiba2 > 1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                // Console.Write("\n" + "Ezt is benézted párszor !:))) " + (hiba - 1) + " esetben rossz értéket adtál az  ,ITT is !");
                Console.Write("\n" + "Nehezen,megy ez  !:))) " + (hiba2) + " esetben rossz értéket adtál az Emeletnél is !,viszont,");

            }

            else { };

            int utaz = 0;
            for (int z = 0; z < ListAdat.Count; z++)
            {
                if ((ListAdat[z].kszam == kártya) && (ListAdat[z].érkE == emelet)) utaz++;
            }
            Console.ForegroundColor = ConsoleColor.Yellow;

            if (utaz > 0) Console.WriteLine("\n Utaztak a:  {0}. kártyával a megadott : {1}.emeletre", kártya, emelet);

            else if (utaz == 0) Console.WriteLine("\n Nem utaztak a " + kártya + ". kártyával, a " + emelet + ". emeletre.");
            Console.ReadKey();

            Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("\n7.Feladat.");

            int MD = maxi.Day + 1; //max nap- utolso nap-MAXDAX
            int DM = mini.Day;       //min nap-kezdő nap -DAYMIN
            int[] db = new int[MD];
            DateTime[] H = new DateTime[MD];
            int c;//biztos hogy ment a lift
            int a;
            for (int t = 0; t < ListAdat.Count; t++)

            {

                c = (ListAdat[t].indE + ListAdat[t].érkE);
                a = ListAdat[t].idö.Day;

                if ((a < MD) && (c > 0)) db[a]++; H[a] = ListAdat[t].idö;

            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nA lift használatok száma, napi bontásban: . . . .  ");
            Console.WriteLine("\n");

            //Console.Write(idö.Substring(0, 11));

            
            {
                for (int idök = DM; idök < MD; idök++)

                    if ((H[idök] > mini))  //0 idős sorok szűrése


                    { Console.WriteLine("" + H[idök].ToString().Substring(0,11) + " napon, a lift használat száma :  " + db[idök]); }
      

            }
           
            try
            {
                kártya = Convert.ToInt32(Console.ReadLine());

            }

            catch (FormatException)

            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\n" + "A program lefutott,ne  irájá ,te BUZI !:))) ");
            }

            Console.ReadKey();
        }

    }
}








































































































