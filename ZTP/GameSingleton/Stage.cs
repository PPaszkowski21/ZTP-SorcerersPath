using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ZTP.Images;
using ZTP.Monsters;
using ZTP.PlayerClassess;

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

        private List<Spawner> Spawners = new List<Spawner>();
        public VisualBrush Background { get; set; }

        public Stage(int nr)
        {
            switch (nr)
            {
                case 1:
                    Background = new VisualBrush(ImageManager.CreateGif(ImageManager.background1));
                    enemySpawnTimerLimit = 20;
                    maxEnemies = 22;
                    enemiesToKill = maxEnemies;
                    playerSpeed = 5;
                    playerStartHP = 1000;
                    Spawners.Add(new Spawner(210, 210, new SkeletonCreator()));
                    break;
                case 2:
                    Background = new VisualBrush(ImageManager.CreateGif(ImageManager.background2));
                    enemySpawnTimerLimit = 1;
                    maxEnemies = 40;
                    enemiesToKill = maxEnemies;
                    playerSpeed = 10;
                    playerStartHP = 2000;
                    Spawners.Add(new Spawner(210, 210, new SkeletonCreator()));
                    Spawners.Add(new Spawner(200,1300, new SkeletonCreator()));
                    Spawners.Add(new Spawner(820,800, new PhantomCreator()));
                    break;
                case 3:
                    Background = new VisualBrush(ImageManager.CreateGif(ImageManager.background3));
                    enemySpawnTimerLimit = 20;
                    maxEnemies = 40;
                    enemiesToKill = maxEnemies;
                    playerSpeed = 10;
                    playerStartHP = 20000;
                    Spawners.Add(new Spawner(450, 1500, new PhantomCreator()));
                    Spawners.Add(new Spawner(875, 750, new SkeletonCreator()));
                    Spawners.Add(new Spawner(4, 715, new LichCreator()));
                    Spawners.Add(new Spawner(450, 4, new PhantomCreator()));
                    break;
                case 4:
                    Background = new VisualBrush(ImageManager.CreateGif(ImageManager.background4));
                    enemySpawnTimerLimit = 40;
                    maxEnemies = 40;
                    enemiesToKill = maxEnemies;
                    playerSpeed = 10;
                    playerStartHP = 2000;
                    Spawners.Add(new Spawner(640, 4, new LichCreator()));
                    Spawners.Add(new Spawner(640, 1500, new LichCreator()));
                    Spawners.Add(new Spawner(100, 700, new DemonCreator()));
                    break;
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
        public void MakeEnemies2(int enemiesToSpawn, List<Rectangle> monstersAllowedToMove, List<IMonster> monsters, Player player, Canvas myCanvas)
        {
            if (enemiesSpawned < maxEnemies)
            {
                if (enemiesToSpawn > (maxEnemies - enemiesSpawned))
                {
                    enemiesToSpawn = maxEnemies - enemiesSpawned;
                }
                for (int i = 0; i < enemiesToSpawn; i++)
                {
                    MonsterCreator monsterCreator;
                    IMonster monster;
                    if (i == 1)
                    {
                        monsterCreator = new SkeletonCreator();

                    }
                    else if (i == 2)
                    {
                        monsterCreator = new SkeletonCreator();
                    }
                    else
                    {
                        monsterCreator = new SkeletonCreator();
                    }
                    monster = monsterCreator.CreateMonster();
                    monsters.Add(monster);
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
    }
}
