using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        private static Canvas myCanvas;
        private static Stage stage;
        public static Game GetInstance(Canvas mainCanvas, Grid mainGrid)
        {
            if (_instance == null)
            {
                stage = new Stage(4);
                _instance = new Game(mainCanvas, mainGrid);
                myCanvas = mainCanvas;
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

        Player player = new Player(stage.playerStartHP,stage.playerSpeed);
        List<IMonster> monsters = new List<IMonster>();
        List<IProjectile> projectiles = new List<IProjectile>();
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
            player.FearCooldown += 5;
            if(player.FearCooldown >= 500)
            {
                player.notifyObservers(new RegularMovementStrategy());
            }
            player.ProjectileCooldown += 5;
            player.BlinkCooldown += 25;
            if (player.BlinkCooldown >= 260 && player.Blinks.Any())
            {
                myCanvas.Children.Remove(player.Blinks.FirstOrDefault().Instance);
                myCanvas.Children.Remove(player.Blinks.FirstOrDefault().Instance);
                player.Blinks.Remove(player.Blinks.FirstOrDefault());
                player.Blinks.Remove(player.Blinks.FirstOrDefault());
            }
            
            stage.enemySpawnTimer -= 1;
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
                else if (items[i].Name == "projectile")
                {
                    SpellOverview(player, myCanvas, monsters, projectiles, ref stage.enemiesToKill, items[i],drop);
                }

                //enemy movement
                else if (items[i].Name == "enemy")
                {
                   EnemyOverview(player, monstersAllowedToMove, monstersBannedFromMoving, monsters, myCanvas, items[i], playerHitBox);
                }

                //coin gathering
                else if (items[i].Name == "coin")
                {
                    CoinOverview(player, myCanvas, items[i], playerHitBox, drop);
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

            if (e.Key == Key.LeftCtrl && player.BlinkCooldown >= 260)
            {
                player.BlinkCooldown = 0;
                player.PlayerDash(40, myCanvas);
            }

            if (e.Key == Key.Space && player.ProjectileCooldown >= 100)
            {
                player.ProjectileCooldown = 0;
                player.ProjectileThrow(myCanvas,projectiles,player.Projectile);
            }

            if (e.Key == Key.R && player.FearCooldown >= 1000)
            {
                player.FearCooldown = 0;
                player.FearEnemies(myCanvas);
            }

            if(e.Key == Key.Q)
            {
                player.Projectile = "fireball";
            }
            if(e.Key == Key.W)
            {
                player.Projectile = "toxicbolt";
            }
            if (e.Key == Key.E)
            {
                player.Projectile = "lightning";
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
        public void EnemyOverview(Player player, List<Rectangle> monstersAllowedToMove, List<Rectangle> monstersBannedFromMoving, List<IMonster> monsters, Canvas myCanvas, Rectangle x, Rect playerHitBox)
        {
            Rect enemyHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
            IMonster monster = monsters.FirstOrDefault(c => c.Instance == x);

            if (monstersAllowedToMove.Contains(x) && !monstersBannedFromMoving.Contains(x))
            {
                monster.Move(player, monster, myCanvas);
            }
            bool isFirst = true;
            foreach (var y in myCanvas.Children.OfType<Rectangle>())
            {
                if (isFirst)
                {
                    isFirst = false;
                    continue;
                }

                if (y is Rectangle && (string)y.Name == "enemy")
                {
                    Rect enemy2HitBox = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);
                    if (enemy2HitBox.IntersectsWith(enemyHitBox))
                    {
                        IMonster monster2 = monsters.FirstOrDefault(c => c.Instance == y);
                        monstersBannedFromMoving.Add(y);
                        if (!enemy2HitBox.IntersectsWith(playerHitBox))
                        {
                            monster2.CollisionAvoiding(monster2, enemyHitBox, enemy2HitBox, myCanvas);
                        }
                    }
                }
            }

            if (playerHitBox.IntersectsWith(enemyHitBox))
            {
                player.HitPoints -= monster.Damage;
            }
        }
        public static void SpellOverview(Player player, Canvas myCanvas, List<IMonster> monsters, List<IProjectile> projectiles, ref int enemiesKilled, Rectangle x, List<Rectangle> drop)
        {
            IProjectile spell = projectiles.FirstOrDefault(c => c.Instance == x);
            spell.SpellBehaviour(myCanvas, projectiles);
            Rect spellHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
            List<Rectangle> enemies = myCanvas.Children.OfType<Rectangle>().ToList();
            enemies = enemies.Where(e => (string)e.Name == "enemy").ToList();
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    continue;
                }
                Rect enemyHitBox = new Rect(Canvas.GetLeft(enemies[i]), Canvas.GetTop(enemies[i]), enemies[i].Width, enemies[i].Height);
                if (spellHitBox.IntersectsWith(enemyHitBox))
                {
                    var monster = monsters.FirstOrDefault(z => z.Instance == enemies[i]);
                    if (monster != null)
                    {
                        monster.HitPoints -= spell.Damage;
                    }
                    if (monster.HitPoints <= 0)
                    {
                        myCanvas.Children.Remove(enemies[i]);
                        player.deleteObserver(monster);
                        monsters.Remove(monster);
                        enemiesKilled--;
                        monster.DropCoin(myCanvas, Canvas.GetLeft(enemies[i]), Canvas.GetTop(enemies[i]), drop);
                    }
                    if ((string)spell.Instance.Tag == "fireball")
                    {
                        myCanvas.Children.Remove(x);
                        projectiles.Remove(spell);
                        break;
                    }
                    else if ((string)spell.Instance.Tag == "toxicbolt" || (string)spell.Instance.Tag == "lightning")
                    {
                        continue;
                    }
                }
            }
        }
        public static void CoinOverview(Player player, Canvas myCanvas, Rectangle x, Rect playerHitBox, List<Rectangle> drop)
        {
            Rect coinHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
            if (playerHitBox.IntersectsWith(coinHitBox))
            {
                myCanvas.Children.Remove(x);
                drop.Remove(x);
                player.Gold += (int)x.Tag;
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

