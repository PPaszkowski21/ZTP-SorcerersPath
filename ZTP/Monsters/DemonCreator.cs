namespace ZTP.Monsters
{
    public class DemonCreator : MonsterCreator
    {
        public override IMonster CreateMonster()
        {
            return new Demon();
        }
    }
}
