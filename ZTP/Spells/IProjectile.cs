using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using ZTP.Monsters;
using ZTP.PlayerClassess;

namespace ZTP.Spells
{
    public interface IProjectile
    {
        Rectangle Instance { get; set; }
        int Direction { get; set; }
        int Damage { get; set; }
        int Speed { get; set; }
        void SpellBehaviour(Canvas myCanvas, List<IProjectile> projectiles);
        EndOfSpell SpellFinishBehaviour(Canvas myCanvas, IProjectile spell, Rectangle x, List<IProjectile> projectiles, Player player, List<Rectangle> enemies, Rect spellHitBox, List<IMonster> monsters, ref int enemiesKilled, List<Rectangle> drop);
    }
}
