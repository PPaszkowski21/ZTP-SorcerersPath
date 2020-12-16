using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ZTP.monsters
{
    public class SkeletonArcher
    {
        public Rectangle Instance { get; set; }
        public SkeletonArcher()
        {
            ImageBrush monsterSkin = new ImageBrush();
            Random rnd = new Random();
            int enemyImages = rnd.Next(1, 4);
            switch (enemyImages)
            {
                case 1:
                    monsterSkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/skeletal_archer_pixel.PNG"));
                    break;
                case 2:
                    monsterSkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/skeletal_archer2_pixel.PNG"));
                    break;
                case 3:
                    monsterSkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/skeletal_archer3_pixel.PNG"));
                    break;
            }
            Instance = new Rectangle
            {
                Tag = "enemy",
                Height = 45,
                Width = 45,
                Fill = monsterSkin
            };
        }
    }
}
