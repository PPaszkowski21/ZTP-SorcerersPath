using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;
using ZTP.PlayerClassess;

namespace ZTP.Images
{
    public static class ImageManager
    {
        public static string mageRight = "pack://application:,,,/images/playerbody/BodyPlayerRight.gif";
        public static string mageLeft = "pack://application:,,,/images/playerbody/BodyPlayerLeft.gif";
        public static string mageFront = "pack://application:,,,/images/playerbody/BodyPlayerDown.gif";
        public static string mageBack = "pack://application:,,,/images/playerbody/BodyPlayerUp.gif";
        public static string mageDead = "pack://application:,,,/images/playerbody/DeadBodyPlayer.png";

        public static string fireballRight = "pack://application:,,,/Images/fireball_pixel_right.png";
        public static string fireballLeft = "pack://application:,,,/Images/fireball_pixel_left.png";
        public static string fireballUp = "pack://application:,,,/Images/fireball_pixel_up.png";
        public static string fireballDown = "pack://application:,,,/Images/fireball_pixel_down.png";
        public static string fireballGif = "pack://application:,,,/Images/fireball2.gif";
        public static string blink = "pack://application:,,,/Images/Blink.gif";
        public static string blinkShow = "pack://application:,,,/Images/BlinkReverse.gif";

        public static string skeletonRight = "pack://application:,,,/images/skeleton/SkeletonRight.gif";
        public static string skeletonLeft = "pack://application:,,,/images/skeleton/SkeletonLeft.gif";
        public static string skeletonFront = "pack://application:,,,/images/skeleton/SkeletonUp.gif";
        public static string skeletonBack = "pack://application:,,,/images/skeleton/SkeletonDown.gif";

        public static string PhantomRight = "pack://application:,,,/images/Phantom/PhantomRight.gif";
        public static string PhantomLeft = "pack://application:,,,/images/Phantom/PhantomLeft.gif";
        public static string PhantomFront = "pack://application:,,,/images/Phantom/PhantomUp.gif";
        public static string PhantomBack = "pack://application:,,,/images/Phantom/PhantomDown.gif";

        public static string DemonRight = "pack://application:,,,/images/Demon/DemonRight.gif";
        public static string DemonLeft = "pack://application:,,,/images/Demon/DemonLeft.gif";
        public static string DemonFront = "pack://application:,,,/images/Demon/DemonUp.gif";
        public static string DemonBack = "pack://application:,,,/images/Demon/DemonDown.gif";

        public static string LichRight = "pack://application:,,,/images/Lich/LichRight.gif";
        public static string LichLeft = "pack://application:,,,/images/Lich/LichLeft.gif";
        public static string LichFront = "pack://application:,,,/images/Lich/LichUp.gif";
        public static string LichBack = "pack://application:,,,/images/Lich/LichDown.gif";

        public static string background1 = "pack://application:,,,/Images/Backgrounds/background1.jpg";
        public static string background2 = "pack://application:,,,/Images/Backgrounds/background2.jpg";
        public static string background3 = "pack://application:,,,/Images/Backgrounds/background3.jpg";
        public static string background4 = "pack://application:,,,/Images/Backgrounds/background4.gif";
        public static string coinAnimated = "pack://application:,,,/Images/coinAnimated.gif";

        public static string hpBar0 = "pack://application:,,,/Images/hpBar0.jpg";
        public static string hpBar10 = "pack://application:,,,/Images/hpBar10Red.jpg";
        public static string hpBar20 = "pack://application:,,,/Images/hpBar20Red.jpg";
        public static string hpBar30 = "pack://application:,,,/Images/hpBar30Orange.jpg";
        public static string hpBar40 = "pack://application:,,,/Images/hpBar40Orange.jpg";
        public static string hpBar50 = "pack://application:,,,/Images/hpBar50Orange.jpg";
        public static string hpBar60 = "pack://application:,,,/Images/hpBar60.jpg";
        public static string hpBar70 = "pack://application:,,,/Images/hpBar70.jpg";
        public static string hpBar80 = "pack://application:,,,/Images/hpBar80.jpg";
        public static string hpBar90 = "pack://application:,,,/Images/hpBar90.jpg";
        public static string hpBar100 = "pack://application:,,,/Images/hpBar100.jpg";

        public static Image CreateGif(string img)
        {
            BitmapImage Source = new BitmapImage(new Uri(img));
            Image image = new Image();
            ImageBehavior.SetAnimatedSource(image, Source);
            return image;
        }

        public static bool ChangeHpBarImage(Player player, int startHp, Image barHP)
        {
            
            if (player.HitPoints*100/startHp == 100)
            {
                barHP.Source = new BitmapImage(new Uri(ImageManager.hpBar100));
            }
            else if (player.HitPoints * 100 / startHp >= 90)
            {
                barHP.Source = new BitmapImage(new Uri(ImageManager.hpBar90));
            }
            else if (player.HitPoints * 100 / startHp >= 80)
            {
                barHP.Source = new BitmapImage(new Uri(ImageManager.hpBar80));
            }
            else if (player.HitPoints * 100 / startHp >= 70)
            {
                barHP.Source = new BitmapImage(new Uri(ImageManager.hpBar70));
            }
            else if (player.HitPoints * 100 / startHp >= 60)
            {
                barHP.Source = new BitmapImage(new Uri(ImageManager.hpBar60));
            }
            else if (player.HitPoints * 100 / startHp >= 50)
            {
                barHP.Source = new BitmapImage(new Uri(ImageManager.hpBar50));
            }
            else if (player.HitPoints * 100 / startHp >= 40)
            {
                barHP.Source = new BitmapImage(new Uri(ImageManager.hpBar40));
            }
            else if (player.HitPoints * 100 / startHp >= 30)
            {
                barHP.Source = new BitmapImage(new Uri(ImageManager.hpBar30));
            }
            else if (player.HitPoints * 100 / startHp >= 20)
            {
                barHP.Source = new BitmapImage(new Uri(ImageManager.hpBar20));
            }
            else if (player.HitPoints * 100 / startHp >= 10)
            {
                barHP.Source = new BitmapImage(new Uri(ImageManager.hpBar10));
            }
            else if (player.HitPoints * 100 / startHp <= 0)
            {
                barHP.Source = new BitmapImage(new Uri(ImageManager.hpBar0));
                player.Instance.Width = 100;
                player.Instance.Height = 100;
                player.Instance.Fill = new VisualBrush(CreateGif(mageDead));
                return true;
            }
            return false;
        }
    }
}
