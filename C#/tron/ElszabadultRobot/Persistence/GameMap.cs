using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElszabadultRobot.Persistence
{

   public class GameMap
    {
        int x1;
        int y1;
        int x2;
        int y2;
        int meret;
        int direction1;
        int direction2;
        public int[,] grid;
        public GameMap(int meret)
        {
            this.meret = meret;
            int[,] grid = new int[meret, meret];
            for (int i = 0; i < meret; i++)
            {
                for (int j = 0; j < meret; j++)
                {
                    grid[i, j] = 0;
                }
            }
                 
            
        }
        public int Direction1 { get{ return direction1; } set { direction1 = value; } }
        public int Direction2 { get { return direction2; } set { direction2= value; } }
        public int X1 { get { return x1; } set { x1 = value; } }
        public int Y1 { get { return y1; } set { y1 = value; } }
        public int X2 { get { return x2; } set { x2 = value; } }
        public int Y2 { get { return y2; } set { y2 = value; } }
        public int Meret  { get { return meret; } set { meret = value; } } 
        //public int[,] Grid { get { return Grid; } }
    }
    
}
