using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZTP.Images;
using ZTP.PlayerClassess;
using ZTP.Spells;

namespace ZTP.Actions
{
    public static class Movement
    {
        public static void MovePlayer(bool goLeft, bool goRight, bool goUp, bool goDown, Player player, int speed)
        {
            if (goLeft == true && Canvas.GetLeft(player.Instance) > 0)
            {
                player.PlayerSkin.ImageSource = new BitmapImage(new Uri(ImageManager.mageLeft));
                player.Direction = 1;
                Canvas.SetLeft(player.Instance, Canvas.GetLeft(player.Instance) - speed);
                
            }
            if (goRight == true && Canvas.GetLeft(player.Instance) + 73 < Application.Current.MainWindow.Width)
            {
                player.PlayerSkin.ImageSource = new BitmapImage(new Uri(ImageManager.mageRight));
                player.Direction = 3;
                Canvas.SetLeft(player.Instance, Canvas.GetLeft(player.Instance) + speed);
            }
            if (goUp == true && Canvas.GetTop(player.Instance) > 0)
            {
                player.PlayerSkin.ImageSource = new BitmapImage(new Uri(ImageManager.mageBack));
                player.Direction = 2;
                Canvas.SetTop(player.Instance, Canvas.GetTop(player.Instance) - speed);
            }
            if (goDown == true && Canvas.GetTop(player.Instance) + 105 < Application.Current.MainWindow.Height)
            {
                player.PlayerSkin.ImageSource = new BitmapImage(new Uri(ImageManager.mageFront));
                player.Direction = 0;
                Canvas.SetTop(player.Instance, Canvas.GetTop(player.Instance) + speed);
            }
        }

        public static void FireballThrow(Canvas canvas, Player player)
        {
            Fireball fireball = new Fireball(player);

            if (player.Direction == 0)
            {
                Canvas.SetTop(fireball.Instance, Canvas.GetTop(player.Instance) + player.Instance.ActualHeight);
                Canvas.SetLeft(fireball.Instance, Canvas.GetLeft(player.Instance) + player.Instance.Width / 2);
            }
            else if (player.Direction == 1)
            {
                Canvas.SetTop(fireball.Instance, Canvas.GetTop(player.Instance));
                Canvas.SetLeft(fireball.Instance, Canvas.GetLeft(player.Instance) - 10);
            }
            else if (player.Direction == 2)
            {
                Canvas.SetTop(fireball.Instance, Canvas.GetTop(player.Instance) - fireball.Instance.ActualHeight);
                Canvas.SetLeft(fireball.Instance, Canvas.GetLeft(player.Instance) + player.Instance.Width / 2);
            }
            else if (player.Direction == 3)
            {
                Canvas.SetTop(fireball.Instance, Canvas.GetTop(player.Instance));
                Canvas.SetLeft(fireball.Instance, Canvas.GetLeft(player.Instance) + player.Instance.Width - 5);
            }
            canvas.Children.Add(fireball.Instance);
        }

        public static void FireballFlying(Rectangle x)
        {
            switch (x.Tag)
            {
                case 0:
                    Canvas.SetTop(x, Canvas.GetTop(x) + 20);
                    break;
                case 1:
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - 20);
                    break;
                case 2:
                    Canvas.SetTop(x, Canvas.GetTop(x) - 20);
                    break;
                case 3:
                    Canvas.SetLeft(x, Canvas.GetLeft(x) + 20);
                    break;
            }
        }
    }
}
