using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP.Images;

namespace ZTP.Monsters
{
    public class DreadIntruderBuilder : IMonsterBuilder
    {
        public Monster CreateMonster(List<Monster> monsters)
        {
            Monster dreadIntruder = new Monster(2, ImageManager.CreateGif(ImageManager.dreadintruder),"dreadintruder",90,90,3,1);
            monsters.Add(dreadIntruder);
            return dreadIntruder;
        }
    }
}
