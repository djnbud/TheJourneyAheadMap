using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheJourneyAhead
{
    public class RandMove
    {
        public int rand(int max, Random rand)
        {
            return rand.Next(max);
        }
    }
}
