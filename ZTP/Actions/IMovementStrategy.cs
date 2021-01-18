using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ZTP.Monsters;
using ZTP.PlayerClassess;

namespace ZTP.Actions
{
    public interface IMovementStrategy
    {
        void Move(Player player, IMonster monster, Canvas myCanvas);
        void CollisionAvoiding(IMonster monster, Rect monsterHitBox, Rect otherMonsterHitBox, Canvas myCanvas);
    }
}
