using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Laboratory1
{
    public class PuzzleState : State
    {
        
        public const int GRID_SIZE = 3;
        private string id;
        public int[] wspol;
        public int[,] table;
        private int[] zero;
        public bool[] mozliwosci;    // G D L P
        public PuzzleState rodzic;


        public int[,] Table
        {
            get { return this.table; }
            set { this.table = value; }
        }

        public int Grid()
        {
            return GRID_SIZE;
        }


        public override string ID
        {
            get { return this.id; }
        }

        public bool Rozwiazanie()
        {
            int k = 0;
            bool flag = true;

            for (int i = 0; i < GRID_SIZE; i++)
            {
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    if (table[i, j] != k) flag = false;
                    k++;
                }
            }
                return flag;
        }


        public void Print(int metoda)
        {
            for(int i = 0; i < GRID_SIZE; i++)
            {
                Console.WriteLine();
                for(int j = 0; j < GRID_SIZE; j++)
                {
                    Console.Write(table[i,j]);
                    Console.Write("   ");
                }
            }

            if (metoda == 0)
            ComputeHeuristicGrade();

            else
            Man();
        }


        public override double ComputeHeuristicGrade()
        {
            int k=0;
            int licz = 0;
            for(int i = 0; i < GRID_SIZE; i++)
            {
                for(int j = 0; j < GRID_SIZE; j++)
                {
                    if (table[i,j] != k) licz++;
                    k++;
                }
            }
            return h = licz;
        }

        public void gdzie(int l)
        {
            wspol = new int[2];

            for (int i = 0; i < GRID_SIZE; i++)
            {
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    if (table[i, j] == l)
                    {
                        wspol[0] = i;
                        wspol[1] = j;
                        break;
                    }
                }
            }
        }

        public double Man()
        {
            int licz = 0;
            int n = GRID_SIZE * GRID_SIZE - 1;
            for (int i = 0; i < n; i++)
            {


                float liczba = i / n;
                int liczba2 = (int)Math.Floor(liczba);
                gdzie(i);
                licz = licz + Math.Abs(wspol[0] - liczba2) + Math.Abs(wspol[1] - (i % n));


            }

            return h = licz;
        }



        public void sprawdz_0()
        {
            zero = new int[2];
            for(int i = 0; i < GRID_SIZE; i++)
            {
               
                for(int j = 0; j < GRID_SIZE; j++)
                {
                    if (table[i, j] == 0)
                    {
                      
                        zero[0] = i;
                        zero[1] = j;          
                    }
                }
            }
        }

        public string generujID()
        {
            StringBuilder sb = new StringBuilder();
            foreach (int i in table)
            {
                sb.Append(i.ToString());
                sb.Append(',');
            }
            return sb.ToString();
        }




        public void sprawdz_moz()
        {
            mozliwosci = new bool[4];
            for (int i = 0; i < 4; i++)
                mozliwosci[i] = true; ////G D L P

            if (zero[0] == 0) mozliwosci[0] = false;
            if (zero[0] == GRID_SIZE - 1) mozliwosci[1] = false;

            if (zero[1] == 0) mozliwosci[2] = false;
            if (zero[1] == GRID_SIZE - 1) mozliwosci[3] = false;
        }



        private void przesun(int liczba)
        {

            int tmp = 0;
            if (liczba == 0)
            {
                tmp = table[zero[0] - 1,zero[1]];
                table[zero[0] - 1, zero[1]] = 0;
                table[zero[0], zero[1]] = tmp;
            }

            if (liczba == 1)
            {
                tmp = table[zero[0] + 1, zero[1]];
                table[zero[0] + 1, zero[1]] = 0;
                table[zero[0], zero[1]] = tmp;
            }

            if (liczba == 2)
            {
                tmp = table[zero[0] , zero[1]-1];
                table[zero[0] , zero[1]-1] = 0;
                table[zero[0], zero[1]] = tmp;
            }

            if (liczba == 3)
            {
                tmp = table[zero[0], zero[1] + 1];
                table[zero[0], zero[1] + 1] = 0;
                table[zero[0], zero[1]] = tmp;
            }
        }




        private void mieszaj(int ilosc)      //glowna
        {
            Random los = new Random();
            int liczba;
            for (int i = 0; i < ilosc;i++)
            {
                sprawdz_0();
                sprawdz_moz();
                while (true)
                {
                     liczba = los.Next(0, 4);
                    if (mozliwosci[liczba] != false) break;
                }
                przesun(liczba);
             }
        }



        public PuzzleState(int ilosc,int metoda)
        {

            table = new int[GRID_SIZE, GRID_SIZE];
            for(int i = 0; i < GRID_SIZE; i++) 
                for(int j = 0; j < GRID_SIZE; j++)
                {
                    table[i, j] = i * GRID_SIZE + j ;
                }

             mieszaj(ilosc);

            if(metoda == 1)
            Man();


            rodzic = null;
            this.id = generujID();
            this.Print(metoda);

            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Szukanie rozwiazania");
        }




        public PuzzleState() 
        {

            this.table = new int[GRID_SIZE, GRID_SIZE];
        }

        public PuzzleState(PuzzleState parent, int Kierunek)
            : base(parent)
        {
          
            //Console.WriteLine("konstruktor glowny");
            this.table = new int[GRID_SIZE, GRID_SIZE];
            // Skopiowanie stanu sudoku do nowej tabeli
            Array.Copy(parent.table, this.table, this.table.Length);
            // Ustawienie nowej wartosci w wybranym polu sudoku

            this.sprawdz_0();
            this.przesun(Kierunek);

            //            Print();

            this.id = generujID();
            this.g = parent.g + 1;
        }
    }
} 
