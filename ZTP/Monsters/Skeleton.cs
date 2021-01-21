using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAnimatedGif;
using ZTP.Actions;
using ZTP.GameSingleton;
using ZTP.Images;
using ZTP.PlayerClassess;

namespace ZTP.Monsters
{
    public class Skeleton : IMonster, IMonsterObserver
    {
        public Skeleton()
        {
            this.Speed = 2;
            this.Damage = 1;
            this.HitPoints = 100;
            this.Images = new List<VisualBrush>();
            Images.Add(new VisualBrush(ImageManager.CreateGif(ImageManager.skeletonBack)));
            Images.Add(new VisualBrush(ImageManager.CreateGif(ImageManager.skeletonLeft)));
            Images.Add(new VisualBrush(ImageManager.CreateGif(ImageManager.skeletonFront)));
            Images.Add(new VisualBrush(ImageManager.CreateGif(ImageManager.skeletonRight)));
            this.Instance = new Rectangle
            {
                Name = "enemy",
                Tag = "skeleton",
                Height = 70,
                Width = 70,
                Fill = Images[0]
            };
            Direction = 0;
            MovementStrategy = new RegularMovementStrategy();
        }
        public int HitPoints { get; set; }
        public Rectangle Instance { get; set; }
        public int Speed { get; set; }
        public int Damage { get; set; }
        public List<VisualBrush> Images { get; set; }
        public int Direction { get; set; }
        public int PreviousDirection { get; set; }
        public IMovementStrategy MovementStrategy { get; set; }
        public int GifTimer { get; set; }

        public void setMovementStrategy(IMovementStrategy movementStrategy)
        {
            this.MovementStrategy = movementStrategy;
        }
        public void Move(Player player, IMonster thisMonster, Canvas myCanvas)
        {
            MovementStrategy.Move(player, thisMonster, myCanvas);
        }
        public void CollisionAvoiding(IMonster monster, Rect monsterHitBox, Rect otherMonsterHitBox, Canvas myCanvas)
        {
            MovementStrategy.CollisionAvoiding(monster,monsterHitBox,otherMonsterHitBox,myCanvas);
        }
        public void UpdateMovement(IMovementStrategy movementStrategy)
        {
            setMovementStrategy(movementStrategy);
        }
        public void DropCoin(Canvas myCanvas, double x, double y, List<Rectangle> drop)
        {
            Rectangle coin = new Rectangle
            {
                Name = "coin",
                Tag = 1,
                Height = 30,
                Width = 30,
                Fill = new VisualBrush((ImageManager.CreateGif(ImageManager.CoinOrange)))
            };
            Canvas.SetLeft(coin, x);
            Canvas.SetTop(coin, y);
            myCanvas.Children.Add(coin);
            drop.Add(coin);

        }
    }
}
