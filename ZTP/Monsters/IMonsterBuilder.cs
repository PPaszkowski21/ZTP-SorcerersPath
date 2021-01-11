using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ZTP.Monsters
{
    public interface IMonsterBuilder
    {
        Monster CreateMonster(List<Monster> monsters);
    }
}
