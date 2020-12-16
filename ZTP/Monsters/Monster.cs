using System.Windows.Shapes;

// Usage: Monster monster = MonsterFactory.GetMonsterById(0); returns SkeletonArcher

namespace ZTP.Monsters
{
    public class Monster
    {
        public int HitPoints { get; set; }

        public Rectangle Instance { get; set; }
    }
}
