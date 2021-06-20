using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElszabadultRobot.Persistence;


//p1=1 , p2=2, p1 fal=3, p2 fal = 4; 
namespace ElszabadultRobot.Model
{
  public  class   GameModel
    {
         GameMap _map;
        public GameMap map;
       
        LoadSave persistence;
      // public int[,] map.grid;
      // public int map.X, map.Y;
        Random rnd;
       public int n;
        public int stepcount;
        int direction1;
        int direction2;
        #region Kreálás
        public GameModel()
        {
            LoadSave persistence = new LoadSave();
            rnd = new Random();
            direction1 = 2;
            direction2 = 0;
        }
       public void Mapgenerator(int n)
        {
            map = new GameMap(n);
            map.Direction1 = direction1;
            map.Direction2 = direction2;
           // map.map.grid = null;
           
            this.n = n;
            map.grid = new int[n, n];
            map.X1 = 0;
            map.Y1 = n / 2;
            map.X2 = n - 1; ;
            map.Y2 = n / 2;

            //map.grid[map.X, map.Y] = 1;
            //map.grid[n / 2, n / 2] = 5;


        }
        #endregion
        /// <summamap.Y>
        /// Ez lépteti a robotot a pályán , a timer.tick hívja meg
        /// </summamap.Y>
        #region Léptetés
        public void Step()
        {
            if(!Endgame())
            {
                map.grid[map.X1, map.Y1] = 3;
                map.grid[map.X2, map.Y2] = 4;
                switch (map.Direction1)
                {
                    case 0: map.X1--; break;
                    case 1: map.Y1++; break;
                    case 2: map.X1++; break;
                    case 3: map.Y1--; break;

                }
                switch (map.Direction2)
                {
                    case 0: map.X2--; break;
                    case 1: map.Y2++; break;
                    case 2: map.X2++; break;
                    case 3: map.Y2--; break;

                }

                map.grid[map.X1, map.Y1] += 1;
                map.grid[map.X2, map.Y2] += 2;

            }

        }

        
        
        #endregion
        #region Játék Vége
        public event EventHandler<GameWonEventArgs> GameWon;
        bool Endgame()
        {
            int döntetlen = 0;
            bool eg = false;
            if (map.grid[map.X1, map.Y1] > 1 ||
                map.X1 == 0 && map.Direction1 == 0 ||
                map.Y1 == map.Meret - 1 && map.Direction1 == 1 ||
                map.X1 == map.Meret - 1 && map.Direction1 == 2 ||
                map.Y1 ==0 && map.Direction1 ==3
                )
            {
                
                 GameWon(this,new GameWonEventArgs(true));
                eg = true;
            }

            if(map.grid[map.X2, map.Y2]>2 ||
                map.X2 == 0 && map.Direction2 == 0 ||
                map.Y2 == map.Meret - 1 && map.Direction2 == 1 ||
                map.X2 == map.Meret - 1 && map.Direction2 == 2 ||
                map.Y2 == 0 && map.Direction2 == 3)
            {
                
                GameWon(this,new GameWonEventArgs(false));
                eg = true;
            }
            return eg;
        }
        #endregion
        public void Load()
        {
            //Mapgenerator(n);
            
            LoadSave persistence = new LoadSave();
            map = persistence.Load();
            n = map.Meret;
 
        }
        public void Save()
        {
            LoadSave persistence = new LoadSave();
            persistence.Save(map);
        }

    }

}
