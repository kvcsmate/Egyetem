 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ElszabadultRobot.Model;


namespace ElszabadultRobot
{

    public partial class Form1 : Form
    {
       // LoadSave persistence;
        GameModel model;
        int elapsedtime=0;
        int currmapsize=100;
        int mapsize=7;
        int windowsize = 40;
        Button[,] buttongrid;
        Timer timer;
        Timer clock;
        
        
        
        public Form1()
        {
            model = new GameModel();
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += Directiongiving;
            WindowState = FormWindowState.Maximized;
           windowsize = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height-100;
        }
        #region indítás
        void Reset()
        {
            if (buttongrid != null)
            {
                foreach (Button a in buttongrid)
                {
                    this.Controls.Remove(a);

                }

            }
            if (timer != null) timer.Dispose();
            if (clock != null) clock.Dispose();
            buttongrid = null;
            //model = null;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            currmapsize = int.Parse(mapsizedecimal.Value.ToString());
            

        }
        void GenerateGrid()
        {
            //buttongrid = null;



            //int gridsize = buttonsize/model.n;
            //this.Size = new Size(gridsize+250,gridsize+100);
            int buttonsize = windowsize / model.n;
            buttongrid = new Button[model.n,model.n];
            for (int i = 0; i <model.n; i++)
            {
                for (int j = 0; j < model.n; j++)
                {
                    int x = 20 + j * buttonsize;
                    int y = 20 + i * buttonsize;
                    buttongrid[i, j] = new Button();
                    buttongrid[i, j].TabIndex = i * model.n + j;
                    //buttongrid[i, j].MouseClick += new MouseEventHandler(GridMouseClick);
                    buttongrid[i, j].Location = new Point(x, y);
                    buttongrid[i, j].Size = new Size(buttonsize+1, buttonsize+1);
                    buttongrid[i, j].BackColor = Color.White;
                    this.Controls.Add(buttongrid[i, j]);
                    
                }
            }



        }
        private void Start_Click(object sender, EventArgs e)
        {
            NewGame();
            
        }

        void NewGame()
        {
            Reset();
            mapsize = currmapsize;

            model = new GameModel();
            model.Mapgenerator(mapsize);
            model.GameWon += new EventHandler<GameWonEventArgs>(Gameover);
            elapsedtime = 0;
            GenerateGrid();

            timer = new Timer();
            timer.Interval = 500;
            timer.Tick += Ontick;

            clock = new Timer();
            clock.Tick += Onclock;
            clock.Interval = 1000;
            timer.Start();
            clock.Start();
            
        }
        #endregion
        private void mapsize_ValueChanged(object sender, EventArgs e)
        {
            currmapsize = int.Parse(mapsizedecimal.Value.ToString());
        }
        #region timers
        private void Ontick(object sender, EventArgs e)
        {
            model.Step();
            RefreshGrid();
            



        }
        private void Onclock(object sender, EventArgs e)
        {
            elapsedtime++;
            timelabel.Text = elapsedtime.ToString();
                 }
        #endregion
        #region screen refresh
        private void RefreshGrid()
        {
            for (int i = 0; i < model.n; i++)
            {
                for (int j = 0; j < model.n; j++)
                {
                    switch (model.map.grid[i,j])
                    {
                        case 1: buttongrid[i, j].BackColor = Color.Blue;break;
                        case 3:buttongrid[i, j].BackColor=Color.RoyalBlue;break;
                        case 2:buttongrid[i, j].BackColor = Color.Red;break;
                        case 4: buttongrid[i, j].BackColor = Color.OrangeRed; break;
                        default:buttongrid[i, j].BackColor = Color.White; break;
                    }
                }
            }
            

        }
        #endregion
        private void Gameover(object sender, GameWonEventArgs e)
        {
            clock.Stop();
            timer.Stop();
            if (e.won==true)
            {
                MessageBox.Show("A kék játékos nyert!\n ELTElt idő: " + elapsedtime.ToString() + "s", "Játék Vége!");
            }
            else MessageBox.Show("A piros játékos nyert!\n ELTElt idő: " + elapsedtime.ToString() + "s", "Játék Vége!");
            //Reset();
            
            
            //NewGame();
            
            

        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (model.map!=null)
            {
                if (timer != null)
                {
                    if (timer.Enabled)
                    {
                        timer.Stop();
                        clock.Stop();
                        Pausebutton.Text = "I>";
                    }
                }

                    model.Save();
            }else MessageBox.Show("Nincs pálya amit el lehetne menteni!", "Hiba!");

        }

        private void LoadGame_Click(object sender, EventArgs e)
        {
            
            Reset();
           
            
            //model = new GameModel();
            model.Load();
            model.GameWon += new EventHandler<GameWonEventArgs>(Gameover);
            elapsedtime = 0;
            

            GenerateGrid();
            this.KeyDown += Directiongiving;
            timer = new Timer();
            timer.Interval = 500;
            timer.Tick += Ontick;

            clock = new Timer();
            clock.Tick += Onclock;
            clock.Interval = 1000;
            timer.Start();
            clock.Start();
            RefreshGrid();
        }

        private void Pausebutton_Click(object sender, EventArgs e)
        {
            if (timer!=null)
            {
                if (timer.Enabled)
                {
                    timer.Stop();
                    clock.Stop();
                    Pausebutton.Text = "I>";
                }
                else
                {
                    timer.Start();
                    clock.Start();
                    Pausebutton.Text = "||";

                }
            }
            
        }
        private void Directiongiving(object sender, KeyEventArgs e)
        {
            //MessageBox.Show("0");

            switch (e.KeyCode)
            {
                case Keys.W: model.map.Direction1= 0;  break;
                case Keys.D: model.map.Direction1 = 1;  break;
                case Keys.S: model.map.Direction1 = 2;break;
                case Keys.A: model.map.Direction1 = 3;  break;

                case Keys.NumPad8: model.map.Direction2 = 0; break;
                case Keys.NumPad6: model.map.Direction2 = 1; break;
                case Keys.NumPad5: model.map.Direction2 = 2; break;
                case Keys.NumPad4: model.map.Direction2 = 3; break;
                default:break;
            }
        }
    }
}
