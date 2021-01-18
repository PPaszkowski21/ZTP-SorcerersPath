using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP.Monsters
{
    public class PhantomCreator : MonsterCreator
    {
        public override IMonster CreateMonster()
        {
            return new Phantom();
        }
    }
}
