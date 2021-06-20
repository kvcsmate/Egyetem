using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElszabadultRobot.Model;



namespace ElszabadultRobot.Persistence
{

    class LoadSave
    {
        StreamWriter sw;
        StreamReader sr;
        string buffer;
        
        public int size = 7;
        public List<int> xcoord;
        public List<int> ycoord;
        public List<int> value;
        public  LoadSave()
        {
            xcoord = new List<int>();
            ycoord = new List<int>();
            value = new List<int>();
        }
        public GameMap Load()
        {
           
            using (StreamReader sr = new StreamReader("save.txt"))
            {
                string line;
                int size = int.Parse(sr.ReadLine());
                
                GameMap map = new GameMap(size);
                map.Direction1 = int.Parse(sr.ReadLine());
                map.Direction2 = int.Parse(sr.ReadLine());
                map.X1 = int.Parse(sr.ReadLine());
                map.Y1 = int.Parse(sr.ReadLine());
                map.X2 = int.Parse(sr.ReadLine());
                map.Y2 = int.Parse(sr.ReadLine());
                map.grid = new int[size, size];
                for(int i=0;i<size;i++)
                {
                    line = sr.ReadLine();
                    for (int j = 0; j < size; j++)
                    {
                            map.grid[i, j] = int.Parse(line[j].ToString());
                    }


                }
                map.grid[map.X1, map.Y1] = 1;
                map.grid[map.X2, map.Y2] = 2;
                return map;

            }
            
            
        }
        public void Save(GameMap map)
        {
            StreamWriter sw = new StreamWriter("save.txt");
            int size = map.Meret;
            sw.WriteLine(size);
            sw.WriteLine(map.Direction1);
            sw.WriteLine(map.Direction2);
            sw.WriteLine(map.X1);
            sw.WriteLine(map.Y1);
            sw.WriteLine(map.X2);
            sw.WriteLine(map.Y2);
            for (int i = 0; i < size; i++)
            {
                string line="";
                for (int j = 0; j < size; j++)
                {
                    line += map.grid[i, j];
                }
                sw.WriteLine(line);
            }

            sw.Close();
        }
    }
    

}
