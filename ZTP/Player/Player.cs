using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP.Player
{
    public class Player
    {
        public int HitPoints { get; private set; }
        public int ExperiencePoints { get; private set; }
        public int Gold { get; private set; }

        internal Player(int _HitPoints, int _ExperiencePoints, int _Gold)
        {
            HitPoints = _HitPoints;
            ExperiencePoints = _ExperiencePoints;
            Gold = _Gold;
        }            
    }
}
