using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAnimatedGif;
using ZTP.GameSingleton;
using ZTP.Images;
using ZTP.Monsters;
using ZTP.PlayerClassess;

namespace ZTP.Spells
{
    public class Fireball: IProjectile
    {
        public Rectangle Instance { get; set; }
        public int Direction { get; set; }
        public int Damage { get; set; }
        public int Speed { get; set; }
        public List<VisualBrush> Images { get; set; }
        public Fireball(int direction)
        {
            Direction = direction;
            Damage = 100;
            Speed = 20;
            int height = 0;
            int width = 0;
            Images = new List<VisualBrush>();
            Images.Add(new VisualBrush(ImageManager.CreateGif(ImageManager.fireballDown)));
            Images.Add(new VisualBrush(ImageManager.CreateGif(ImageManager.fireballLeft)));
            Images.Add(new VisualBrush(ImageManager.CreateGif(ImageManager.fireballUp)));
            Images.Add(new VisualBrush(ImageManager.CreateGif(ImageManager.fireballRight)));
            VisualBrush projectileSkin = new VisualBrush();
            switch(Direction)
            {
                case 0:
                    height = 47;
                    width = 28;
                    projectileSkin = Images[0];
                    break;
                case 1:
                    height = 28;
                    width = 47;
                    projectileSkin = Images[1];
                    break;
                case 2:
                    height = 47;
                    width = 28;
                    projectileSkin = Images[2];
                    break;
                case 3:
                    height = 28;
                    width = 47;
                    projectileSkin = Images[3];
                    break;
            }
            Instance = new Rectangle
            {
                Name = "projectile",
                Tag = "fireball",
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
                    double top = Canvas.GetTop(x);
                    double left = Canvas.GetLeft(x);
                    myCanvas.Children.Remove(x);
                    projectiles.Remove(spell);
                    return new EndOfSpell(top, left, true);
                    //if ((string)spell.Instance.Tag == "fireball")
                    //{
                    //    myCanvas.Children.Remove(x);
                    //    projectiles.Remove(spell);
                    //    break;
                    //}
                    //else if ((string)spell.Instance.Tag == "toxicbolt" || (string)spell.Instance.Tag == "lightning")
                    //{
                    //    continue;
                    //}
                }
            }
            return new EndOfSpell(0, 0, false);
        }
    }
}
