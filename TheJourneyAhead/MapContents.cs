using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheJourneyAhead
{
    public class MapContents //Will store the different x/y location, biome, map IDs used in location etc
    {
        #region variables

        int xLoc;
        int yLoc;
        string biome;
        List<int> mapIDs = new List<int>(); //list of each map ID to create location

        public int XLoc
        {
            get { return xLoc; }
            set { xLoc = value; }
        }
        public int YLoc
        {
            get { return yLoc; }
            set { yLoc = value; }
        }
        public string Biome
        {
            get { return biome; }
            set { biome = value; }
        }
        public List<int> MapIDs
        {
            get { return mapIDs; }
            set { mapIDs = value; }
        }

        #endregion

        #region methods

        public void LoadContent(ContentManager Content, InputManager inputManager)
        {

        }
        #endregion


    }
}
