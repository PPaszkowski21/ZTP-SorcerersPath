using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAnimatedGif;
using ZTP.GameSingleton;
using ZTP.Images;
using ZTP.Monsters;
using ZTP.PlayerClassess;
using ZTP.Spells;

namespace ZTP.Actions
{
    public class SpellFly
    {
        public static void FireballFlying(Rectangle x, Canvas myCanvas)
        {
            int speed = 20;
            switch (x.Tag)
            {
                case 0:
                    Canvas.SetTop(x, Canvas.GetTop(x) + speed);
                    break;
                case 1:
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - speed);
                    break;
                case 2:
                    Canvas.SetTop(x, Canvas.GetTop(x) - speed);
                    break;
                case 3:
                    Canvas.SetLeft(x, Canvas.GetLeft(x) + speed);
                    break;
            }
            if (Helper.IsOnTheBorder(x,myCanvas))
            {
                myCanvas.Children.Remove(x);
            }
        }
    }
}
