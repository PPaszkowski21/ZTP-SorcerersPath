using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace ZTP.Spells
{
    public interface IProjectile
    {
        Rectangle Instance { get; set; }
        int Direction { get; set; }
        int Damage { get; set; }
        int Speed { get; set; }
        void SpellBehaviour(Canvas myCanvas, List<IProjectile> projectiles);
    }
}
