using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ZTP.Actions;
using ZTP.PlayerClassess;

namespace ZTP.Monsters
{
    public interface IMonster : IMonsterObserver
    {
        int HitPoints { get; set; }
        Rectangle Instance { get; set; }
        int Speed { get; set; }
        int Damage { get; set; }
        List<VisualBrush> Images { get; set; }
        int Direction { get; set; }
        int PreviousDirection { get; set; }
        IMovementStrategy MovementStrategy { get; set; }
        int GifTimer { get; set; }
        void Move(Player player, IMonster thisMonster, Canvas myCanvas);
        void CollisionAvoiding(IMonster monster, Rect monsterHitBox, Rect otherMonsterHitBox, Canvas myCanvas);
        void DropCoin(Canvas myCanvas, double x, double y, List<Rectangle> drop);
    }
}
