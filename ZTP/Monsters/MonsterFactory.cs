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
                    return new Vampire();
                case 2:
                    return new SkeletonArcher();
                case 3:
                    return new SkeletonArcher();
                default:
                    return new SkeletonArcher();
            }
        }

        public static Monster GetVampire()
        {
            return new Vampire();
        }

        public static SkeletonArcher GetSkeletonArcher()
        {
            return new SkeletonArcher();
        }
    }
}
