using ZTP.monsters;

namespace ZTP.Monsters
{
    public static class MonsterFactory
    {
        public static Monster GetMonsterById(int _Id)
        {
            switch(_Id) 
            {
                case 1:
                    return null;
                default:
                    return new SkeletonArcher();
            }
        }
    }
}
