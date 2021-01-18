using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP.Images;

namespace ZTP.Monsters
{
    public abstract class MonsterCreator
    {
        public abstract IMonster CreateMonster();
    }
}
