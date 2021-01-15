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
        public static Game GetInstance(Canvas mainCanvas, Grid mainGrid)
        {
            if (_instance == null)
            {
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

            enemiesLeft.Content = "Enemies Left: " + enemiesKilled + " / " + maxEnemies;

            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

            //player init
            Canvas.SetTop(player.Instance, 344);
            Canvas.SetLeft(player.Instance, 373);
            canvas.Children.Add(player.Instance);

            barHP.Source = new BitmapImage(new Uri(ImageManager.hpBar100));

            ImageBrush background = new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(ImageManager.background1))
            };

            canvas.Background = background;
            canvas.Focus();
        }

        bool goLeft, goRight, goUp, goDown;

        int maxEnemies = 20;
        int enemiesKilled = 20;
        int enemiesSpawned = 0;
        int playerSpeed = 5;
        int playerStartHP = 1000;

        DispatcherTimer gameTimer = new DispatcherTimer(DispatcherPriority.Normal);
        int blinkGifTimer = 0;
        bool startGifTimer = false;
        int enemySpawnTimer = 0;
        int enemySpawnTimerLimit = 90;

        Player player = new Player(1000, 0, 0,5);
        List<Monster> monsters = new List<Monster>();
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
            enemiesLeft.Content = "Enemies Left: " + enemiesKilled + " / " + maxEnemies;
            playerHP.Content = "HP: " + player.HitPoints + " / " + playerStartHP;
            playerGold.Content = "Gold: " + player.Gold;

            //win,lose
            if (ImageManager.ChangeHpBarImage(player, playerStartHP,barHP))
            {
                ShowGameOver("You died!");
            }
            if (enemiesKilled == 0 && drop.Count == 0)
            {
                ShowGameOver("You win, you saved the world!");
            }

            //player movement
            Movement.MovePlayer(goLeft, goRight, goUp, goDown, player,myCanvas);
            Rect playerHitBox = new Rect(Canvas.GetLeft(player.Instance), Canvas.GetTop(player.Instance), player.Instance.Width, player.Instance.Height);

            //timers
            enemySpawnTimer -= 1;
            if (startGifTimer)
            {
                //destroyInstanceOfBlink
                blinkGifTimer += 7;
                if (blinkGifTimer >= 100)
                {
                    myCanvas.Children.Remove(blinkInstances.FirstOrDefault());
                    blinkInstances.Remove(blinkInstances.FirstOrDefault());
                    myCanvas.Children.Remove(blinkInstances.FirstOrDefault());
                    blinkInstances.Remove(blinkInstances.FirstOrDefault());
                    startGifTimer = false;
                    blinkGifTimer = 0;
                }
            }
            if (enemySpawnTimer < 0)
            {
                //create enemies
                int enemiesToSpawn = 4;
                MakeEnemies(enemiesToSpawn, monstersAllowedToMove);
                enemySpawnTimer = enemySpawnTimerLimit;
            }

            var monstersBannedFromMoving = new List<Rectangle>();
            List<Rectangle> items = myCanvas.Children.OfType<Rectangle>().ToList();

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
                    Overview.SpellOverview(player, myCanvas, monsters, ref enemiesKilled, items[i],drop);
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
                Movement.PlayerDash(player, playerSpeed, 40, myCanvas, ref startGifTimer, blinkInstances);
            }

            if (e.Key == Key.Space)
            {
                Movement.FireballThrow(myCanvas, player);
            }

            if (e.Key == Key.LeftShift)
            {
                player.notifyObservers();
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
        private void MakeEnemies(int enemiesToSpawn, List<Rectangle> monstersAllowedToMove)
        {
            if (enemiesSpawned < maxEnemies)
            {
                if (enemiesToSpawn > (maxEnemies - enemiesSpawned))
                {
                    enemiesToSpawn = maxEnemies - enemiesSpawned;
                }
                for (int i = 0; i < enemiesToSpawn; i++)
                {
                    IMonsterBuilder builder;
                    Monster monster;
                    if (i == 1)
                    {
                        builder = new BeholderBuilder();

                    }
                    else if (i == 2)
                    {
                        builder = new DreadIntruderBuilder();
                    }
                    else
                    {
                        builder = new DragonBuilder();
                    }
                    monster = builder.CreateMonster(monsters);
                    player.addObserver(monster);
                    int top = 0, left = 0;
                    switch (i)
                    {
                        case 0:
                            top = 890;
                            left = 710;
                            break;
                        case 1:
                            top = 10;
                            left = 710;
                            break;
                        case 2:
                            top = 440;
                            left = 1430;
                            break;
                        case 3:
                            top = 440;
                            left = 10;
                            break;
                    }
                    Canvas.SetTop(monster.Instance, top);
                    Canvas.SetLeft(monster.Instance, left);
                    myCanvas.Children.Add(monster.Instance);
                    monsters.Add(monster);
                    monstersAllowedToMove.Add(monster.Instance);
                    enemiesSpawned++;
                }
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

