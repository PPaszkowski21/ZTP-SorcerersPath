using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAnimatedGif;
using ZTP.Images;
using ZTP.Monsters;
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

        private static void MovePlayer(Player player, int speed)
        {
            if (player.Direction == 1 && Canvas.GetLeft(player.Instance) > 0)
            {
                Canvas.SetLeft(player.Instance, Canvas.GetLeft(player.Instance) - speed);
            }
            if (player.Direction == 3 && Canvas.GetLeft(player.Instance) + 73 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(player.Instance, Canvas.GetLeft(player.Instance) + speed);
            }
            if (player.Direction == 2 && Canvas.GetTop(player.Instance) > 0)
            {
                Canvas.SetTop(player.Instance, Canvas.GetTop(player.Instance) - speed);
            }
            if (player.Direction == 0 && Canvas.GetTop(player.Instance) + 105 < Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(player.Instance, Canvas.GetTop(player.Instance) + speed);
            }
        }

        public static void PlayerDash(Player player, int speed, int dashRange, Canvas canvas, ref bool startGifTimer, List<Rectangle> blinkInstances)
        {
            Blink blink = new Blink(ImageManager.blink);
            Canvas.SetLeft(blink.Instance, Canvas.GetLeft(player.Instance) - player.Instance.Width/2);
            Canvas.SetTop(blink.Instance, Canvas.GetTop(player.Instance) - player.Instance.Height/2);
            canvas.Children.Add(blink.Instance);

            for (int i = 0; i < dashRange; i++)
            {
                MovePlayer(player, speed);
            }
            Blink blink2 = new Blink(ImageManager.blinkShow);
            Canvas.SetLeft(blink2.Instance, Canvas.GetLeft(player.Instance) - player.Instance.Width / 2);
            Canvas.SetTop(blink2.Instance, Canvas.GetTop(player.Instance) - player.Instance.Width / 2);
            canvas.Children.Add(blink2.Instance);


            blinkInstances.Add(blink.Instance);
            blinkInstances.Add(blink2.Instance);
            startGifTimer = true;
        }

        public static void EnemyMovement(Rectangle monster, Player player, int enemySpeed)
        {
            double distance;
            for (int i = 0; i < enemySpeed; i++)
            {
                distance = Canvas.GetLeft(player.Instance) - Canvas.GetLeft(monster);
                if (distance < 0)
                {
                    Canvas.SetLeft(monster, Canvas.GetLeft(monster) - 1);
                }
                else if(distance > 0)
                {
                    Canvas.SetLeft(monster, Canvas.GetLeft(monster) + 1);
                }
                distance = Canvas.GetTop(player.Instance) - Canvas.GetTop(monster);
                if (distance < 0)
                {
                    Canvas.SetTop(monster, Canvas.GetTop(monster) - 1);
                }
                else if(distance > 0)
                {
                    Canvas.SetTop(monster, Canvas.GetTop(monster) + 1);
                }
            }
        }

        public static void EnemyAvoidingOtherEnemy(Rectangle monster, Rect monsterHitBox, Rect otherMonsterHitBox, int enemySpeed)
        {
            Rect rectHelper = new Rect(otherMonsterHitBox.BottomLeft, otherMonsterHitBox.TopRight);
            otherMonsterHitBox.Intersect(monsterHitBox);

            //monster coming from top
            if(otherMonsterHitBox.TopLeft.Y == monsterHitBox.TopLeft.Y)
            {
                //moving left
                if(rectHelper.BottomLeft.X < otherMonsterHitBox.BottomLeft.X)
                {
                    Canvas.SetLeft(monster, Canvas.GetLeft(monster) - enemySpeed);
                }
                //moving right
                else
                {
                    Canvas.SetLeft(monster, Canvas.GetLeft(monster) + enemySpeed);
                }
            }
            //monster coming from bot
            else if (otherMonsterHitBox.BottomLeft.Y == monsterHitBox.BottomLeft.Y)
            {
                //moving left
                if (rectHelper.TopLeft.X < otherMonsterHitBox.TopLeft.X)
                {
                    Canvas.SetLeft(monster, Canvas.GetLeft(monster) - enemySpeed);
                }
                //moving right
                else
                {
                    Canvas.SetLeft(monster, Canvas.GetLeft(monster) + enemySpeed);
                }
            }
            //monster coming from left
            else if (otherMonsterHitBox.TopLeft.X == monsterHitBox.TopLeft.X)
            {
                //moving down
                if (rectHelper.TopLeft.Y > otherMonsterHitBox.TopLeft.Y)
                {
                    Canvas.SetTop(monster, Canvas.GetTop(monster) + enemySpeed);
                }
                //moving top
                else
                {
                    Canvas.SetTop(monster, Canvas.GetTop(monster) - enemySpeed);
                }
            }
            //monster coming from right
            else if (otherMonsterHitBox.TopRight.X == monsterHitBox.TopRight.X)
            {
                //moving down
                if (rectHelper.TopRight.Y > otherMonsterHitBox.TopRight.Y)
                {
                    Canvas.SetTop(monster, Canvas.GetTop(monster) + enemySpeed);
                }
                //moving top
                else
                {
                    Canvas.SetTop(monster, Canvas.GetTop(monster) - enemySpeed);
                }
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

        public static void FireballFlying(Rectangle x, List<Rectangle> itemsToRemove)
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
            if (Canvas.GetTop(x) < 10)
            {
                itemsToRemove.Add(x);
            }
        }
    }
}
