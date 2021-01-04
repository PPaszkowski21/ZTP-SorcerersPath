using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ZTP.PlayerClassess;

namespace ZTP.Images
{
    public static class ImageManager
    {
        public static string mageRight = "pack://application:,,,/images/white_mage_pixel_right.png";
        public static string mageLeft = "pack://application:,,,/images/white_mage_pixel_Left.png";
        public static string mageFront = "pack://application:,,,/images/white_mage_pixel_front.png";
        public static string mageBack = "pack://application:,,,/images/white_mage_pixel_back.png";

        public static string fireballRight = "pack://application:,,,/Images/fireball_pixel_right.png";
        public static string fireballLeft = "pack://application:,,,/Images/fireball_pixel_left.png";
        public static string fireballUp = "pack://application:,,,/Images/fireball_pixel_up.png";
        public static string fireballDown = "pack://application:,,,/Images/fireball_pixel_down.png";
        public static string fireballGif = "pack://application:,,,/Images/fireball2.gif";

        public static string dungeonBackground = "pack://application:,,,/Images/dungeonfloor.png";
        public static string background1 = "pack://application:,,,/Images/background1.jpg";
        public static string coin = "pack://application:,,,/Images/coin_1.png";
        public static string coinAnimated = "pack://application:,,,/Images/coinAnimated.gif";

        public static string avatar = "pack://application:,,,/Images/PlayerFrame.png";
        public static string hpBar0 = "pack://application:,,,/Images/hpBar0.jpg";
        public static string hpBar10 = "pack://application:,,,/Images/hpBar10.jpg";
        public static string hpBar20 = "pack://application:,,,/Images/hpBar20.jpg";
        public static string hpBar30 = "pack://application:,,,/Images/hpBar30.jpg";
        public static string hpBar40 = "pack://application:,,,/Images/hpBar40.jpg";
        public static string hpBar50 = "pack://application:,,,/Images/hpBar50.jpg";
        public static string hpBar60 = "pack://application:,,,/Images/hpBar60.jpg";
        public static string hpBar70 = "pack://application:,,,/Images/hpBar70.jpg";
        public static string hpBar80 = "pack://application:,,,/Images/hpBar80.jpg";
        public static string hpBar90 = "pack://application:,,,/Images/hpBar90.jpg";
        public static string hpBar100 = "pack://application:,,,/Images/hpBar100.jpg";

        public static bool ChangeHpBarImage(Player player, Image barHP)
        {
            if (player.HitPoints == 100)
            {
                barHP.Source = new BitmapImage(new Uri(ImageManager.hpBar100));
            }
            else if (player.HitPoints >= 90)
            {
                barHP.Source = new BitmapImage(new Uri(ImageManager.hpBar90));
            }
            else if (player.HitPoints >= 80)
            {
                barHP.Source = new BitmapImage(new Uri(ImageManager.hpBar80));
            }
            else if (player.HitPoints >= 70)
            {
                barHP.Source = new BitmapImage(new Uri(ImageManager.hpBar70));
            }
            else if (player.HitPoints >= 60)
            {
                barHP.Source = new BitmapImage(new Uri(ImageManager.hpBar60));
            }
            else if (player.HitPoints >= 50)
            {
                barHP.Source = new BitmapImage(new Uri(ImageManager.hpBar50));
            }
            else if (player.HitPoints >= 40)
            {
                barHP.Source = new BitmapImage(new Uri(ImageManager.hpBar40));
            }
            else if (player.HitPoints >= 30)
            {
                barHP.Source = new BitmapImage(new Uri(ImageManager.hpBar30));
            }
            else if (player.HitPoints >= 20)
            {
                barHP.Source = new BitmapImage(new Uri(ImageManager.hpBar20));
            }
            else if (player.HitPoints >= 10)
            {
                barHP.Source = new BitmapImage(new Uri(ImageManager.hpBar10));
            }
            else if (player.HitPoints <= 0)
            {
                barHP.Source = new BitmapImage(new Uri(ImageManager.hpBar0));
                return true;
            }
            return false;
        }
    }
}
