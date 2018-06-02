using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;


namespace Laboratory1
{

    class Program
    {

        static void Main(string[] args)
        {
            int ile = 0;
            bool sposob = false;     // false - Manhattan; true - Misplaced Tiles 
            int sciezka;
            long czas;


            Console.WriteLine("PUZZLE PRZESUWNE  -   Porownanie metod Manhattan i Misplaced Tiles");
            Console.WriteLine();

            Console.Write("Ilosc puzzli: ");
            string linia = Console.ReadLine();
            int ilosc_puzzli = int.Parse(linia);

            Console.Write("Ilosc ruchow mieszajacych: ");
            linia = Console.ReadLine();
            int ilosc_ruchow = int.Parse(linia);

            for (int metoda = 0; metoda < 2; metoda++)
            {
                if (metoda == 0)
                {
                    Console.WriteLine("Metoda Manhattan");
                    Console.WriteLine();
                }

                else
                {
                    sposob = true;
                    Console.WriteLine("Metoda Misplaced Tiles");
                    Console.WriteLine();
                }

                sciezka = 0;
                czas = 0;

                for (int i = 0; i < ilosc_puzzli; i++)
                {
                    PuzzleState startState = new PuzzleState(ilosc_ruchow,sposob);
                    PuzzleSearch searcher = new PuzzleSearch(startState);

                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    searcher.DoSearch();

                    IState state = searcher.Solutions[0];

                    List<PuzzleState> solutionPath = new List<PuzzleState>();
                    solutionPath.Sort();

                    while (state != null)
                    {
                        solutionPath.Add((PuzzleState)state);
                        state = state.Parent;
                    }

                    stopwatch.Stop();
                    czas += stopwatch.ElapsedMilliseconds;
                    solutionPath.Reverse();
               
                    Console.Write(i+1);
                    Console.Write(". Czas: ");
                    Console.Write(stopwatch.ElapsedMilliseconds);
                    Console.Write("ms");
                    Console.Write(" || Sciezka: ");
                
                    ile = 0;
                    foreach (PuzzleState s in solutionPath)
                    {
                        ile++;
                    }

                    sciezka += ile;
                    Console.WriteLine(ile);
                }

                float czas_sr = (float)czas/100000;
                float sciezka_sr = (float)sciezka / ilosc_puzzli;

                Console.WriteLine();
                Console.Write("Czas sredni: ");
                Console.Write(czas_sr);
                Console.WriteLine("s");

                Console.Write("Srednia sciezka: ");
                Console.WriteLine(sciezka_sr);
                Console.WriteLine();

                if(metoda == 0)
                Console.WriteLine("Wcisnij przycisk, aby zobaczyc 2 metode");

                Console.ReadKey();
            }
        }
  
    }
  
}
