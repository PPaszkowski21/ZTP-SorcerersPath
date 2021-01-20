using ZTP.Monsters;

namespace ZTP.GameSingleton
{
    public class Spawner
    {
        public double X { get; set; }
        public double Y { get; set; }
        public MonsterCreator MonsterCreator { get; set; }

        public Spawner(double x, double y, MonsterCreator monsterCreator)
        {
            X = x;
            Y = y;
            MonsterCreator = monsterCreator;
        }
    }
}
