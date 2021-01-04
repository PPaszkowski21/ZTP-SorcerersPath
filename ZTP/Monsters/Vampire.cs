using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZTP.Monsters;

namespace ZTP.Monsters
{
    public class Vampire : Monster
    {
        public Vampire()
        {
            monsterSkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/vampire_pixel.PNG"));
            Instance = new Rectangle
            {
                Name = "enemy",
                Tag = "vampire",
                Height = 90,
                Width = 90,
                Fill = monsterSkin
            };
            HitPoints = 4;
        }
    }
}
