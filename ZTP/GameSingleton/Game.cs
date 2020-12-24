using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP.GameSingleton
{
    public class Game
    {
        private Game() { }

        private static Game _instance;

        public static Game GetInstance()
        {
            if (_instance == null)
                _instance = new Game();
            return _instance;
        }
    }
}
