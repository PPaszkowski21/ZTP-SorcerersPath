using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ZTP.Spells
{
    public class Fireball
    {
        public Rectangle Instance { get; set; }
        public Fireball()
        {
            ImageBrush spellSkin = new ImageBrush();
            spellSkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/fireball_pixel.png"));
            Instance= new Rectangle
            {
                Tag = "spell",
                Height = 47,
                Width = 28,
                Fill = spellSkin
            };
        }
    }

}
