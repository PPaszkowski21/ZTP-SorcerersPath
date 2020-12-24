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
using ZTP.EnemyAttacks;
using ZTP.GameSingleton;
using ZTP.monsters;
using ZTP.Monsters;
using ZTP.Spells;

namespace ZTP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool goLeft, goRight;

        List<Rectangle> itemsToRemove = new List<Rectangle>();
        int bulletTimer = 0;
        int bulletTimerLimit = 90;

        int enemySpawnTimer = 0;
        int enemySpawnTimerLimit = 90;
        int maxEnemies = 11;
        int enemiesKilled = 11;
        int enemiesSpawned = 0;
        int enemySpeed = 6;

        bool gameOver = false;

        DispatcherTimer gameTimer = new DispatcherTimer(DispatcherPriority.Normal);
        ImageBrush playerSkin = new ImageBrush();

        [Obsolete]
        public MainWindow()
        {
            InitializeComponent();
            //Game g = Game.GetInstance();
            enemiesLeft.Content = "Enemies Left: " + enemiesKilled + " / " + maxEnemies;

            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

            playerSkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/wizard_pixel.png"));
            player.Fill = playerSkin;
            myCanvas.Focus();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            Rect playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
            if(goLeft == true && Canvas.GetLeft(player)>0)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - 10);
            }
            if(goRight == true && Canvas.GetLeft(player) + 80 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + 10);
            }

            bulletTimer -= 3;
            enemySpawnTimer -= 3;

            if(enemySpawnTimer <0)
            {
                int enemiesToSpawn = 1;
                makeEnemies(enemiesToSpawn);
                enemySpawnTimer = enemySpawnTimerLimit;
            }
            if(bulletTimer < 0)
            {
               // var monster = myCanvas.Children.OfType<Rectangle>().FirstOrDefault(x => x is Rectangle && (string)x.Tag == "enemy");

                enemyArrowMaker(Canvas.GetLeft(player) + 20, 10 );

                bulletTimer = bulletTimerLimit;
            }
            
            foreach (var x in myCanvas.Children.OfType<Rectangle>())
            {
                if(x is Rectangle && (string)x.Tag == "spell")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) - 20);

                    if(Canvas.GetTop(x)<10)
                    {
                        itemsToRemove.Add(x);
                    }

                   Rect spell = new Rect(Canvas.GetLeft(x),Canvas.GetTop(x), x.Width, x.Height);

                    foreach (var y in myCanvas.Children.OfType<Rectangle>())
                    {
                        if(y is Rectangle && (string)y.Tag == "enemy")
                        {
                            Rect enemyHit = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);
                            if (spell.IntersectsWith(enemyHit))
                            {
                                itemsToRemove.Add(x);
                                itemsToRemove.Add(y);
                                --enemiesKilled;
                                enemiesLeft.Content = "Enemies Left: " + enemiesKilled + " / " + maxEnemies;
                            }
                        }
                    }

                }

                if (x is Rectangle && (string)x.Tag == "enemy")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) + enemySpeed);

                    if(Canvas.GetLeft(x) > 820)
                    {
                        Canvas.SetLeft(x, -80);
                        Canvas.SetTop(x, Canvas.GetTop(x) + (x.Height + 10));
                    }

                    Rect enemyHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if(playerHitBox.IntersectsWith(enemyHitBox))
                    {
                        showGameOver("You were killed by the skeletons!");
                    }
                }
                if (x is Rectangle && (string)x.Tag == "enemyArrow")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + 10);

                    if (Canvas.GetTop(x) > 480)
                    {
                        itemsToRemove.Add(x);
                    }

                    Rect enemyArrowHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox.IntersectsWith(enemyArrowHitBox))
                    {
                        showGameOver("You were killed by the skeleton arrow!");
                    }
                }
            }

            foreach (Rectangle i in itemsToRemove)
            {
                myCanvas.Children.Remove(i);
            }
            if(enemiesKilled < 10)
            {
                enemySpeed = 10;
            }
            else
            {
                enemySpeed = 5;
            }
            if(enemiesKilled == 0)
            {
                showGameOver("You win, you saved the world!");
            }
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

            if(e.Key == Key.Space)
            {
                Fireball fireball = new Fireball();
                Canvas.SetTop(fireball.Instance, Canvas.GetTop(player) - fireball.Instance.ActualHeight);
                Canvas.SetLeft(fireball.Instance, Canvas.GetLeft(player) + player.Width / 2);
                myCanvas.Children.Add(fireball.Instance);
            }

            if(e.Key == Key.Enter && gameOver == true)
            {
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
        }

        private void enemyArrowMaker(double x, double y)
        {
            Arrow arrow = new Arrow();
            Canvas.SetTop(arrow.Instance, y);
            Canvas.SetLeft(arrow.Instance, x);
            myCanvas.Children.Add(arrow.Instance);
        }

        private void makeEnemies(int enemiesToSpawn)
        {
            int left = 800;
            if(enemiesSpawned<maxEnemies)
            {
                if(enemiesToSpawn>(maxEnemies-enemiesSpawned))
                {
                    enemiesToSpawn  = maxEnemies - enemiesSpawned;
                }    
                for(int i=0;i< enemiesToSpawn; i++)
                {
                    //SkeletonArcher skeletonArcher = new SkeletonArcher();
                    Monster monster = MonsterFactory.GetVampire();
                    Canvas.SetTop(monster.Instance, 30);
                    Canvas.SetLeft(monster.Instance, left);
                    myCanvas.Children.Add(monster.Instance);
                    left -= 60;
                    enemiesSpawned++;
                }
            }
        }

        private void showGameOver(string message)
        {
            gameOver = true;
            gameTimer.Stop();
            enemiesLeft.Content += " " + message + "Press Enter to play again";
        }
    }
}
