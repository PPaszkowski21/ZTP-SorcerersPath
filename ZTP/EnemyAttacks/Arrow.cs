using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ZTP.EnemyAttacks
{
    public class Arrow
    {
        public Rectangle Instance { get; set; }
        public Arrow()
        {
            ImageBrush arrowSkin = new ImageBrush();
            arrowSkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/monster_arrow_pixel.png"));
            Instance = new Rectangle
            {
                Tag = "enemyArrow",
                Height = 40,
                Width = 15,
                Fill = arrowSkin,
            };
        }
    }
}
