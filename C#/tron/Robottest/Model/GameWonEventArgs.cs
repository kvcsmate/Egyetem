using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElszabadultRobot.Model
{
    class GameWonEventArgs:EventArgs
    {

        public bool won { get; private set; }
        public GameWonEventArgs(bool w) { won = w; }
    }
}
