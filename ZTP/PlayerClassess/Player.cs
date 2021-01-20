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
            GameSave = new GameSave();
            Monsters = new List<ICustomObserver>();
            Images = new List<VisualBrush>();
            Blinks = new List<EffectSpell>();
            Images.Add(new VisualBrush(ImageManager.CreateGif(ImageManager.mageFront)));
            Images.Add(new VisualBrush(ImageManager.CreateGif(ImageManager.mageLeft)));
            Images.Add(new VisualBrush(ImageManager.CreateGif(ImageManager.mageBack)));
            Images.Add(new VisualBrush(ImageManager.CreateGif(ImageManager.mageRight)));
            Instance = new Rectangle
            {
                Tag = "player",
                Height = 70,
                Width = 70,
                Fill = Images[0]
            };
            Direction = 0;
            Projectile = "fireball";
        }
        public int HitPoints { get; set; }
        public int ExperiencePoints { get; private set; }
        public GameSave GameSave { get; set; }
        public Rectangle Instance { get; set; }
        public int Direction { get; set; }
        public int PreviousDirection { get; set; }
        public int Speed { get; set; }
        public string Projectile { get; set; }
        public int ProjectileCooldown { get; set; }

        public List<EffectSpell> Blinks { get; set; }
        public int BlinkCooldown { get; set; }
        public EffectSpell Fear { get; set; }
        public int FearCooldown { get; set; }
        public List<VisualBrush> Images { get; }
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
                        Instance.Fill = Images[0];
                        break;
                    case 1:
                        Instance.Fill =Images[1];
                        break;
                    case 2:
                        Instance.Fill = Images[2];
                        break;
                    case 3:
                        Instance.Fill =Images[3];
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
        public void PlayerDash(int dashRange, Canvas canvas)
        {
            EffectSpell blink = new EffectSpell(ImageManager.blink, 140, 140);
            Canvas.SetLeft(blink.Instance, Canvas.GetLeft(Instance) - Instance.Width / 2);
            Canvas.SetTop(blink.Instance, Canvas.GetTop(Instance) - Instance.Height / 2);
            canvas.Children.Add(blink.Instance);

            for (int i = 0; i < dashRange; i++)
            {
                MovePlayerForDash(Speed, canvas);
            }
            EffectSpell blink2 = new EffectSpell(ImageManager.blinkShow, 140, 140);
            Canvas.SetLeft(blink2.Instance, Canvas.GetLeft(Instance) - Instance.Width / 2);
            Canvas.SetTop(blink2.Instance, Canvas.GetTop(Instance) - Instance.Width / 2);
            canvas.Children.Add(blink2.Instance);

            Blinks.Add(blink);
            Blinks.Add(blink2);
        }
        public void FearEnemies(Canvas canvas)
        {
            EffectSpell fear = new EffectSpell(ImageManager.fear, 300, 300);
            Canvas.SetLeft(fear.Instance, Canvas.GetLeft(Instance)-115);
            Canvas.SetTop(fear.Instance, Canvas.GetTop(Instance)-115);
            canvas.Children.Add(fear.Instance);
            notifyObservers(new FearRunningStrategy());
        }
        public void ProjectileThrow(Canvas canvas, List<IProjectile> projectiles, string tag)
        {
            IProjectile projectile;
            if(tag == "fireball")
            {
               projectile = new Fireball(Direction);
            }
            else if (tag == "toxicbolt")
            {
                projectile = new ToxicBolt(Direction);
            }
            else
            {
                projectile = new Lightning(Direction);
            }

            if (Direction == 0)
            {
                Canvas.SetTop(projectile.Instance, Canvas.GetTop(Instance) + Instance.ActualHeight);
                Canvas.SetLeft(projectile.Instance, Canvas.GetLeft(Instance) + Instance.ActualWidth/2 - projectile.Instance.Width/2);
            }
            else if (Direction == 1)
            {
                Canvas.SetTop(projectile.Instance, Canvas.GetTop(Instance) + Instance.ActualHeight/2 - projectile.Instance.Height/2);
                Canvas.SetLeft(projectile.Instance, Canvas.GetLeft(Instance) - projectile.Instance.Width);
            }
            else if (Direction == 2)
            {
                Canvas.SetTop(projectile.Instance, Canvas.GetTop(Instance) - projectile.Instance.Height);
                Canvas.SetLeft(projectile.Instance, Canvas.GetLeft(Instance) + Instance.ActualWidth / 2 - projectile.Instance.Width/2);
            }
            else if (Direction == 3)
            {
                Canvas.SetTop(projectile.Instance, Canvas.GetTop(Instance) + Instance.ActualHeight/2 - projectile.Instance.Height/2);
                Canvas.SetLeft(projectile.Instance, Canvas.GetLeft(Instance) + Instance.ActualWidth);
            }
            projectiles.Add(projectile);
            canvas.Children.Add(projectile.Instance);
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
