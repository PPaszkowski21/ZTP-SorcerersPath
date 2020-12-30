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
using ZTP.Monsters;

namespace ZTP.Actions
{
    public static class MonsterDrop
    {
        public static void DropCoin(Canvas myCanvas, double x, double y)
        {
            BitmapImage Source = new BitmapImage(new Uri(ImageManager.coinAnimated));
            Image image = new Image();
            ImageBehavior.SetAnimatedSource(image, Source);
            Rectangle coin = new Rectangle
            {
                Name = "coin",
                Height = 30,
                Width = 30,
                Fill = new VisualBrush(image)
            };
            Canvas.SetLeft(coin, x);
            Canvas.SetTop(coin, y);
            myCanvas.Children.Add(coin);

        }
    }
}
