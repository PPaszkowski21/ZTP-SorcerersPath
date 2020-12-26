﻿using System;
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
        int maxEnemies = 11;
        int enemiesKilled = 11;
        int enemiesSpawned = 0;
        int enemySpeed = 6;
        int playerSpeed = 7;

        Player player = PlayerFactory.LoadPlayer();

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

            Canvas.SetTop(player.Instance, 344);
            Canvas.SetLeft(player.Instance, 373);
            myCanvas.Children.Add(player.Instance);

            myCanvas.Focus();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            Rect playerHitBox = new Rect(Canvas.GetLeft(player.Instance), Canvas.GetTop(player.Instance), player.Instance.Width, player.Instance.Height);

            Movement.MovePlayer(goLeft, goRight, goUp, goDown, player, playerSpeed);

            bulletTimer -= 3;
            enemySpawnTimer -= 3;

            if(enemySpawnTimer <0)
            {
                int enemiesToSpawn = 1;
                MakeEnemies(enemiesToSpawn);
                enemySpawnTimer = enemySpawnTimerLimit;
            }
            if(bulletTimer < 0)
            {
               //EnemyArrowMaker(Canvas.GetLeft(player.Instance) + 20, 10 );

                bulletTimer = bulletTimerLimit;
            }
            
            foreach (var x in myCanvas.Children.OfType<Rectangle>())
            {
                if(x is Rectangle && (string)x.Name == "fireball")
                {
                    Movement.FireballFlying(x);

                    if(Canvas.GetTop(x)<10)
                    {
                        itemsToRemove.Add(x);
                    }

                   Rect spell = new Rect(Canvas.GetLeft(x),Canvas.GetTop(x), x.Width, x.Height);

                    foreach (var y in myCanvas.Children.OfType<Rectangle>())
                    {
                        if(y is Rectangle && (string)y.Name == "enemy")
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

                if (x is Rectangle && (string)x.Name == "enemy")
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
                        ShowGameOver("You were killed by the skeletons!");
                    }
                }
                if (x is Rectangle && (string)x.Name == "enemyArrow")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + 10);

                    if (Canvas.GetTop(x) > 480)
                    {
                        itemsToRemove.Add(x);
                    }

                    Rect enemyArrowHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox.IntersectsWith(enemyArrowHitBox))
                    {
                        ShowGameOver("You were killed by the skeleton arrow!");
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
                ShowGameOver("You win, you saved the world!");
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


            if(e.Key == Key.Space)
            {
                Movement.FireballThrow(myCanvas,player);
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

        private void MakeEnemies(int enemiesToSpawn)
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
                    Monster monster = MonsterFactory.GetSkeletonArcher();
                    Canvas.SetTop(monster.Instance, 30);
                    Canvas.SetLeft(monster.Instance, left);
                    myCanvas.Children.Add(monster.Instance);
                    left -= 60;
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
