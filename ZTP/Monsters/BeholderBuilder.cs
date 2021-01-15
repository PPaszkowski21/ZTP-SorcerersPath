using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAnimatedGif;
using ZTP.Images;

namespace ZTP.Monsters
{
    public class BeholderBuilder : IMonsterBuilder
    {
        public Monster CreateMonster(List<Monster> monsters)
        {
            Monster beholder = new Monster(1, ImageManager.CreateGif(ImageManager.beholder), "beholder", 150, 150, 1, 3, GetImagesPaths());
            monsters.Add(beholder);
            return beholder;
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
