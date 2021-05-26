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
    public class PlayerDetails
    {
        #region variables
        private static PlayerDetails instance;
        string area;
        int hair;
        int hairColour;
        int skin;
        int head;
        int eye;
        int eyeColour;
        string gender;
        Vector2 pPos;
        int areaPosX;
        int areaPosY;
        string pName;        
        bool fromPortal;//variable to define whether game should load near portal or actual location
        int portalID; //The id of the portal came from not the one they should spawn on!!! e.g if this is 1 then user would spawn at 2.
        bool newGame; //This will say if newly created/loaded game
        bool loaded;
        public static PlayerDetails Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PlayerDetails();
                }
                return instance;
            }
        }
        public string Area
        {
            get { return area; }
            set { area = value; }
        }

        public int Hair
        {
            get { return hair; }
            set { hair = value; }
        }

        public int HairColour
        {
            get { return hairColour; }
            set { hairColour = value; }
        }

        public int Skin
        {
            get { return skin; }
            set { skin = value; }
        }

        public int Head
        {
            get { return head; }
            set { head = value; }
        }

        public int Eye
        {
            get { return eye; }
            set { eye = value; }
        }

        public int EyeColour
        {
            get { return eyeColour; }
            set { eyeColour = value; }
        }

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public Vector2 PPos
        {
            get { return pPos; }
            set { pPos = value; }
        }

        public int AreaPosX
        {
            get { return areaPosX; }
            set { areaPosX = value; }
        }

        public int AreaPosY
        {
            get { return areaPosY; }
            set { areaPosY = value; }
        }

        public string PName
        {
            get { return pName; }
            set { pName = value; }
        }

        public bool FromPortal
        {
            get { return fromPortal; }
            set { fromPortal = value; }
        }

        public int PortalID
        {
            get { return portalID; }
            set { portalID = value; }
        }

        public bool NewGame
        {
            get { return newGame; }
            set { newGame = value; }
        }

        public bool Loaded
        {
            get { return loaded; }
            set { loaded = value; }
        }
        
        #endregion

        #region methods

        public void LoadContent(ContentManager Content, InputManager inputManager)
        {
            area = "";
            hair = 0;
            hairColour = 1;
            skin = 0;
            head = 0;
            eye = 0;
            eyeColour = 0;
            gender = "";
            pPos = new Vector2();
            areaPosX = 0;
            areaPosY = 0;
            pName = "";
            fromPortal = false;//variable to define whether game should load near portal or actual location
            portalID = 0;
            newGame = false;
            loaded = false;
        }

        #endregion

    }
}
