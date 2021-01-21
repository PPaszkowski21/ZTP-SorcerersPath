using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZTP.Images;

namespace ZTP.GameSingleton
{
    public static class Helper
    {
        public static Image GamePauseMessage{ get; set; }
        public static bool IsOnTheBorder(Rectangle x, Canvas myCanvas)
        {
            if (IsOnTheLeftBorder(x) || IsOnTheRightBorder(x, myCanvas) || IsOnTheTopBorder(x) || IsOnTheBottomBorder(x, myCanvas))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsOnTheLeftBorder(Rectangle x)
        {
            if (Canvas.GetLeft(x) < 10)
            {
                return true;
            }
            else return false;
        }
        public static bool IsOnTheRightBorder(Rectangle x, Canvas myCanvas)
        {
            if (Canvas.GetLeft(x) + x.ActualWidth >= myCanvas.ActualWidth - 10)
            {
                return true;
            }
            else return false;
        }
        public static bool IsOnTheTopBorder(Rectangle x)
        {
            if (Canvas.GetTop(x) < 10)
            {
                return true;
            }
            else return false;
        }
        public static bool IsOnTheBottomBorder(Rectangle x, Canvas myCanvas)
        {
            if (Canvas.GetTop(x) + x.ActualHeight >= myCanvas.ActualHeight - 10)
            {
                return true;
            }
            else return false;
        }
        public static Image ConstructPauseImage()
        {
            if(GamePauseMessage !=null)
            {
                return GamePauseMessage;
            }
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(ImageManager.Pause));
            image.Width = 1620;
            image.Height = 1080;
            image.Stretch = Stretch.Fill;
            Canvas.SetLeft(image, 0);
            Canvas.SetTop(image, 0);
            GamePauseMessage = image;
            return image;
        }
    }
}
