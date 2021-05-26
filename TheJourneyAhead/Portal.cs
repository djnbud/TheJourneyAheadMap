using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TheJourneyAhead
{
    public class Portal
    {
        Vector2 pos;
        Texture2D t;
        Rectangle r;
        int id;
        bool collided;
        string Area;        
        int AreaX;
        int AreaY;
        string Name;
        InputManager inputManager;
        LocationsManager locManager;
        public Vector2 PPortal
        {
            get { return pos; }
            set { pos = value; }
        }              

        public Rectangle RPortal
        {
            get { return r; }
            set { r = value; }
        }

        public int idPortal
        {
            get { return id; }
        }

        public void LoadContent(ContentManager Content, InputManager InputManager, int ID, int PosX, int PosY)
        {
            inputManager = InputManager;
            id = ID;
            t = Content.Load<Texture2D>("temp_portal");
            pos = new Vector2(PosX * 64, PosY * 64);
            r = new Rectangle(Convert.ToInt32(pos.X), Convert.ToInt32(pos.Y), t.Width, t.Height);
            collided = false;
            Area = PlayerDetails.Instance.Area;
            AreaX = PlayerDetails.Instance.AreaPosX;
            AreaY = PlayerDetails.Instance.AreaPosY;
            Name = PlayerDetails.Instance.PName;
            locManager = new LocationsManager();            
        }

        public void Update(CollisionManager cm, InputManager input)
        {
            collided = cm.checkPPos(r);
            //if player collides with portal then they will go to either existing area or new
            
            if (collided)
            {
                if (id == 1 || id == 3 || id == 5)
                {
                    //x area - 1
                    AreaX--;
                }
                else
                {
                    //x area + 1
                    AreaX++;
                }

                if (id > 4)
                {
                    //y area - 1
                    AreaY--;
                }

                if (id < 3)
                {
                    //y area + 1
                    AreaY++;
                }

                //if (id == 3 || id == 4)
                //y area same
                //use below to create new area.... see menumanager code for help. Also will need to go to gameplayscreen as linkID[i]
                //Will need to change file so it knows what area it is loading

                if (!locManager.locationsCheck(LocationsManager.Instance.MapContents, AreaX, AreaY))
                {
                    Area = locManager.locationChange(Area, LocationsManager.Instance.optionalBiomes);
                }
                else
                {
                    Area = locManager.locationGet(LocationsManager.Instance.MapContents, AreaX, AreaY);
                }

                PlayerDetails.Instance.Area = Area;
                PlayerDetails.Instance.PPos = new Vector2(220, 500);
                PlayerDetails.Instance.AreaPosX = AreaX;
                PlayerDetails.Instance.AreaPosY = AreaY;
                PlayerDetails.Instance.PName = Name;
                PlayerDetails.Instance.FromPortal = true;
                PlayerDetails.Instance.PortalID = id;   

                Type newClass = Type.GetType("TheJourneyAhead.GameplayScreen");
                ScreenManager.Instance.AddScreen((Screens)Activator.CreateInstance(newClass), input);                
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(t, r, Color.White);

        }
    }
}
