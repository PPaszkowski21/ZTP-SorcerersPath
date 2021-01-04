using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZTP.Monsters;

namespace ZTP.monsters
{
    public class SkeletonArcher : Monster
    {
        public SkeletonArcher()
        {
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
                //Stroke = new SolidColorBrush(Colors.Red),
                Name = "enemy",
                Tag = "skeletonArcher",
                Height = 45,
                Width = 45,
                Fill = monsterSkin
            };
            HitPoints = 2;
        }
    }
}
