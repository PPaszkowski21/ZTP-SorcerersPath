using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP.GameSingleton
{
    public class GameSave
    {
        public int Gold { get; set; }
        public bool ToxicBoltAvailable { get; set; }
        public bool LightningAvailable { get; set; }
        public bool DashAvailable { get; set; }
        public bool FearAvailable { get; set; }
        public bool ExplosionAvailable { get; set; }
        public bool StageFirst { get; set; }
        public bool StageSecond { get; set; }
        public bool StageThird { get; set; }
        public bool StageFourth { get; set; }
    }
}
