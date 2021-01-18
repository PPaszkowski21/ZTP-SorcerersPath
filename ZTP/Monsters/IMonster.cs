using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using ZTP.Actions;
using ZTP.PlayerClassess;

namespace ZTP.Monsters
{
    public interface IMonster : ICustomObserver
    {
        int HitPoints { get; set; }
        Rectangle Instance { get; set; }
        int Speed { get; set; }
        int Damage { get; set; }
        List<string> Images { get; set; }
        int Direction { get; set; }
        int PreviousDirection { get; set; }
        IMovementStrategy MovementStrategy { get; set; }
        int GifTimer { get; set; }
        void Move(Player player, IMonster thisMonster, Canvas myCanvas);
        void CollisionAvoiding(IMonster monster, Rect monsterHitBox, Rect otherMonsterHitBox, Canvas myCanvas);
    }
}
