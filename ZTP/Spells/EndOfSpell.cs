using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP.Spells
{
    public class EndOfSpell
    {
        public double Top { get; set; }
        public double Left { get; set; }
        public bool End { get; set; }
        public EndOfSpell(double top, double left, bool end)
        {
            Top = top;
            Left = left;
            End = end;
        }
    }
}
