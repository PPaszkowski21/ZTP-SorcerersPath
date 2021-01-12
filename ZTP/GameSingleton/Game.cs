using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public static Game GetInstance(Canvas mainCanvas)
        {
            if (_instance == null)
            {
                _instance = new Game(mainCanvas);
                myCanvas = mainCanvas;
            }
            return _instance;
        }
        private Game(Canvas canvas)
        {
            foreach (var stackPanel in canvas.Children.OfType<StackPanel>())
            {
                foreach (var item in stackPanel.Children.OfType<Control>())
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

                foreach (var item in stackPanel.Children.OfType<Image>())
                {
                    if (item.Name == "barHP")
                    {
                        barHP = item;
                    }
                    if (item.Name == "avatar")
                    {
                        avatar = item;
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
            avatar.Source = new BitmapImage(new Uri(ImageManager.avatar));

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
        int playerStartHP = 100;

        DispatcherTimer gameTimer = new DispatcherTimer(DispatcherPriority.Normal);
        int blinkGifTimer = 0;
        bool startGifTimer = false;
        int enemySpawnTimer = 0;
        int enemySpawnTimerLimit = 90;

        Player player = new Player(1000, 0, 0);
        List<Monster> monsters = new List<Monster>();
        List<Rectangle> blinkInstances = new List<Rectangle>();
        List<Rectangle> monstersAllowedToMove = new List<Rectangle>();
        List<Rectangle> itemsToRemove = new List<Rectangle>();

        bool isGamePaused = false;
        bool gameOver = false;

        Label enemiesLeft;
        Label playerHP;
        Label playerGold;

        Image barHP;
        Image avatar;


        private void GameLoop(object sender, EventArgs e)
        {
            enemiesLeft.Content = "Enemies Left: " + enemiesKilled + " / " + maxEnemies;
            playerHP.Content = "HP: " + player.HitPoints + " / " + playerStartHP;
            playerGold.Content = "Gold: " + player.Gold;

            //win,lose

            if (ImageManager.ChangeHpBarImage(player, barHP))
            {
                ShowGameOver("You died!");
            }
            if (enemiesKilled == 0)
            {
                ShowGameOver("You win, you saved the world!");
            }


            Rect playerHitBox = new Rect(Canvas.GetLeft(player.Instance), Canvas.GetTop(player.Instance), player.Instance.Width, player.Instance.Height);

            //player movement
            Movement.MovePlayer(goLeft, goRight, goUp, goDown, player, playerSpeed);

            //timers
            enemySpawnTimer -= 1;

            if (startGifTimer)
            {
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

            //create enemies
            if (enemySpawnTimer < 0)
            {
                int enemiesToSpawn = 4;
                MakeEnemies(enemiesToSpawn, monstersAllowedToMove);
                enemySpawnTimer = enemySpawnTimerLimit;
            }


            var DropCoinCoordinates = new List<Tuple<double, double>>();
            var monstersBannedFromMoving = new List<Rectangle>();

            //hitboxes, occurences
            foreach (var x in myCanvas.Children.OfType<Rectangle>())
            {
                //fireball fly
                if (x is Rectangle && (string)x.Name == "fireball")
                {
                    Movement.FireballFlying(x, itemsToRemove);

                    Rect spell = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    foreach (var y in myCanvas.Children.OfType<Rectangle>())
                    {
                        if (y is Rectangle && (string)y.Name == "enemy")
                        {
                            Rect enemyHit = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);
                            if (spell.IntersectsWith(enemyHit))
                            {
                                itemsToRemove.Add(x);
                                var monster = monsters.FirstOrDefault(z => z.Instance == y);
                                if (monster != null)
                                {
                                    monster.HitPoints--;
                                }
                                if (monster.HitPoints <= 0)
                                {
                                    itemsToRemove.Add(y);
                                    player.deleteObserver(monster);
                                    monsters.Remove(monster);
                                    --enemiesKilled;
                                    DropCoinCoordinates.Add(new Tuple<double, double>(Canvas.GetLeft(y), Canvas.GetTop(y)));
                                }
                                break;
                            }
                        }
                    }
                }

                //enemy movement
                if (x is Rectangle && (string)x.Name == "enemy")
                {
                    Rect enemyHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    Monster monster = monsters.FirstOrDefault(c => c.Instance == x);

                    if (monstersAllowedToMove.Contains(x) && !monstersBannedFromMoving.Contains(x))
                    {
                        Movement.EnemyMovement(monster, player);
                    }
                    bool isFirst = true;
                    foreach (var y in myCanvas.Children.OfType<Rectangle>())
                    {
                        if (isFirst == true)
                        {
                            isFirst = false;
                            continue;
                        }

                        if (y is Rectangle && (string)y.Name == "enemy")
                        {
                            Rect enemy2HitBox = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);
                            if (enemy2HitBox.IntersectsWith(enemyHitBox))
                            {
                                Monster monster2 = monsters.FirstOrDefault(c => c.Instance == y);
                                monstersBannedFromMoving.Add(y);
                                if (!enemy2HitBox.IntersectsWith(playerHitBox))
                                {
                                    Movement.EnemyAvoidingOtherEnemy(monster2, enemyHitBox, enemy2HitBox);
                                }
                            }
                        }
                    }

                    if (playerHitBox.IntersectsWith(enemyHitBox))
                    {
                        player.HitPoints -= monster.Damage;
                    }
                }

                if (x is Rectangle && (string)x.Name == "coin")
                {
                    Rect coinHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerHitBox.IntersectsWith(coinHitBox))
                    {
                        itemsToRemove.Add(x);
                        player.Gold++;
                    }
                }
            }

            foreach (var item in DropCoinCoordinates)
            {
                MonsterDrop.DropCoin(myCanvas, item.Item1, item.Item2);
            }

            //clearing items
            foreach (Rectangle i in itemsToRemove)
            {
                myCanvas.Children.Remove(i);
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

