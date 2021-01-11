using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP.Actions
{
    public interface ICustomObservable
    {
        void addObserver(ICustomObserver observer);
        void deleteObserver(ICustomObserver o);
        int countObservers();
        void notifyObservers();
    }
}
