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
                //player.Instance.Fill = new VisualBrush(ImageManager.CreateGif(ImageManager.demon));
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

        public static void EnemyMovement(Monster monster, Player player)
        {
            double distance;
            for (int i = 0; i < monster.Speed; i++)
            {
                distance = Canvas.GetTop(player.Instance) - Canvas.GetTop(monster.Instance);
                if (distance < 0)
                {
                    Canvas.SetTop(monster.Instance, Canvas.GetTop(monster.Instance) - 1);
                    monster.Direction = 2;
                }
                else if(distance > 0)
                {
                    Canvas.SetTop(monster.Instance, Canvas.GetTop(monster.Instance) + 1);
                    monster.Direction = 0;
                }
                distance = Canvas.GetLeft(player.Instance) - Canvas.GetLeft(monster.Instance);
                if (distance < 0)
                {
                    Canvas.SetLeft(monster.Instance, Canvas.GetLeft(monster.Instance) - 1);
                    monster.Direction = 1;
                }
                else if(distance > 0)
                {
                    Canvas.SetLeft(monster.Instance, Canvas.GetLeft(monster.Instance) + 1);
                    monster.Direction = 3;
                }
                switch (monster.Direction)
                {
                    case 0:
                        monster.Instance.Fill = new VisualBrush(ImageManager.CreateGif(monster.Images[0]));
                        break;
                    case 1:
                        monster.Instance.Fill = new VisualBrush(ImageManager.CreateGif(monster.Images[1]));
                        break;
                    case 2:
                        monster.Instance.Fill = new VisualBrush(ImageManager.CreateGif(monster.Images[2]));
                        break;
                    case 3:
                        monster.Instance.Fill = new VisualBrush(ImageManager.CreateGif(monster.Images[3]));
                        break;
                }
            }
        }

        public static void EnemyAvoidingOtherEnemy(Monster monster, Rect monsterHitBox, Rect otherMonsterHitBox)
        {
            Rect rectHelper = new Rect(otherMonsterHitBox.BottomLeft, otherMonsterHitBox.TopRight);
            otherMonsterHitBox.Intersect(monsterHitBox);

            //monster coming from top
            if(otherMonsterHitBox.TopLeft.Y == monsterHitBox.TopLeft.Y)
            {
                //moving left
                if(rectHelper.BottomLeft.X < otherMonsterHitBox.BottomLeft.X)
                {
                    Canvas.SetLeft(monster.Instance, Canvas.GetLeft(monster.Instance) - monster.Speed);
                }
                //moving right
                else
                {
                    Canvas.SetLeft(monster.Instance, Canvas.GetLeft(monster.Instance) + monster.Speed);
                }
            }
            //monster coming from bot
            else if (otherMonsterHitBox.BottomLeft.Y == monsterHitBox.BottomLeft.Y)
            {
                //moving left
                if (rectHelper.TopLeft.X < otherMonsterHitBox.TopLeft.X)
                {
                    Canvas.SetLeft(monster.Instance, Canvas.GetLeft(monster.Instance) - monster.Speed);
                }
                //moving right
                else
                {
                    Canvas.SetLeft(monster.Instance, Canvas.GetLeft(monster.Instance) + monster.Speed);
                }
            }
            //monster coming from left
            else if (otherMonsterHitBox.TopLeft.X == monsterHitBox.TopLeft.X)
            {
                //moving down
                if (rectHelper.TopLeft.Y > otherMonsterHitBox.TopLeft.Y)
                {
                    Canvas.SetTop(monster.Instance, Canvas.GetTop(monster.Instance) + monster.Speed);
                }
                //moving top
                else
                {
                    Canvas.SetTop(monster.Instance, Canvas.GetTop(monster.Instance) - monster.Speed);
                }
            }
            //monster coming from right
            else if (otherMonsterHitBox.TopRight.X == monsterHitBox.TopRight.X)
            {
                //moving down
                if (rectHelper.TopRight.Y > otherMonsterHitBox.TopRight.Y)
                {
                    Canvas.SetTop(monster.Instance, Canvas.GetTop(monster.Instance) + monster.Speed);
                }
                //moving top
                else
                {
                    Canvas.SetTop(monster.Instance, Canvas.GetTop(monster.Instance) - monster.Speed);
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
