using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ZTP.GameSingleton;
using ZTP.Images;
using ZTP.Monsters;
using ZTP.PlayerClassess;

namespace ZTP.Actions
{
    public class RegularMovementStrategy : IMovementStrategy
    {
        public void Move(Player player, IMonster monster, Canvas myCanvas)
        {
            double distance;
            for (int i = 0; i < monster.Speed; i++)
            {
                distance = Canvas.GetTop(player.Instance) - Canvas.GetTop(monster.Instance);
                if (distance < 0 && !Helper.IsOnTheTopBorder(monster.Instance,myCanvas))
                {
                    Canvas.SetTop(monster.Instance, Canvas.GetTop(monster.Instance) - 1);
                    monster.Direction = 2;
                }
                else if (distance > 0 && !Helper.IsOnTheBottomBorder(monster.Instance, myCanvas))
                {
                    Canvas.SetTop(monster.Instance, Canvas.GetTop(monster.Instance) + 1);
                    monster.Direction = 0;
                }
                distance = Canvas.GetLeft(player.Instance) - Canvas.GetLeft(monster.Instance);
                if (distance < 0 && !Helper.IsOnTheLeftBorder(monster.Instance, myCanvas))
                {
                    Canvas.SetLeft(monster.Instance, Canvas.GetLeft(monster.Instance) - 1);
                    monster.Direction = 1;
                }
                else if (distance > 0 && !Helper.IsOnTheRightBorder(monster.Instance, myCanvas))
                {
                    Canvas.SetLeft(monster.Instance, Canvas.GetLeft(monster.Instance) + 1);
                    monster.Direction = 3;
                }
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
        public void CollisionAvoiding(IMonster monster, Rect monsterHitBox, Rect otherMonsterHitBox, Canvas myCanvas)
        {
            if (Helper.IsOnTheBorder(monster.Instance, myCanvas))
                return;
            Rect rectHelper = new Rect(otherMonsterHitBox.BottomLeft, otherMonsterHitBox.TopRight);
            otherMonsterHitBox.Intersect(monsterHitBox);

            //monster coming from top
            if (otherMonsterHitBox.TopLeft.Y == monsterHitBox.TopLeft.Y)
            {
                //moving left
                if (rectHelper.BottomLeft.X < otherMonsterHitBox.BottomLeft.X)
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
    }
}
