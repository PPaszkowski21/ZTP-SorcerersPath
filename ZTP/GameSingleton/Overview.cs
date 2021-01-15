using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using ZTP.Actions;
using ZTP.Monsters;
using ZTP.PlayerClassess;

namespace ZTP.GameSingleton
{
    public class Overview
    {
        public static void EnemyOverview(Player player, List<Rectangle> monstersAllowedToMove, List<Rectangle> monstersBannedFromMoving, List<Monster> monsters, Canvas myCanvas, Rectangle x, Rect playerHitBox)
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
                            Movement.EnemyAvoidingOtherEnemy(monster2, enemyHitBox, enemy2HitBox, myCanvas);
                        }
                    }
                }
            }

            if (playerHitBox.IntersectsWith(enemyHitBox))
            {
                player.HitPoints -= monster.Damage;
            }
        }
        public static void SpellOverview(Player player, Canvas myCanvas, List<Monster> monsters, ref int enemiesKilled, Rectangle x, List<Rectangle> drop)
        {
            Movement.FireballFlying(x, myCanvas);
            Rect spell = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
            foreach (var y in myCanvas.Children.OfType<Rectangle>())
            {
                if (y is Rectangle && (string)y.Name == "enemy")
                {
                    Rect enemyHit = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);
                    if (spell.IntersectsWith(enemyHit))
                    {
                        myCanvas.Children.Remove(x);
                        var monster = monsters.FirstOrDefault(z => z.Instance == y);
                        if (monster != null)
                        {
                            monster.HitPoints--;
                        }
                        if (monster.HitPoints <= 0)
                        {
                            myCanvas.Children.Remove(y);
                            player.deleteObserver(monster);
                            monsters.Remove(monster);
                            enemiesKilled--;
                            MonsterDrop.DropCoin(myCanvas, Canvas.GetLeft(y), Canvas.GetTop(y), drop);
                        }
                        break;
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
                player.Gold++;
            }
        }
    }
}
