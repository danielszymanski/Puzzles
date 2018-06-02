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

            int metoda = 1;
            int ilosc_mieszan = 100;

            if (metoda == 0)
                Console.WriteLine("Metoda Misplaced Tiles");
            else
                Console.WriteLine("Metoda Manhattan");

         PuzzleState startState = new PuzzleState(ilosc_mieszan, metoda);
         PuzzleSearch searcher = new PuzzleSearch ( startState ) ;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            searcher . DoSearch () ;

         IState state = searcher . Solutions [0];

         List < PuzzleState > solutionPath = new List < PuzzleState >() ;
            solutionPath.Sort();

            while ( state != null ) 
            {
             solutionPath . Add (( PuzzleState ) state ) ;
             state = state . Parent ;
            }
            
            stopwatch.Stop();

            Console.WriteLine();
            Console.WriteLine("Czas obliczen: {0}", stopwatch.Elapsed);


            solutionPath . Reverse () ;
            Console.WriteLine();
            Console.Write("Pokaz rozwiazanie <wcisnij klawisz>");
            Console.ReadKey();

            foreach ( PuzzleState s in solutionPath )
            {
                Console.Clear();
                s.Print (metoda) ;

                System.Threading.Thread.Sleep(1000);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Gotowe!");
            Console.ReadKey();
        }

        
    }
  
}
