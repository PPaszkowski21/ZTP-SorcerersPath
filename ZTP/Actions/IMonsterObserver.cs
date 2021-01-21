using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP.Actions
{
    public interface IMonsterObserver
    {
        void UpdateMovement(IMovementStrategy strategy);
    }
}
