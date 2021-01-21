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
    public class ToxicBolt : IProjectile
    {
        public Rectangle Instance { get; set; }
        public int Direction { get; set; }
        public int Damage { get; set; }
        public int Speed { get; set; }
        public List<VisualBrush> Images { get; set; }
        public ToxicBolt(int direction)
        {
            Direction = direction;
            Damage = 15;
            Speed = 20;
            int height = 75;
            int width = 75;
            Images = new List<VisualBrush>();
            Images.Add(new VisualBrush(ImageManager.CreateGif(ImageManager.ToxicBoltDown)));
            Images.Add(new VisualBrush(ImageManager.CreateGif(ImageManager.ToxicBoltLeft)));
            Images.Add(new VisualBrush(ImageManager.CreateGif(ImageManager.ToxicBoltUp)));
            Images.Add(new VisualBrush(ImageManager.CreateGif(ImageManager.ToxicBoltRight)));
            VisualBrush projectileSkin = new VisualBrush();
            switch (Direction)
            {
                case 0:
                    projectileSkin = Images[0];
                    break;
                case 1:
                    projectileSkin = Images[1];
                    break;
                case 2:
                    projectileSkin = Images[2];
                    break;
                case 3:
                    projectileSkin = Images[3];
                    break;
            }
            Instance = new Rectangle
            {
                Name = "projectile",
                Tag = "toxicbolt",
                Height = height,
                Width = width,
                Fill = projectileSkin
            };
        }
        public void SpellBehaviour(Canvas myCanvas, List<IProjectile> projectiles)
        {
            switch (Direction)
            {
                case 0:
                    Canvas.SetTop(Instance, Canvas.GetTop(Instance) + Speed);
                    break;
                case 1:
                    Canvas.SetLeft(Instance, Canvas.GetLeft(Instance) - Speed);
                    break;
                case 2:
                    Canvas.SetTop(Instance, Canvas.GetTop(Instance) - Speed);
                    break;
                case 3:
                    Canvas.SetLeft(Instance, Canvas.GetLeft(Instance) + Speed);
                    break;
            }
            if (Helper.IsOnTheBorder(Instance, myCanvas))
            {
                projectiles.Remove(this);
                myCanvas.Children.Remove(Instance);
            }
        }
        public EndOfSpell SpellFinishBehaviour(Canvas myCanvas, IProjectile spell, Rectangle x, List<IProjectile> projectiles, Player player, List<Rectangle> enemies, Rect spellHitBox, List<IMonster> monsters, ref int enemiesKilled, List<Rectangle> drop)
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
