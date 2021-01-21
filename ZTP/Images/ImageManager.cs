using System;
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

        public static string fireballRight = "pack://application:,,,/Images/Fireball/FireballRight.gif";
        public static string fireballLeft = "pack://application:,,,/Images/Fireball/FireballLeft.gif";
        public static string fireballUp = "pack://application:,,,/Images/Fireball/FireballUp.gif";
        public static string fireballDown = "pack://application:,,,/Images/Fireball/FireballDown.gif";
        public static string explosion = "pack://application:,,,/Images/Fireball/Explosion.gif";


        public static string ToxicBoltRight = "pack://application:,,,/Images/ToxicBolt/ToxicBoltRight.gif";
        public static string ToxicBoltLeft = "pack://application:,,,/Images/ToxicBolt/ToxicBoltLeft.gif";
        public static string ToxicBoltUp = "pack://application:,,,/Images/ToxicBolt/ToxicBoltUp.gif";
        public static string ToxicBoltDown = "pack://application:,,,/Images/ToxicBolt/ToxicBoltDown.gif";

        public static string LightningRight = "pack://application:,,,/Images/Lightning/LightningRight.gif";
        public static string LightningLeft = "pack://application:,,,/Images/Lightning/LightningLeft.gif";
        public static string LightningUp = "pack://application:,,,/Images/Lightning/LightningUp.gif";
        public static string LightningDown = "pack://application:,,,/Images/Lightning/LightningDown.gif";

        public static string blink = "pack://application:,,,/Images/Effects/Blink.gif";
        public static string blinkShow = "pack://application:,,,/Images/Effects/BlinkReverse.gif";
        public static string fear = "pack://application:,,,/Images/Effects/Fear.gif";

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
        public static string interface1 = "pack://application:,,,/Images/Backgrounds/interface.png";
        public static string PressEnterTogo = "pack://application:,,,/Images/Backgrounds/PressEnterTogo.png";
        public static string Congratulations = "pack://application:,,,/Images/Backgrounds/Congratulations.png";


        public static string FireballIcon = "pack://application:,,,/Images/Icons/FireballIcon.jpg";
        public static string ToxicBoltIcon = "pack://application:,,,/Images/Icons/ToxicBoltIcon.jpg";
        public static string LightningIcon = "pack://application:,,,/Images/Icons/LightningIcon.jpg";
        public static string FearIcon = "pack://application:,,,/Images/Icons/FearIcon.jpg";
        public static string BlinkIcon = "pack://application:,,,/Images/Icons/BlinkIcon.jpg";


        public static string CoinOrange = "pack://application:,,,/Images/Coins/CoinOrange.gif";
        public static string CoinRed = "pack://application:,,,/Images/Coins/CoinRed.gif";
        public static string CoinYellow = "pack://application:,,,/Images/Coins/CoinYellow.gif";

        public static string hpBar0 = "pack://application:,,,/Images/HpBars/hpBar0.jpg";
        public static string hpBar10 = "pack://application:,,,/Images/HpBars/hpBar10Red.jpg";
        public static string hpBar20 = "pack://application:,,,/Images/HpBars/hpBar20Red.jpg";
        public static string hpBar30 = "pack://application:,,,/Images/HpBars/hpBar30Orange.jpg";
        public static string hpBar40 = "pack://application:,,,/Images/HpBars/hpBar40Orange.jpg";
        public static string hpBar50 = "pack://application:,,,/Images/HpBars/hpBar50Orange.jpg";
        public static string hpBar60 = "pack://application:,,,/Images/HpBars/hpBar60.jpg";
        public static string hpBar70 = "pack://application:,,,/Images/HpBars/hpBar70.jpg";
        public static string hpBar80 = "pack://application:,,,/Images/HpBars/hpBar80.jpg";
        public static string hpBar90 = "pack://application:,,,/Images/HpBars/hpBar90.jpg";
        public static string hpBar100 = "pack://application:,,,/Images/HpBars/hpBar100.jpg";

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
                barHP.Source = new BitmapImage(new Uri(hpBar100));
            }
            else if (player.HitPoints * 100 / startHp >= 90)
            {
                barHP.Source = new BitmapImage(new Uri(hpBar90));
            }
            else if (player.HitPoints * 100 / startHp >= 80)
            {
                barHP.Source = new BitmapImage(new Uri(hpBar80));
            }
            else if (player.HitPoints * 100 / startHp >= 70)
            {
                barHP.Source = new BitmapImage(new Uri(hpBar70));
            }
            else if (player.HitPoints * 100 / startHp >= 60)
            {
                barHP.Source = new BitmapImage(new Uri(hpBar60));
            }
            else if (player.HitPoints * 100 / startHp >= 50)
            {
                barHP.Source = new BitmapImage(new Uri(hpBar50));
            }
            else if (player.HitPoints * 100 / startHp >= 40)
            {
                barHP.Source = new BitmapImage(new Uri(hpBar40));
            }
            else if (player.HitPoints * 100 / startHp >= 30)
            {
                barHP.Source = new BitmapImage(new Uri(hpBar30));
            }
            else if (player.HitPoints * 100 / startHp >= 20)
            {
                barHP.Source = new BitmapImage(new Uri(hpBar20));
            }
            else if (player.HitPoints * 100 / startHp >= 10)
            {
                barHP.Source = new BitmapImage(new Uri(hpBar10));
            }
            else if (player.HitPoints * 100 / startHp <= 0)
            {
                barHP.Source = new BitmapImage(new Uri(hpBar0));
                player.Instance.Width = 100;
                player.Instance.Height = 100;
                player.Instance.Fill = new VisualBrush(CreateGif(mageDead));
                return true;
            }
            return false;
        }
    }
}
