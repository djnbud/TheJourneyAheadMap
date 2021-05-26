using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TheJourneyAhead
{
    class capsLock
    {
        bool caps;
        public static bool capslockCheck()
        {
            if (Control.IsKeyLocked(Keys.CapsLock))
                return true;
            else
                return false;
        }

        public static bool numlockCheck()
        {
            if (Control.IsKeyLocked(Keys.NumLock))
                return true;
            else
                return false;
        }
    }
}
