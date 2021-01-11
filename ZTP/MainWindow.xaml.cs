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
using ZTP.EnemyAttacks;
using ZTP.Images;
using ZTP.Monsters;
using ZTP.PlayerClassess;
using ZTP.Spells;

namespace ZTP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool goLeft, goRight, goUp, goDown;

        List<Rectangle> itemsToRemove = new List<Rectangle>();
        int bulletTimer = 0;
        int bulletTimerLimit = 90;

        int enemySpawnTimer = 0;
        int enemySpawnTimerLimit = 90;
        int maxEnemies = 20;
        int enemiesKilled = 20;
        int enemiesSpawned = 0;
        int playerSpeed = 5;
        int playerStartHP = 100;

        int blinkGifTimer = 0;
        bool startGifTimer = false;


        //Player player = PlayerFactory.LoadPlayer();
        Player player = new Player(100, 0, 0);
        List<Monster> monsters = new List<Monster>();
        List<Rectangle> blinkInstances = new List<Rectangle>();


        List<Rectangle> monstersAllowedToMove = new List<Rectangle>();
        bool gameOver = false;

        DispatcherTimer gameTimer = new DispatcherTimer(DispatcherPriority.Normal);

        public MainWindow()
        {
            InitializeComponent();
            //Game g = Game.GetInstance();
            enemiesLeft.Content = "Enemies Left: " + enemiesKilled + " / " + maxEnemies;

            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

            //player init
            Canvas.SetTop(player.Instance, 344);
            Canvas.SetLeft(player.Instance, 373);
            myCanvas.Children.Add(player.Instance);

            barHP.Source = new BitmapImage(new Uri(ImageManager.hpBar100));
            avatar.Source = new BitmapImage(new Uri(ImageManager.avatar));

            ImageBrush background = new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(ImageManager.background1))
            };
            myCanvas.Background = background;

            myCanvas.Focus();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            enemiesLeft.Content = "Enemies Left: " + enemiesKilled + " / " + maxEnemies;
            playerHP.Content = "HP: " + player.HitPoints + " / " + playerStartHP;
            playerGold.Content = "Gold: " + player.Gold;
            //win,lose

            if(ImageManager.ChangeHpBarImage(player, barHP))
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
            bulletTimer -= 3;
            enemySpawnTimer -= 1;

            if(startGifTimer)
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
            if(enemySpawnTimer <0)
            {
                int enemiesToSpawn = 4;
                MakeEnemies(enemiesToSpawn, monstersAllowedToMove);
                enemySpawnTimer = enemySpawnTimerLimit;
            }
            //create arrows
            if(bulletTimer < 0)
            {
               //EnemyArrowMaker(Canvas.GetLeft(player.Instance) + 20, 10 );

                bulletTimer = bulletTimerLimit;
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

                   Rect spell = new Rect(Canvas.GetLeft(x),Canvas.GetTop(x), x.Width, x.Height);

                    foreach (var y in myCanvas.Children.OfType<Rectangle>())
                    {
                        if(y is Rectangle && (string)y.Name == "enemy")
                        {
                            Rect enemyHit = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);
                            if (spell.IntersectsWith(enemyHit))
                            {
                                itemsToRemove.Add(x);
                                var monster = monsters.FirstOrDefault(z => z.Instance == y);
                                if(monster != null)
                                {
                                    monster.HitPoints--;
                                }
                                if(monster.HitPoints<=0)
                                {
                                    itemsToRemove.Add(y);
                                    --enemiesKilled;
                                    DropCoinCoordinates.Add(new Tuple<double, double>(Canvas.GetLeft(y),Canvas.GetTop(y)));
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
                    if (monster.HitPoints == 0)
                    {
                        itemsToRemove.Add(x);
                        break;
                    }

                    if (monstersAllowedToMove.Contains(x) && !monstersBannedFromMoving.Contains(x))
                    {
                        Movement.EnemyMovement(x, player, monster.Speed);
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
                                monstersBannedFromMoving.Add(y);
                                if (!enemy2HitBox.IntersectsWith(playerHitBox))
                                {
                                    Movement.EnemyAvoidingOtherEnemy(y, enemyHitBox, enemy2HitBox, 1);
                                }
                            }
                        }
                    }

                    if (playerHitBox.IntersectsWith(enemyHitBox))
                    {
                        player.HitPoints -= monster.Damage;
                    }
                }

                ////arrow hit
                //if (x is Rectangle && (string)x.Name == "enemyArrow")
                //{
                //Canvas.SetTop(x, Canvas.GetTop(x) + 10);

                //if (Canvas.GetTop(x) > 480)
                //{
                //    itemsToRemove.Add(x);
                //}

                //Rect enemyArrowHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                //if (playerHitBox.IntersectsWith(enemyArrowHitBox))
                //{
                //    ShowGameOver("You were killed by the skeleton arrow!");
                //}
                //}

                if(x is Rectangle &&(string)x.Name == "coin")
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

            //optional speed
            //if(enemiesKilled < 10)
            //{
            //    enemySpeed = 10;
            //}
            //else
            //{
            //    enemySpeed = 5;
            //}
        }



        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Left)
            {
                goLeft = true;
            }
            if (e.Key == Key.Right)
            {
                goRight = true;
            }
            if(e.Key == Key.Up)
            {
                goUp = true;
            }
            if(e.Key == Key.Down)
            {
                goDown = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                goLeft = false;
            }
            if (e.Key == Key.Right)
            {
                goRight = false;
            }
            if(e.Key == Key.Up)
            {
                goUp = false;
            }
            if(e.Key == Key.Down)
            {
                goDown = false;
            }

            if(e.Key == Key.LeftCtrl && startGifTimer == false)
            {
                Movement.PlayerDash(player, playerSpeed, 40, myCanvas, ref startGifTimer, blinkInstances);
            }

            if(e.Key == Key.Space)
            {
                Movement.FireballThrow(myCanvas,player);
            }

            if(e.Key == Key.LeftShift)
            {
                player.notifyObservers();
            }

            if(e.Key == Key.Enter && gameOver == true)
            {
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
        }

        private void EnemyArrowMaker(double x, double y)
        {
            Arrow arrow = new Arrow();
            Canvas.SetTop(arrow.Instance, y);
            Canvas.SetLeft(arrow.Instance, x);
            myCanvas.Children.Add(arrow.Instance);
        }

        private void MakeEnemies(int enemiesToSpawn, List<Rectangle> monstersAllowedToMove)
        {
            if(enemiesSpawned<maxEnemies)
            {
                if(enemiesToSpawn>(maxEnemies-enemiesSpawned))
                {
                    enemiesToSpawn  = maxEnemies - enemiesSpawned;
                }    
                for(int i=0;i< enemiesToSpawn; i++)
                {
                    Monster monster;
                    if(i == 1)
                    {
                        BeholderBuilder builder = new BeholderBuilder();
                        monster = builder.CreateMonster(monsters);
                        player.addObserver(monster);
                    }
                    else
                    {
                        DreadIntruderBuilder builder = new DreadIntruderBuilder();
                        monster = builder.CreateMonster(monsters);
                        player.addObserver(monster);
                    }
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
