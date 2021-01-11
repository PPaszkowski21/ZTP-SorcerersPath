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
            Monster beholder = new Monster(3, ImageManager.CreateGif(ImageManager.beholder),"beholder",150,150,1,3);
            monsters.Add(beholder);
            return beholder;
        }
    }
}
