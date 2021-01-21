using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP.Actions
{
    public interface IPlayerObservable
    {
        void addObserver(IMonsterObserver observer);
        void deleteObserver(IMonsterObserver o);
        int countObservers();
        void notifyObservers(IMovementStrategy strategy);
    }
}
