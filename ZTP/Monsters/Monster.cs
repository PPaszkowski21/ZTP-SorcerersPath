using System.Windows.Media;
using System.Windows.Shapes;

// Usage: Monster monster = MonsterFactory.GetMonsterById(0); returns SkeletonArcher

namespace ZTP.Monsters
{
    abstract public class Monster
    {
        public int HitPoints { get; set; }

        public Rectangle Instance { get; set; }

        public ImageBrush monsterSkin = new ImageBrush();
    }
}
