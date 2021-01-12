using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTP.Images;

namespace ZTP.Monsters
{
    public class DragonBuilder : IMonsterBuilder
    {
        public Monster CreateMonster(List<Monster> monsters)
        {
            Monster dragon = new Monster(10, ImageManager.CreateGif(ImageManager.dragon), "dragon", 50, 50, 3, 4, GetImagesPaths());
            monsters.Add(dragon);
            return dragon;
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
