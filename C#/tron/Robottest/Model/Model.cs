using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//robot =1, fal =2, összetört fal=3 
namespace ElszabadultRobot.Model
{
    class GameModel
    {
       
       public int[,] grid;
       public int rx, ry;
        Random rnd;
       public int n;
        public int stepcount;
        int direction;
        public GameModel()
        {
            
        }
       public void Mapgenerator(int n)
        {
            grid = null;
            rnd = new Random();
            this.n = n;
            grid = new int[n, n];
            rx = rnd.Next(0, n);
            ry = rnd.Next(0, n);
            //grid[rx, ry] = 1;
            //grid[n / 2, n / 2] = 5;

            direction = rnd.Next(0, 4);
        }
        /// <summary>
        /// Ez lépteti a robotot a pályán , a timer.tick hívja meg
        /// </summary>
        #region Léptetés
        public void AutoStep()
        {
            int nx = rx;
            int ny = ry;
            if (rnd.Next(0,20)==1)
            {
                direction = rnd.Next(0, 4);
            }
            switch (direction)
            {
                case 0:nx=rx+1; break;
                case 1:nx=rx-1; break;
                case 2:ny=ry+1; break;
                case 3:ny=ry-1; break;
            }
            int checkresult = CheckNewPosition(nx, ny);
            int tempx = nx;
            int tempy = ny;
            if (checkresult==-1 || checkresult==2)
            {
                
                nx = rx;
                ny = ry;
                List<int> randomdir = new List<int>();
                if (CheckNewPosition(nx + 1, ny) == 0) randomdir.Add(0);
                if (CheckNewPosition(nx - 1, ny) == 0) randomdir.Add(1);
                if (CheckNewPosition(nx, ny + 1) == 0) randomdir.Add(2);
                if (CheckNewPosition(nx, ny - 1) == 0) randomdir.Add(3);
                if (randomdir.Count != 0) direction = randomdir[rnd.Next(0, randomdir.Count)];
                else
                {
                    if (CheckNewPosition(nx + 1, ny) != -1) randomdir.Add(0);
                    if (CheckNewPosition(nx - 1, ny) != -1) randomdir.Add(1);
                    if (CheckNewPosition(nx, ny + 1) != -1) randomdir.Add(2);
                    if (CheckNewPosition(nx, ny - 1) != -1) randomdir.Add(3);
                    direction = randomdir[rnd.Next(0, randomdir.Count)];

                }

                /*  do
              {
                  temp = rnd.Next(0, 4);

                  switch (temp)
                  {
                      case 0: nx = rx + 1;direction = nx; break;
                      case 1: nx = rx - 1; direction = nx; break;
                      case 2: ny = ry + 1; direction = ny; break;
                      case 3: ny = ry - 1; direction = ny; break;
                  }
              } while (CheckNewPosition(nx, ny) != 0); */

                if (checkresult==2)
                {
                    grid[tempx, tempy] = 3;
                }
                
            }
            switch (direction)
            {
                case 0: nx = rx + 1; break;
                case 1: nx = rx - 1; break;
                case 2: ny = ry + 1; break;
                case 3: ny = ry - 1; break;
            }

            
            if (grid[nx, ny]==3|| grid[nx, ny] == 2)
            {
                grid[nx, ny] = 4;
            }else grid[nx, ny] = 1;

            if (grid[rx,ry]==4)
            {
                grid[rx, ry] = 3;
            }else grid[rx, ry] = 0;


            rx = nx;
            ry = ny;
            ++stepcount;
            Endgame();
        }

       int CheckNewPosition(int x, int y)
        {
            if(x<0||x>n-1 || y<0 || y>n-1)
            {
                return -1;
            }
            return grid[x, y];
        }
    #endregion
    public event EventHandler<EventArgs> GameWon;
        void Endgame()
        {
            if (grid[n/2,n/2]==1)
            {
                 GameWon(this,new EventArgs());
            }
            
        }
    
    }

}
