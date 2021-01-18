using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace ZTP.GameSingleton
{
    public static class Helper
    {
        public static bool IsOnTheBorder(Rectangle x, Canvas myCanvas)
        {
            if (IsOnTheLeftBorder(x,myCanvas) || IsOnTheRightBorder(x, myCanvas) || IsOnTheTopBorder(x, myCanvas) || IsOnTheBottomBorder(x, myCanvas))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsOnTheLeftBorder(Rectangle x, Canvas myCanvas)
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
        public static bool IsOnTheTopBorder(Rectangle x, Canvas myCanvas)
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
    }
}
