using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using ZTP.Actions;
using ZTP.GameSingleton;
using ZTP.Images;
using ZTP.PlayerClassess;

namespace ZTP.Monsters
{
    public class Skeleton : IMonster, ICustomObserver
    {
        public Skeleton()
        {
            this.Speed = 4;
            this.Damage = 1;
            this.HitPoints = 1;
            this.Images = new List<string>();
            Images.Add(ImageManager.testDown);
            Images.Add(ImageManager.testLeft);
            Images.Add(ImageManager.testUp);
            Images.Add(ImageManager.testRight);
            this.Instance = new Rectangle
            {
                Name = "enemy",
                Tag = "skeleton",
                Height = 70,
                Width = 70,
            };
            MovementStrategy = new RegularMovementStrategy();
        }
        public int HitPoints { get; set; }
        public Rectangle Instance { get; set; }
        public int Speed { get; set; }
        public int Damage { get; set; }
        public List<string> Images { get; set; }
        public int Direction { get; set; }
        public int PreviousDirection { get; set; }
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
            MovementStrategy.CollisionAvoiding(monster,monsterHitBox,otherMonsterHitBox,myCanvas);
        }
        public void UpdateMovement(IMovementStrategy movementStrategy)
        {
            setMovementStrategy(movementStrategy);
        }
    }
}
