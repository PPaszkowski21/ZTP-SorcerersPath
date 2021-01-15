using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZTP.Actions;
using ZTP.Images;
using ZTP.Monsters;

namespace ZTP.PlayerClassess
{
    public class Player : ICustomObservable
    {
        internal Player(int _HitPoints, int _ExperiencePoints, int _Gold, int _Speed)
        {
            HitPoints = _HitPoints;
            ExperiencePoints = _ExperiencePoints;
            Gold = _Gold;
            Speed = _Speed;
            PlayerSkin = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(ImageManager.mageFront))
            };
            Instance = new Rectangle
            {
                Tag = "player",
                Height = 65,
                Width = 55,
                Fill = PlayerSkin
            };
            monsters = new List<ICustomObserver>();
        }
        public int HitPoints { get; set; }
        public int ExperiencePoints { get; private set; }
        public int Gold { get; set; }
        public Rectangle Instance { get; set; }

        public ImageBrush PlayerSkin;

        public int Direction;
        public int Speed { get; set; }

        public List<ICustomObserver> monsters;
        public void addObserver(ICustomObserver observer)
        {
            monsters.Add(observer);
        }

        public void deleteObserver(ICustomObserver observer)
        {
            monsters.Remove(observer);
        }

        public int countObservers()
        {
            return monsters.Count;
        }

        public void notifyObservers()
        {
            foreach (var monster in monsters)
            {
                monster.Update();
            }
        }
    }
}
