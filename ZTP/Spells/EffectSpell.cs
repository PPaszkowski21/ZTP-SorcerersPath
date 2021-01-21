using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAnimatedGif;
using ZTP.Images;

namespace ZTP.Spells
{
    public class EffectSpell
    {
        //instance of blink gif and fear gif
        public Rectangle Instance { get; set; }
        public EffectSpell(string image, int height, int width)
        {
            Instance = new Rectangle
            {
                Name = "effectspell",
                Height = height,
                Width = height,
                Fill = new VisualBrush(ImageManager.CreateGif(image))
            };
        }

    }
}
