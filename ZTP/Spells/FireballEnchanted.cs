using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ZTP.Images;
using ZTP.Monsters;
using ZTP.PlayerClassess;

namespace ZTP.Spells
{
    public class FireballEnchanted : Fireball
    {
        public FireballEnchanted(int direction) : base(direction)
        {
            
        }

        public override EndOfSpell SpellFinishBehaviour(Canvas myCanvas, IProjectile spell, Rectangle x, List<IProjectile> projectiles, Player player, List<Rectangle> enemies, Rect spellHitBox, List<IMonster> monsters, ref int enemiesKilled, List<Rectangle> drop)
        {
            EndOfSpell endOfSpell = base.SpellFinishBehaviour(myCanvas, spell, x, projectiles, player, enemies, spellHitBox, monsters, ref enemiesKilled, drop);
            if (endOfSpell.End)
            {
                Explosion explosion = new Explosion(0);
                Canvas.SetLeft(explosion.Instance, endOfSpell.Left - 100);
                Canvas.SetTop(explosion.Instance, endOfSpell.Top- 100);
                myCanvas.Children.Add(explosion.Instance);
                projectiles.Add(explosion);
            }
            return new EndOfSpell(0, 0, false);
        }
    }
}
