using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using ZTP.Actions;
using ZTP.Images;
using ZTP.PlayerClassess;

namespace ZTP.Monsters
{
    public class Demon : IMonster, ICustomObserver
    {
        public Demon()
        {
            this.Speed = 3;
            this.Damage = 5;
            this.HitPoints = 7;
            this.Images = new List<string>();
            Images.Add(ImageManager.DemonBack);
            Images.Add(ImageManager.DemonLeft);
            Images.Add(ImageManager.DemonFront);
            Images.Add(ImageManager.DemonRight);
            this.Instance = new Rectangle
            {
                Name = "enemy",
                Tag = "demon",
                Height = 200,
                Width = 200,
            };
            Direction = 0;
            MovementStrategy = new RegularMovementStrategy();
        }
        public int HitPoints { get; set; }
        public Rectangle Instance { get; set; }
        public int Speed { get; set; }
        public int Damage { get; set; }
        public List<string> Images { get; set; }
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
    }
}
