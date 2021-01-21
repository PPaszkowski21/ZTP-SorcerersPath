using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ZTP.Actions;
using ZTP.Images;
using ZTP.PlayerClassess;

namespace ZTP.Monsters
{
    public class Demon : IMonster, IMonsterObserver
    {
        public Demon()
        {
            this.Speed = 3;
            this.Damage = 5;
            this.HitPoints = 700;
            this.Images = new List<VisualBrush>();
            Images.Add(new VisualBrush(ImageManager.CreateGif(ImageManager.DemonBack)));
            Images.Add(new VisualBrush(ImageManager.CreateGif(ImageManager.DemonLeft)));
            Images.Add(new VisualBrush(ImageManager.CreateGif(ImageManager.DemonFront)));
            Images.Add(new VisualBrush(ImageManager.CreateGif(ImageManager.DemonRight)));
            this.Instance = new Rectangle
            {
                Name = "enemy",
                Tag = "demon",
                Height = 200,
                Width = 200,
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
        public int GifTimer { get; set; }
        public IMovementStrategy MovementStrategy { get; set; }
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
            MovementStrategy.CollisionAvoiding(monster, monsterHitBox, otherMonsterHitBox, myCanvas);
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
                Tag = 3,
                Height = 30,
                Width = 30,
                Fill = new VisualBrush(ImageManager.CreateGif(ImageManager.CoinRed))
            };
            Canvas.SetLeft(coin, x);
            Canvas.SetTop(coin, y);
            myCanvas.Children.Add(coin);
            drop.Add(coin);

        }
    }
}
