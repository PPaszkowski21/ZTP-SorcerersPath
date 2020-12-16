using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ZTP.Spells
{
    public class Fireball: Projectile
    {
        internal Fireball()
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
