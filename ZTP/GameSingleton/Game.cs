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
                Player = new Player(1, 5);
                _instance = new Game(mainCanvas, mainGrid, stageNumber);
            }
            return _instance;
        }
        private Game(Canvas canvas, Grid grid, int stageNumber)
        {
            myCanvas = canvas;
            myGrid = grid;

            int buttonsWidth = 115;
            int buttonsHeight = 115;
            Button button = new Button();
            button.Content = new Image()
            {
                Source = new BitmapImage(new Uri(ImageManager.FireballIcon))
            };
            button.Width = buttonsWidth;
            button.Height = buttonsHeight;
            button.Name = "FireballButton";
            button.IsEnabled = false;
            button.Click += BuyEnchantedFireball;

            Button button2 = new Button();
            button2.Content = new Image()
            {
                Source = new BitmapImage(new Uri(ImageManager.ToxicBoltIcon))
            };
            button2.Width = buttonsWidth;
            button2.Height = buttonsHeight;
            button2.Name = "ToxicBoltButton";
            button2.IsEnabled = false;
            button2.Click += BuyToxicBolt;

            Button button3 = new Button();
            button3.Content = new Image()
            {
                Source = new BitmapImage(new Uri(ImageManager.LightningIcon))
            };
            button3.Width = buttonsWidth;
            button3.Height = buttonsHeight;
            button3.Name = "LightningButton";
            button3.IsEnabled = false;
            button3.Click += BuyLightning;

            Button button4 = new Button();
            button4.Content = new Image()
            {
                Source = new BitmapImage(new Uri(ImageManager.FearIcon))
            };
            button4.Width = buttonsWidth;
            button4.Height = buttonsHeight;
            button4.Name = "FearButton";
            button4.IsEnabled = false;
            button4.Click += BuyFear;

            Button button5 = new Button();
            button5.Content = new Image()
            {
                Source = new BitmapImage(new Uri(ImageManager.BlinkIcon))
            };
            button5.Width = buttonsWidth;
            button5.Height = buttonsHeight;
            button5.Name = "BlinkButton";
            button5.IsEnabled = false;
            button5.Click += BuyDash;



            Image image = new Image();
            image.Source = new BitmapImage(new Uri(ImageManager.interface1));
            image.Stretch = Stretch.Fill;


            grid.Children.OfType<StackPanel>().FirstOrDefault().Children.Add(button);
            grid.Children.OfType<StackPanel>().FirstOrDefault().Children.Add(button2);
            grid.Children.OfType<StackPanel>().FirstOrDefault().Children.Add(button3);
            grid.Children.OfType<StackPanel>().FirstOrDefault().Children.Add(button4);
            grid.Children.OfType<StackPanel>().FirstOrDefault().Children.Add(button5);
            grid.Children.OfType<Grid>().FirstOrDefault().Children.Add(image);
            
            
            Stage = new Stage(stageNumber, canvas, grid, Player);
        }
        private void BuyLightning(object sender, RoutedEventArgs e)
        {
            if (Player.GameSave.Gold >= 0)
            {
                Player.GameSave.Gold -= 0;
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
            if (Player.GameSave.Gold >= 0)
            {
                Player.GameSave.Gold -= 0;
                myGrid.Children.OfType<StackPanel>().FirstOrDefault().Children.Remove(myGrid.Children.OfType<StackPanel>().FirstOrDefault().Children.OfType<Button>().FirstOrDefault(x => x.Name == "FireballButton"));
                Player.GameSave.EnchantedFireballAvaible = true;
            }
            myCanvas.Focus();
        }
        private void BuyFear(object sender, RoutedEventArgs e)
        {
            if (Player.GameSave.Gold >= 0)
            {
                Player.GameSave.Gold -= 0;
                myGrid.Children.OfType<StackPanel>().FirstOrDefault().Children.Remove(myGrid.Children.OfType<StackPanel>().FirstOrDefault().Children.OfType<Button>().FirstOrDefault(x => x.Name == "FearButton"));
                Player.GameSave.FearAvailable = true;
            }
            myCanvas.Focus();
        }

        private void BuyDash(object sender, RoutedEventArgs e)
        {
            if (Player.GameSave.Gold >= 0)
            {
                Player.GameSave.Gold -= 0;
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

