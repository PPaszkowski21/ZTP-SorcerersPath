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
    public class Stage
    {
        public int enemySpawnTimer = 0;
        public int enemySpawnTimerLimit;
        public int maxEnemies;
        public int enemiesToKill;
        public int enemiesSpawned = 0;
        public int playerSpeed;
        public int playerStartHP;
        private int SpawnerCounter = 0;

        private Player player;
        private Canvas myCanvas;
        private Grid myGrid;
        bool goLeft, goRight, goUp, goDown;

        public DispatcherTimer gameTimer;

        List<IMonster> monsters;
        List<IProjectile> projectiles;
        List<Rectangle> monstersAllowedToMove;
        List<Rectangle> drop;

        bool isGamePaused = false;
        bool gameOver = false;

        Label enemiesLeft;
        Label playerHP;
        Label playerGold;

        Image barHP;


        private List<Spawner> Spawners = new List<Spawner>();
        public VisualBrush Background { get; set; }

        public Stage(int nr, Canvas canvas, Grid grid, Player playerGlobal)
        {
            gameTimer = new DispatcherTimer(DispatcherPriority.Normal);
            myCanvas = canvas;
            myGrid = grid;
            StackPanel stackPanel = new StackPanel()
            {
                Name = "info",
                Orientation = Orientation.Vertical,
            };

            barHP = new Image();
            barHP.Source = new BitmapImage(new Uri((ImageManager.hpBar100)));
            barHP.Margin = new Thickness(10,-169,25,168);
            enemiesLeft = new Label();
            enemiesLeft.Foreground = new SolidColorBrush(Colors.White);
            enemiesLeft.FontSize = 16;
            enemiesLeft.FontWeight = FontWeights.ExtraBold;
            playerHP = new Label();
            playerHP.Foreground = new SolidColorBrush(Colors.White);
            playerHP.FontSize = 16;
            playerHP.FontWeight = FontWeights.ExtraBold;
            playerGold = new Label();
            playerGold.Foreground = new SolidColorBrush(Colors.White);
            playerGold.FontSize = 16;
            playerGold.FontWeight = FontWeights.ExtraBold;
            stackPanel.Children.Add(enemiesLeft);
            stackPanel.Children.Add(playerHP);
            stackPanel.Children.Add(playerGold);
            canvas.Children.Add(stackPanel);
            grid.Children.Add(barHP);
            monsters = new List<IMonster>();
            projectiles = new List<IProjectile>();
            monstersAllowedToMove = new List<Rectangle>();
            drop = new List<Rectangle>();



            switch (nr)
            {
                case 1:
                    Background = new VisualBrush(ImageManager.CreateGif(ImageManager.background1));
                    enemySpawnTimerLimit = 20;
                    maxEnemies = 1;
                    enemiesToKill = maxEnemies;
                    playerSpeed = playerGlobal.Speed;
                    playerStartHP = playerGlobal.HitPoints;
                    Spawners.Add(new Spawner(210, 210, new SkeletonCreator()));
                    break;
                case 2:
                    Background = new VisualBrush(ImageManager.CreateGif(ImageManager.background2));
                    enemySpawnTimerLimit = 1;
                    maxEnemies = 40;
                    enemiesToKill = maxEnemies;
                    playerSpeed = playerGlobal.Speed;
                    playerStartHP = 1000;
                    Spawners.Add(new Spawner(210, 210, new SkeletonCreator()));
                    Spawners.Add(new Spawner(200,1300, new SkeletonCreator()));
                    Spawners.Add(new Spawner(820,800, new PhantomCreator()));
                    break;
                case 3:
                    Background = new VisualBrush(ImageManager.CreateGif(ImageManager.background3));
                    enemySpawnTimerLimit = 20;
                    maxEnemies = 40;
                    enemiesToKill = maxEnemies;
                    playerSpeed = playerGlobal.Speed;
                    playerStartHP = 1000;
                    Spawners.Add(new Spawner(450, 1500, new PhantomCreator()));
                    Spawners.Add(new Spawner(875, 750, new SkeletonCreator()));
                    Spawners.Add(new Spawner(4, 715, new LichCreator()));
                    Spawners.Add(new Spawner(450, 4, new PhantomCreator()));
                    break;
                case 4:
                    Background = new VisualBrush(ImageManager.CreateGif(ImageManager.background4));
                    enemySpawnTimerLimit = 40;
                    maxEnemies = 1;
                    enemiesToKill = maxEnemies;
                    playerSpeed = playerGlobal.Speed;
                    playerStartHP = 1000;
                    Spawners.Add(new Spawner(640, 4, new LichCreator()));
                    Spawners.Add(new Spawner(640, 1500, new LichCreator()));
                    Spawners.Add(new Spawner(100, 700, new DemonCreator()));
                    break;
                default:
                    Background = new VisualBrush(ImageManager.CreateGif(ImageManager.background4));
                    enemySpawnTimerLimit = 10;
                    maxEnemies = 1000;
                    enemiesToKill = maxEnemies;
                    playerSpeed = playerGlobal.Speed;
                    playerStartHP = 1000;
                    Spawners.Add(new Spawner(640, 4, new LichCreator()));
                    Spawners.Add(new Spawner(640, 1500, new LichCreator()));
                    Spawners.Add(new Spawner(100, 700, new DemonCreator()));
                    break;
            }

            playerGlobal.HitPoints = playerStartHP;
            player = playerGlobal;
           
            Canvas.SetTop(player.Instance, 450);
            Canvas.SetLeft(player.Instance, 600);
            canvas.Children.Add(player.Instance);

            canvas.Background = Background;
            canvas.Focus();
        }
        public void GameLoop(object sender, EventArgs e)
        {
            //updating labels
            enemiesLeft.Content = "Enemies Left: " + enemiesToKill + " / " + maxEnemies;
            playerHP.Content = "HP: " + player.HitPoints + " / " +playerStartHP;
            playerGold.Content = "Gold: " + player.GameSave.Gold;

            //win,lose
            if (ImageManager.ChangeHpBarImage(player, playerStartHP, barHP))
            {
                ShowGameOver("You died!");
            }
            if (enemiesToKill == 0 && drop.Count == 0)
            {
                if(Game.ActualStage == 4)
                {
                    ShowGameOver("YOU WIN, CONGRATULATIONS!");
                }
                myCanvas.Children.Remove(player.Instance);
                gameTimer.Stop();
                enemiesLeft.Content = "";
                playerHP.Content = "";
                playerGold.Content = "";
                player = null;
                return;

            }

            //player movement
            player.MovePlayer(goLeft, goRight, goUp, goDown, myCanvas);
            //timers
            player.FearCooldown += 5;
            if (player.FearCooldown >= 500)
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

            enemySpawnTimer -= 1;
            if (enemySpawnTimer < 0)
            {
                //create enemies
                MakeEnemies(monstersAllowedToMove, monsters, player, myCanvas);
                enemySpawnTimer = enemySpawnTimerLimit;
            }

            var monstersBannedFromMoving = new List<Rectangle>();
            List<Rectangle> items = myCanvas.Children.OfType<Rectangle>().ToList();
            Rect playerHitBox = new Rect(Canvas.GetLeft(player.Instance), Canvas.GetTop(player.Instance), player.Instance.Width, player.Instance.Height);

            //itemsOverview
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] == null)
                {
                    continue;
                }
                //fireball fly
                else if (items[i].Name == "projectile")
                {
                    SpellOverview(player, myCanvas, monsters, projectiles, ref enemiesToKill, items[i], drop);
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
        public void NextSpawner()
        {
            SpawnerCounter++;
            if (SpawnerCounter == Spawners.Count)
            {
                SpawnerCounter = 0;
            }
        }
        public void MakeEnemies(List<Rectangle> monstersAllowedToMove, List<IMonster> monsters, Player player, Canvas myCanvas)
        {
            if (enemiesSpawned < maxEnemies)
            {
                IMonster monster = Spawners[SpawnerCounter].MonsterCreator.CreateMonster();
                monsters.Add(monster);
                player.addObserver(monster);
                Canvas.SetTop(monster.Instance, Spawners[SpawnerCounter].X);
                Canvas.SetLeft(monster.Instance, Spawners[SpawnerCounter].Y);
                myCanvas.Children.Add(monster.Instance);
                monsters.Add(monster);
                monstersAllowedToMove.Add(monster.Instance);
                enemiesSpawned++;
                NextSpawner();
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
                player.ProjectileThrow(myCanvas, projectiles, player.Projectile);
            }

            if (e.Key == Key.R && player.FearCooldown >= 1000)
            {
                player.FearCooldown = 0;
                player.FearEnemies(myCanvas);
            }

            if (e.Key == Key.Q)
            {
                player.Projectile = "fireball";
            }
            if (e.Key == Key.W)
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

            if (e.Key == Key.RightShift && gameOver == true)
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
                player.GameSave.Gold += (int)x.Tag;
            }
        }
        private void ShowGameOver(string message)
        {
            gameOver = true;
            enemiesLeft.Content += " " + message + "Press Enter to play again";
        }
    }
}
