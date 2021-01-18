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
    public class FearRunningStrategy : IMovementStrategy
    {
        public void Move(Player player, IMonster monster, Canvas myCanvas)
        {
            double distance;
            for (int i = 0; i < monster.Speed; i++)
            {
                distance = Canvas.GetTop(player.Instance) - Canvas.GetTop(monster.Instance);
                //moving bot
                if (distance < 0 && !Helper.IsOnTheBottomBorder(monster.Instance,myCanvas))
                {
                    Canvas.SetTop(monster.Instance, Canvas.GetTop(monster.Instance) + 1);
                    monster.Direction = 0;
                }
                //moving top
                else if (distance > 0 && !Helper.IsOnTheTopBorder(monster.Instance))
                {
                    Canvas.SetTop(monster.Instance, Canvas.GetTop(monster.Instance) - 1);
                    monster.Direction = 2;
                }
                //moving left
                distance = Canvas.GetLeft(player.Instance) - Canvas.GetLeft(monster.Instance);
                if (distance > 0 && !Helper.IsOnTheLeftBorder(monster.Instance))
                {
                    Canvas.SetLeft(monster.Instance, Canvas.GetLeft(monster.Instance) - 1);
                    monster.Direction = 1;
                }
                //moving right
                else if (distance < 0 && !Helper.IsOnTheRightBorder(monster.Instance, myCanvas))
                {
                    Canvas.SetLeft(monster.Instance, Canvas.GetLeft(monster.Instance) + 1);
                    monster.Direction = 3;
                }
            }
            if (monster.Direction != monster.PreviousDirection)
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
            monster.PreviousDirection = monster.Direction;
        }
        public void CollisionAvoiding(IMonster monster, Rect monsterHitBox, Rect otherMonsterHitBox, Canvas myCanvas)
        {
            Random rd = new Random();
            int random = rd.Next(0, 4);
            switch (random)
            {
                case 0:
                    //moving bot
                    if (!Helper.IsOnTheBottomBorder(monster.Instance, myCanvas))
                    {
                        Canvas.SetTop(monster.Instance, Canvas.GetTop(monster.Instance) + 1);
                        monster.Direction = 0;
                    }
                    break;
                case 1:
                    //moving left
                    if (!Helper.IsOnTheLeftBorder(monster.Instance))
                    {
                        Canvas.SetLeft(monster.Instance, Canvas.GetLeft(monster.Instance) - 1);
                        monster.Direction = 1;
                    }
                    break;
                case 2:
                    //moving top
                    if (!Helper.IsOnTheTopBorder(monster.Instance))
                    {
                        Canvas.SetTop(monster.Instance, Canvas.GetTop(monster.Instance) - 1);
                        monster.Direction = 2;
                    }
                    break;
                case 3:
                    //moving right
                    if (!Helper.IsOnTheRightBorder(monster.Instance, myCanvas))
                    {
                        Canvas.SetLeft(monster.Instance, Canvas.GetLeft(monster.Instance) + 1);
                        monster.Direction = 3;
                    }
                    break;
            }
        }
    }
}
