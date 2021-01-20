namespace ZTP.Monsters
{
    public class LichCreator : MonsterCreator
    {
        public override IMonster CreateMonster()
        {
            return new Lich();
        }
    }
}
