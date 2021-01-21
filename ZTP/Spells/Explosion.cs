using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ZTP.GameSingleton;
using ZTP.Images;
using ZTP.Monsters;
using ZTP.PlayerClassess;

namespace ZTP.Spells
{
    public class Explosion : IProjectile
    {
        public Rectangle Instance { get; set; }
        public int Direction { get; set; }
        public int Damage { get; set; }
        public int Speed { get; set; }
        public int Timer { get; set; }
        public VisualBrush Image { get; set; }
        public Explosion(int direction)
        {
            Direction = direction;
            Damage = 10;
            Speed = 0;
            Timer = 100;
            Image = new VisualBrush(ImageManager.CreateGif(ImageManager.explosion));
            Instance = new Rectangle
            {
                Name = "projectile",
                Tag = "explosion",
                Height = 200,
                Width = 200,
                Fill = Image
            };
        }
        public void SpellBehaviour(Canvas myCanvas, List<IProjectile> projectiles)
        {
            Timer -= 5;
            if (Timer < 0)
            {
                projectiles.Remove(this);
                myCanvas.Children.Remove(Instance);
            }
        }
        public virtual EndOfSpell SpellFinishBehaviour(Canvas myCanvas, IProjectile spell, Rectangle x, List<IProjectile> projectiles, Player player, List<Rectangle> enemies, Rect spellHitBox, List<IMonster> monsters, ref int enemiesKilled, List<Rectangle> drop)
        {
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
                    continue;
                }
            }
            return new EndOfSpell(0, 0, false);
        }
    }
}
