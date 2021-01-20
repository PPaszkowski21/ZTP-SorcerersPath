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
    public class Lightning : IProjectile
    {
        public Rectangle Instance { get; set; }
        public int Direction { get; set; }
        public int Damage { get; set; }
        public int Speed { get; set; }
        public int Timer { get; set; }
        public List<VisualBrush> Images { get; set; }
        public Lightning(int direction)
        {
            Direction = direction;
            Damage = 500;
            Speed = 20;
            int height = 0;
            int width = 0;
            Timer = 100;
            Images = new List<VisualBrush>();
            Images.Add(new VisualBrush(ImageManager.CreateGif(ImageManager.LightningDown)));
            Images.Add(new VisualBrush(ImageManager.CreateGif(ImageManager.LightningLeft)));
            Images.Add(new VisualBrush(ImageManager.CreateGif(ImageManager.LightningUp)));
            Images.Add(new VisualBrush(ImageManager.CreateGif(ImageManager.LightningRight)));
            VisualBrush projectileSkin = new VisualBrush();
            switch (Direction)
            {
                case 0:
                    height = 300;
                    width = 120;
                    projectileSkin = Images[0];
                    break;
                case 1:
                    height = 120;
                    width = 300;
                    projectileSkin = Images[1];
                    break;
                case 2:
                    height = 300;
                    width = 120;
                    projectileSkin = Images[2];
                    break;
                case 3:
                    height = 120;
                    width = 300;
                    projectileSkin = Images[3];
                    break;
            }
            Instance = new Rectangle
            {
                Name = "projectile",
                Tag = "lightning",
                Height = height,
                Width = width,
                Fill = projectileSkin
            };
        }
        public void SpellBehaviour(Canvas myCanvas, List<IProjectile> projectiles)
        {
            Timer -= 7;
            if(Timer <0)
            {
                projectiles.Remove(this);
                myCanvas.Children.Remove(Instance);
            }
        }
    }
}

