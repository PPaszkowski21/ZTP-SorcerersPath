using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZTP.Images;

namespace ZTP.PlayerClassess
{
    public class Player
    {
        public int HitPoints { get; set; }
        public int ExperiencePoints { get; private set; }
        public int Gold { get; set; }
        public Rectangle Instance { get; set; }

        public ImageBrush PlayerSkin;

        public int Direction;

        internal Player(int _HitPoints, int _ExperiencePoints, int _Gold)
        {
            HitPoints = _HitPoints;
            ExperiencePoints = _ExperiencePoints;
            Gold = _Gold;
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
        }            
    }
}
