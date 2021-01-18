using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using ZTP.Actions;
using ZTP.Images;
using ZTP.PlayerClassess;

namespace ZTP.Monsters
{
    public class Phantom : IMonster, ICustomObserver
    {
        public Phantom()
        {
            this.Speed = 3;
            this.Damage = 2;
            this.HitPoints = 2;
            this.Images = new List<string>();
            Images.Add(ImageManager.PhantomBack);
            Images.Add(ImageManager.PhantomLeft);
            Images.Add(ImageManager.PhantomFront);
            Images.Add(ImageManager.PhantomRight);
            this.Instance = new Rectangle
            {
                Name = "enemy",
                Tag = "phantom",
                Height = 100,
                Width = 100,
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
