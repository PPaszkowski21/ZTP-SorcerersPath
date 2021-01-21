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
        public static Grid myGrid { get; set; }
        public static Image EndMessage { get; set; }
        //public static Image PauseMessage { get; set; }
        public static int ActualStage { get; set; }
        public static bool GameCanBeContinued { get; set; }
        public static bool NextStagePause { get; set; }

        public static Player Player { get; set; }
        public static Game GetInstance(Canvas mainCanvas, Grid mainGrid, int stageNumber)
        {
            if (_instance == null)
            {
                GameCanBeContinued = true;
                ActualStage = stageNumber;
                Player = new Player(1000, 7);
                _instance = new Game(mainCanvas, mainGrid, stageNumber);
            }
            return _instance;
        }
        private Game(Canvas canvas, Grid grid, int stageNumber)
        {
            myCanvas = canvas;
            myGrid = grid;
            CreateInterface(grid);
            Stage = new Stage(stageNumber, canvas, grid, Player);
        }

        private void CreateInterface(Grid grid)
        {
            int buttonsWidth = 115;
            int buttonsHeight = 115;
            Button fireballButton = new Button();
            fireballButton.Content = new Image()
            {
                Source = new BitmapImage(new Uri(ImageManager.FireballIcon))
            };
            fireballButton.Width = buttonsWidth;
            fireballButton.Height = buttonsHeight;
            fireballButton.Name = "FireballButton";
            fireballButton.IsEnabled = false;
            fireballButton.Click += BuyEnchantedFireball;

            Button toxicBoltButton = new Button();
            toxicBoltButton.Content = new Image()
            {
                Source = new BitmapImage(new Uri(ImageManager.ToxicBoltIcon))
            };
            toxicBoltButton.Width = buttonsWidth;
            toxicBoltButton.Height = buttonsHeight;
            toxicBoltButton.Name = "ToxicBoltButton";
            toxicBoltButton.IsEnabled = false;
            toxicBoltButton.Click += BuyToxicBolt;

            Button lightningButton = new Button();
            lightningButton.Content = new Image()
            {
                Source = new BitmapImage(new Uri(ImageManager.LightningIcon))
            };
            lightningButton.Width = buttonsWidth;
            lightningButton.Height = buttonsHeight;
            lightningButton.Name = "LightningButton";
            lightningButton.IsEnabled = false;
            lightningButton.Click += BuyLightning;

            Button fearButton = new Button();
            fearButton.Content = new Image()
            {
                Source = new BitmapImage(new Uri(ImageManager.FearIcon))
            };
            fearButton.Width = buttonsWidth;
            fearButton.Height = buttonsHeight;
            fearButton.Name = "FearButton";
            fearButton.IsEnabled = false;
            fearButton.Click += BuyFear;

            Button blinkButton = new Button();
            blinkButton.Content = new Image()
            {
                Source = new BitmapImage(new Uri(ImageManager.BlinkIcon))
            };
            blinkButton.Width = buttonsWidth;
            blinkButton.Height = buttonsHeight;
            blinkButton.Name = "BlinkButton";
            blinkButton.IsEnabled = false;
            blinkButton.Click += BuyDash;



            Image mainInterface = new Image();
            mainInterface.Source = new BitmapImage(new Uri(ImageManager.interface1));
            mainInterface.Stretch = Stretch.Fill;


            //PauseMessage = new Image();
            //PauseMessage.Source = new BitmapImage(new Uri(ImageManager.Pause));
            //PauseMessage.Width = 1620;
            //PauseMessage.Height = 1080;
            //PauseMessage.Stretch = Stretch.Fill;
            //PauseMessage.Visibility = Visibility.Hidden;
            //Canvas.SetTop(PauseMessage, 0 );
            //Canvas.SetLeft(PauseMessage, 0 );
            //myCanvas.Children.Add(PauseMessage);

            

            grid.Children.OfType<StackPanel>().FirstOrDefault().Children.Add(fireballButton);
            grid.Children.OfType<StackPanel>().FirstOrDefault().Children.Add(toxicBoltButton);
            grid.Children.OfType<StackPanel>().FirstOrDefault().Children.Add(lightningButton);
            grid.Children.OfType<StackPanel>().FirstOrDefault().Children.Add(fearButton);
            grid.Children.OfType<StackPanel>().FirstOrDefault().Children.Add(blinkButton);
            grid.Children.OfType<Grid>().FirstOrDefault().Children.Add(mainInterface);
        }
        private void BuyLightning(object sender, RoutedEventArgs e)
        {
            if (Player.GameSave.Gold >=30)
            {
                Player.GameSave.Gold -= 30;
                myGrid.Children.OfType<StackPanel>().FirstOrDefault().Children.Remove(myGrid.Children.OfType<StackPanel>().FirstOrDefault().Children.OfType<Button>().FirstOrDefault(x => x.Name == "LightningButton"));
                Player.GameSave.LightningAvailable = true;
            }
            myCanvas.Focus();
        }
        private void BuyToxicBolt(object sender, RoutedEventArgs e)
        {
            if(Player.GameSave.Gold>=10)
            {
                Player.GameSave.Gold -= 10;
                myGrid.Children.OfType<StackPanel>().FirstOrDefault().Children.Remove(myGrid.Children.OfType<StackPanel>().FirstOrDefault().Children.OfType<Button>().FirstOrDefault(x => x.Name == "ToxicBoltButton"));
                Player.GameSave.ToxicBoltAvailable = true;
            }
            myCanvas.Focus();
        }
        private void BuyEnchantedFireball(object sender, RoutedEventArgs e)
        {
            if (Player.GameSave.Gold >= 15)
            {
                Player.GameSave.Gold -= 15;
                myGrid.Children.OfType<StackPanel>().FirstOrDefault().Children.Remove(myGrid.Children.OfType<StackPanel>().FirstOrDefault().Children.OfType<Button>().FirstOrDefault(x => x.Name == "FireballButton"));
                Player.GameSave.EnchantedFireballAvaible = true;
            }
            myCanvas.Focus();
        }
        private void BuyFear(object sender, RoutedEventArgs e)
        {
            if (Player.GameSave.Gold >= 20)
            {
                Player.GameSave.Gold -= 20;
                myGrid.Children.OfType<StackPanel>().FirstOrDefault().Children.Remove(myGrid.Children.OfType<StackPanel>().FirstOrDefault().Children.OfType<Button>().FirstOrDefault(x => x.Name == "FearButton"));
                Player.GameSave.FearAvailable = true;
            }
            myCanvas.Focus();
        }

        private void BuyDash(object sender, RoutedEventArgs e)
        {
            if (Player.GameSave.Gold >= 15)
            {
                Player.GameSave.Gold -= 15;
                myGrid.Children.OfType<StackPanel>().FirstOrDefault().Children.Remove(myGrid.Children.OfType<StackPanel>().FirstOrDefault().Children.OfType<Button>().FirstOrDefault(x => x.Name == "BlinkButton"));
                Player.GameSave.DashAvailable = true;
            }
            myCanvas.Focus();
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
            Stage = new Stage(nr, myCanvas, myGrid, Player);
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

