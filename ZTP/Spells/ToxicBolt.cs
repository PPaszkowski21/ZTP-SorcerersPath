using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ZTP.GameSingleton;
using ZTP.Images;

namespace ZTP.Spells
{
    public class ToxicBolt : IProjectile
    {
        public Rectangle Instance { get; set; }
        public int Direction { get; set; }
        public int Damage { get; set; }
        public int Speed { get; set; }
        public List<VisualBrush> Images { get; set; }
        public ToxicBolt(int direction)
        {
            Direction = direction;
            Damage = 10;
            Speed = 20;
            int height = 75;
            int width = 75;
            Images = new List<VisualBrush>();
            Images.Add(new VisualBrush(ImageManager.CreateGif(ImageManager.ToxicBoltDown)));
            Images.Add(new VisualBrush(ImageManager.CreateGif(ImageManager.ToxicBoltLeft)));
            Images.Add(new VisualBrush(ImageManager.CreateGif(ImageManager.ToxicBoltUp)));
            Images.Add(new VisualBrush(ImageManager.CreateGif(ImageManager.ToxicBoltRight)));
            VisualBrush projectileSkin = new VisualBrush();
            switch (Direction)
            {
                case 0:
                    projectileSkin = Images[0];
                    break;
                case 1:
                    projectileSkin = Images[1];
                    break;
                case 2:
                    projectileSkin = Images[2];
                    break;
                case 3:
                    projectileSkin = Images[3];
                    break;
            }
            Instance = new Rectangle
            {
                Name = "projectile",
                Tag = "toxicbolt",
                Height = height,
                Width = width,
                Fill = projectileSkin
            };
        }
        public void SpellBehaviour(Canvas myCanvas, List<IProjectile> projectiles)
        {
            switch (Direction)
            {
                case 0:
                    Canvas.SetTop(Instance, Canvas.GetTop(Instance) + Speed);
                    break;
                case 1:
                    Canvas.SetLeft(Instance, Canvas.GetLeft(Instance) - Speed);
                    break;
                case 2:
                    Canvas.SetTop(Instance, Canvas.GetTop(Instance) - Speed);
                    break;
                case 3:
                    Canvas.SetLeft(Instance, Canvas.GetLeft(Instance) + Speed);
                    break;
            }
            if (Helper.IsOnTheBorder(Instance, myCanvas))
            {
                projectiles.Remove(this);
                myCanvas.Children.Remove(Instance);
            }
        }
    }
}
