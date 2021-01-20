using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using ZTP.Actions;
using ZTP.Images;
using ZTP.Monsters;
using ZTP.PlayerClassess;
using ZTP.Spells;

namespace ZTP.GameSingleton
{
    public class Game
    {
        private static Game _instance;
        private static Canvas myCanvas { get; set; }
        public static Stage Stage { get; set; }
        public static Grid Grid { get; set; }
        public static int ActualStage { get; set; }
        public static Player Player { get; set;}
        public static Game GetInstance(Canvas mainCanvas, Grid mainGrid, int stageNumber)
        {
            if (_instance == null)
            {
                ActualStage = stageNumber;
                Player = new Player(1000, 5);
                _instance = new Game(mainCanvas, mainGrid, stageNumber);
            }
            return _instance;
        }
        private Game(Canvas canvas, Grid grid, int stageNumber)
        {
            myCanvas = canvas;
            Grid = grid;
            Stage = new Stage(stageNumber, canvas, grid, Player);
        }

        public void Start()
        {
            Stage.gameTimer = new DispatcherTimer(DispatcherPriority.Normal);
            Stage.gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            Stage.gameTimer.Tick += Stage.GameLoop;
            Stage.gameTimer.Start();
        }
        public void ChangeStage(int nr)
        {
            ActualStage += 1;
            Stage = new Stage(nr, myCanvas, Grid, Player);
        }


        public void KeyIsDown(object sender, KeyEventArgs e)
        {
            Stage.KeyIsDown(sender, e);
        }
        public void KeyIsUp(object sender, KeyEventArgs e)
        {
            Stage.KeyIsUp(sender, e);
        }
    }
}

