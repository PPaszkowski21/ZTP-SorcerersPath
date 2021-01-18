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
    public class Lich : IMonster, ICustomObserver
    {
        public Lich()
        {
            this.Speed = 1;
            this.Damage = 7;
            this.HitPoints = 4;
            this.Images = new List<string>();
            Images.Add(ImageManager.LichBack);
            Images.Add(ImageManager.LichLeft);
            Images.Add(ImageManager.LichFront);
            Images.Add(ImageManager.LichRight);
            this.Instance = new Rectangle
            {
                Name = "enemy",
                Tag = "lich",
                Height = 150,
                Width = 150,
            };
            Direction = 0;
            MovementStrategy = new RegularMovementStrategy();
        }
        public int HitPoints { get; set; }
        public Rectangle Instance { get; set; }
        public int Speed { get; set; }
        public int Damage { get; set; }
        public List<string> Images { get; set; }
        public int GifTimer { get; set; }
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
            MovementStrategy.CollisionAvoiding(monster, monsterHitBox, otherMonsterHitBox, myCanvas);
        }
        public void UpdateMovement(IMovementStrategy movementStrategy)
        {
            setMovementStrategy(movementStrategy);
        }
    }
}
