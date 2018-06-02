using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory1
{
    public class PuzzleSearch : AStarSearch
    {
        private PuzzleState[] heury;
        
        public PuzzleSearch(PuzzleState state) : base(state, true, true) { }


        public void sortuj(int ile)
        {

            PuzzleState tmp = new PuzzleState();
            for (int i = 0; i < ile; i++)
            {
                for(int j = 1; j < ile; j++)
                {
                    if(heury[j-1].F> heury[j].F)
                    {
                        tmp = heury[j - 1];
                        heury[j - 1] = heury[j];
                        heury[j] = tmp;

                    }
                }
            }




        }
        protected override void buildChildren(IState parent)
        {
            PuzzleState state = (PuzzleState)parent;

            // poszukiwanie pola 0 
            state.sprawdz_0();
            int ile = 0;
            state.sprawdz();
           
            for (int i = 0; i < 4; i++)
            {
                if (state.kierunek[i] != false)         //spardzenie mozliwosci ruchu
                    ile++;
            }

            heury = new PuzzleState[ile];

            int n= 0;
            for (int i = 0; i < 4; i++)
            {
                if (state.kierunek[i] != false)
                {
                    PuzzleState child = new PuzzleState(state, i);
                    heury[n] = child;
                    n++;
                }

            }

            for(int i=0; i < ile; i++)
            {
              parent.Children.Add(heury[i]);
            }
        }

        protected override bool isSolution(IState state)
        {

            PuzzleState s = (PuzzleState)state;
            return s.Rozwiazanie();
        }
        
    }
}
