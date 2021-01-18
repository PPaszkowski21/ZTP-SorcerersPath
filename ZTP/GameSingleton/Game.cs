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

namespace ZTP.GameSingleton
{
    public class Game
    {
        private static Game _instance;
        private static Canvas myCanvas;
        private static Grid myGrid;
        private static Stage stage;
        public static Game GetInstance(Canvas mainCanvas, Grid mainGrid)
        {
            if (_instance == null)
            {
                stage = new Stage(4);
                _instance = new Game(mainCanvas, mainGrid);
                myCanvas = mainCanvas;
                myGrid = mainGrid;
            }
            return _instance;
        }
        private Game(Canvas canvas, Grid grid)
        {
            foreach (var item in grid.Children.OfType<Image>())
            {
                if(item.Name == "hpBar")
                {
                    barHP = item;
                }
            }
            foreach (var item in canvas.Children.OfType<StackPanel>().FirstOrDefault().Children.OfType<Control>())
            {
                if (item is Label label)
                {
                    if (label.Name == "enemiesLeft")
                    {
                        enemiesLeft = label;
                    }
                    if (label.Name == "playerHP")
                    {
                        playerHP = label;
                    }
                    if (label.Name == "playerGold")
                    {
                        playerGold = label;
                    }
                }
            }

            enemiesLeft.Content = "Enemies Left: " + stage.enemiesToKill + " / " + stage.maxEnemies;

            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

            //player init
            Canvas.SetTop(player.Instance, 450);
            Canvas.SetLeft(player.Instance,600);
            canvas.Children.Add(player.Instance);

            barHP.Source = new BitmapImage(new Uri(ImageManager.hpBar100));

            canvas.Background = stage.Background;
            canvas.Focus();
        }

        bool goLeft, goRight, goUp, goDown;

        DispatcherTimer gameTimer = new DispatcherTimer(DispatcherPriority.Normal);
        int blinkGifTimer = 0;
        bool startGifTimer = false;

        Player player = new Player(stage.playerStartHP,stage.playerSpeed);
        List<IMonster> monsters = new List<IMonster>();
        List<Rectangle> blinkInstances = new List<Rectangle>();
        List<Rectangle> monstersAllowedToMove = new List<Rectangle>();
        List<Rectangle> drop = new List<Rectangle>();

        bool isGamePaused = false;
        bool gameOver = false;

        Label enemiesLeft;
        Label playerHP;
        Label playerGold;

        Image barHP;


        private void GameLoop(object sender, EventArgs e)
        {
            //updating labels
            enemiesLeft.Content = "Enemies Left: " + stage.enemiesToKill + " / " + stage.maxEnemies;
            playerHP.Content = "HP: " + player.HitPoints + " / " + stage.playerStartHP;
            playerGold.Content = "Gold: " + player.Gold;

            //win,lose
            if (ImageManager.ChangeHpBarImage(player, stage.playerStartHP,barHP))
            {
                ShowGameOver("You died!");
            }
            if (stage.enemiesToKill == 0 && drop.Count == 0)
            {
                ShowGameOver("You win, you saved the world!");
            }

            //player movement
            player.MovePlayer(goLeft, goRight, goUp, goDown, myCanvas);

            //timers
            stage.enemySpawnTimer -= 1;
            if (startGifTimer)
            {
                //destroyInstanceOfBlink
                blinkGifTimer += 25;
                if (blinkGifTimer >= 260)
                {
                    myCanvas.Children.Remove(blinkInstances.FirstOrDefault());
                    blinkInstances.Remove(blinkInstances.FirstOrDefault());
                    myCanvas.Children.Remove(blinkInstances.FirstOrDefault());
                    blinkInstances.Remove(blinkInstances.FirstOrDefault());
                    startGifTimer = false;
                    blinkGifTimer = 0;
                }
            }
            if (stage.enemySpawnTimer < 0)
            {
                //create enemies
                stage.MakeEnemies(monstersAllowedToMove, monsters, player, myCanvas);
                stage.enemySpawnTimer = stage.enemySpawnTimerLimit;
            }

            var monstersBannedFromMoving = new List<Rectangle>();
            List<Rectangle> items = myCanvas.Children.OfType<Rectangle>().ToList();
            Rect playerHitBox = new Rect(Canvas.GetLeft(player.Instance), Canvas.GetTop(player.Instance), player.Instance.Width, player.Instance.Height);

            //itemsOverview
            for (int i = 0; i < items.Count; i++)
            {
                if(items[i] == null)
                {
                    continue;
                }
                //fireball fly
                else if (items[i].Name == "fireball")
                {
                    Overview.SpellOverview(player, myCanvas, monsters, ref stage.enemiesToKill, items[i],drop);
                }

                //enemy movement
                else if (items[i].Name == "enemy")
                {
                    Overview.EnemyOverview(player, monstersAllowedToMove, monstersBannedFromMoving, monsters, myCanvas, items[i], playerHitBox);
                }

                //coin gathering
                else if (items[i].Name == "coin")
                {
                    Overview.CoinOverview(player, myCanvas, items[i], playerHitBox, drop);
                }
            }
        }
        public void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                goLeft = true;
            }
            if (e.Key == Key.Right)
            {
                goRight = true;
            }
            if (e.Key == Key.Up)
            {
                goUp = true;
            }
            if (e.Key == Key.Down)
            {
                goDown = true;
            }
        }
        public void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                goLeft = false;
            }
            if (e.Key == Key.Right)
            {
                goRight = false;
            }
            if (e.Key == Key.Up)
            {
                goUp = false;
            }
            if (e.Key == Key.Down)
            {
                goDown = false;
            }

            if (e.Key == Key.LeftCtrl && startGifTimer == false)
            {
                player.PlayerDash(player, 40, myCanvas, ref startGifTimer, blinkInstances);
            }

            if (e.Key == Key.Space)
            {
                player.FireballThrow(myCanvas);
            }

            if (e.Key == Key.LeftShift)
            {
                player.notifyObservers(new FearRunningStrategy());
            }

            if (e.Key == Key.Escape)
            {
                isGamePaused = !isGamePaused;
                if (isGamePaused)
                {
                    gameTimer.Stop();
                }
                else
                {
                    gameTimer.Start();
                }
            }

            if (e.Key == Key.Enter && gameOver == true)
            {
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
        }
        private void ShowGameOver(string message)
        {
            gameOver = true;
            gameTimer.Stop();
            enemiesLeft.Content += " " + message + "Press Enter to play again";
        }
    }
}

