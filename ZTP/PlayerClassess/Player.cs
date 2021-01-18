using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZTP.Actions;
using ZTP.GameSingleton;
using ZTP.Images;
using ZTP.Monsters;
using ZTP.Spells;

namespace ZTP.PlayerClassess
{
    public class Player : ICustomObservable
    {
        public Player(int _HitPoints, int _Speed)
        {
            HitPoints = _HitPoints;
            Speed = _Speed;
            Instance = new Rectangle
            {
                Tag = "player",
                Height = 70,
                Width = 70,
                Fill = new VisualBrush(ImageManager.CreateGif(ImageManager.mageBack))
            };
            Direction = 0;
            Monsters = new List<ICustomObserver>();
            Images = new List<string>();
            Images.Add(ImageManager.mageFront);
            Images.Add(ImageManager.mageLeft);
            Images.Add(ImageManager.mageBack);
            Images.Add(ImageManager.mageRight);
        }
        public int HitPoints { get; set; }
        public int ExperiencePoints { get; private set; }
        public int Gold { get; set; }
        public Rectangle Instance { get; set; }

        public int Direction { get; set; }
        public int PreviousDirection { get; set; }
        public int Speed { get; set; }
        public List<string> Images { get; }

        public void MovePlayer(bool goLeft, bool goRight, bool goUp, bool goDown, Canvas canvas)
        {
            if (goLeft == true && !Helper.IsOnTheLeftBorder(Instance))
            {
                Direction = 1;
                Canvas.SetLeft(Instance, Canvas.GetLeft(Instance) - Speed);

            }
            if (goRight == true && !Helper.IsOnTheRightBorder(Instance,canvas))
            {
                Direction = 3;
                Canvas.SetLeft(Instance, Canvas.GetLeft(Instance) + Speed);
            }
            if (goUp == true && !Helper.IsOnTheTopBorder(Instance))
            {
                Direction = 2;
                Canvas.SetTop(Instance, Canvas.GetTop(Instance) - Speed);
            }
            if (goDown == true && !Helper.IsOnTheBottomBorder(Instance, canvas))
            {
                Direction = 0;
                Canvas.SetTop(Instance, Canvas.GetTop(Instance) + Speed);
            }
            if (Direction != PreviousDirection)
            {
                switch (Direction)
                {
                    case 0:
                        Instance.Fill = new VisualBrush(ImageManager.CreateGif(Images[0]));
                        break;
                    case 1:
                        Instance.Fill = new VisualBrush(ImageManager.CreateGif(Images[1]));
                        break;
                    case 2:
                        Instance.Fill = new VisualBrush(ImageManager.CreateGif(Images[2]));
                        break;
                    case 3:
                        Instance.Fill = new VisualBrush(ImageManager.CreateGif(Images[3]));
                        break;
                }
            }
            PreviousDirection = Direction;
        }
        private void MovePlayerForDash(int speed, Canvas canvas)
        {
            if (Direction == 1 && Canvas.GetLeft(Instance) > 0)
            {
                Canvas.SetLeft(Instance, Canvas.GetLeft(Instance) - speed);
            }
            if (Direction == 3 && Canvas.GetLeft(Instance) + Instance.Width < canvas.ActualWidth)
            {
                Canvas.SetLeft(Instance, Canvas.GetLeft(Instance) + speed);
            }
            if (Direction == 2 && Canvas.GetTop(Instance) > 0)
            {
                Canvas.SetTop(Instance, Canvas.GetTop(Instance) - speed);
            }
            if (Direction == 0 && Canvas.GetTop(Instance) + Instance.Height < canvas.ActualHeight)
            {
                Canvas.SetTop(Instance, Canvas.GetTop(Instance) + speed);
            }
        }
        public void PlayerDash(Player player, int dashRange, Canvas canvas, ref bool startGifTimer, List<Rectangle> blinkInstances)
        {
            Blink blink = new Blink(ImageManager.blink);
            Canvas.SetLeft(blink.Instance, Canvas.GetLeft(player.Instance) - player.Instance.Width / 2);
            Canvas.SetTop(blink.Instance, Canvas.GetTop(player.Instance) - player.Instance.Height / 2);
            canvas.Children.Add(blink.Instance);

            for (int i = 0; i < dashRange; i++)
            {
                MovePlayerForDash(player.Speed, canvas);
            }
            Blink blink2 = new Blink(ImageManager.blinkShow);
            Canvas.SetLeft(blink2.Instance, Canvas.GetLeft(player.Instance) - player.Instance.Width / 2);
            Canvas.SetTop(blink2.Instance, Canvas.GetTop(player.Instance) - player.Instance.Width / 2);
            canvas.Children.Add(blink2.Instance);


            blinkInstances.Add(blink.Instance);
            blinkInstances.Add(blink2.Instance);
            startGifTimer = true;
        }
        public void FireballThrow(Canvas canvas)
        {
            Fireball fireball = new Fireball(Direction);

            if (Direction == 0)
            {
                Canvas.SetTop(fireball.Instance, Canvas.GetTop(Instance) +Instance.ActualHeight);
                Canvas.SetLeft(fireball.Instance, Canvas.GetLeft(Instance) + Instance.Width / 2);
            }
            else if (Direction == 1)
            {
                Canvas.SetTop(fireball.Instance, Canvas.GetTop(Instance));
                Canvas.SetLeft(fireball.Instance, Canvas.GetLeft(Instance) - 10);
            }
            else if (Direction == 2)
            {
                Canvas.SetTop(fireball.Instance, Canvas.GetTop(Instance) - fireball.Instance.ActualHeight);
                Canvas.SetLeft(fireball.Instance, Canvas.GetLeft(Instance) + Instance.Width / 2);
            }
            else if (Direction == 3)
            {
                Canvas.SetTop(fireball.Instance, Canvas.GetTop(Instance));
                Canvas.SetLeft(fireball.Instance, Canvas.GetLeft(Instance) + Instance.Width - 5);
            }
            canvas.Children.Add(fireball.Instance);
        }

        public List<ICustomObserver> Monsters;
        public void addObserver(ICustomObserver observer)
        {
            Monsters.Add(observer);
        }

        public void deleteObserver(ICustomObserver observer)
        {
            Monsters.Remove(observer);
        }

        public int countObservers()
        {
            return Monsters.Count;
        }

        public void notifyObservers(IMovementStrategy strategy)
        {
            foreach (var monster in Monsters)
            {
                monster.UpdateMovement(strategy);
            }
        }
    }
}
