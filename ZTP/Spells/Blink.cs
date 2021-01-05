using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAnimatedGif;
using ZTP.Images;

namespace ZTP.Spells
{
    public class Blink
    {
        public Rectangle Instance { get; set; }

        public Blink(string blinkImage)
        {
            BitmapImage Source = new BitmapImage(new Uri(blinkImage));
            Image image = new Image();
            ImageBehavior.SetAnimatedSource(image, Source);

            Instance = new Rectangle
            {
                Name = "blink",
                Height = 140,
                Width = 140,
                Fill = new VisualBrush(image)
            };
        }
    }
}
