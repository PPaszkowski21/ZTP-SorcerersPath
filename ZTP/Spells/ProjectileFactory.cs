namespace ZTP.Spells
{
    public static class ProjectileFactory
    {
        public static Projectile GetProjectileById(int _Id)
        {
            switch(_Id)
            {
                case 1:
                    return null;
                default:
                    return new Fireball();
            }
        } 
    }
}
