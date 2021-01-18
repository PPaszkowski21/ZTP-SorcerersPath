using System.Windows.Shapes;

namespace ZTP.Spells
{
    public interface IProjectile
    {
        Rectangle Instance { get; set; }
        int Direction { get; set; }
    }
}
