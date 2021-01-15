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
            Monster dreadIntruder = new Monster(1, ImageManager.CreateGif(ImageManager.dreadintruder), "dreadintruder", 90, 90, 3, 1, GetImagesPaths());
            monsters.Add(dreadIntruder);
            return dreadIntruder;
        }

        public List<string> GetImagesPaths()
        {
            List<string> strings = new List<string>();
            strings.Add(ImageManager.testDown);
            strings.Add(ImageManager.testLeft);
            strings.Add(ImageManager.testUp);
            strings.Add(ImageManager.testRight);
            return strings;
        }
    }
}
