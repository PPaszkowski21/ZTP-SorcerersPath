using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAnimatedGif;
using ZTP.Images;
using ZTP.PlayerClassess;

namespace ZTP.Spells
{
    public class Fireball: IProjectile
    {
        public Rectangle Instance { get; set; }
        public int Direction { get; set; }
        public Fireball(int direction)
        {
            Direction = direction;
            int height = 0;
            int width = 0;
            string fireballSkin = "";
            switch(Direction)
            {
                case 0:
                    height = 47;
                    width = 28;
                    fireballSkin = ImageManager.fireballDown;
                    break;
                case 1:
                    height = 28;
                    width = 47;
                    fireballSkin = ImageManager.fireballLeft;
                    break;
                case 2:
                    height = 47;
                    width = 28;
                    fireballSkin = ImageManager.fireballUp;
                    break;
                case 3:
                    height = 28;
                    width = 47;
                    fireballSkin = ImageManager.fireballRight;
                    break;
            }

            ImageBrush spellSkin = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(fireballSkin))
            };

            Instance = new Rectangle
            {
                Name = "fireball",
                Tag = Direction,
                Height = height,
                Width = width,
                Fill = spellSkin
            };
        }
    }
}
