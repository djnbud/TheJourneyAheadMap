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
    public class GameplayScreen : Screens //The link to Player, MapCreation, AI etc
    {
        #region Variables

        List<string> line;
        List<string> creatures;
        FileManager fileManager;
        EntityManager entityManager;
        UIManager uiManager;
        Map map;
        LocationsManager locManager;
        Background background;
        Vector2 pPos;
        int pPosX;
        int pPosY;
        int spawnPortalID; //The portal we want the player to spawn at if came through portal and not loaded in or new game.
        RandCreature randCreature;
        
        
        #endregion

        #region Methods

        public override void LoadContent(ContentManager Content, InputManager inputManager)
        {
            base.LoadContent(Content, inputManager);
            line = new List<string>();
            fileManager = new FileManager();
            entityManager = new EntityManager();
            uiManager = new UIManager();
            creatures = new List<string>();
            map = new Map();
            MapContents mc = new MapContents(); // current map contents
            List<int> ID;
            ID = new List<int>();
            spawnPortalID = 0;
            mc.Biome = PlayerDetails.Instance.Area;
            pPos = PlayerDetails.Instance.PPos;
            mc.XLoc = PlayerDetails.Instance.AreaPosX;
            mc.YLoc = PlayerDetails.Instance.AreaPosY;
            locManager = new LocationsManager();
            background = new Background();
            
            //add optional biomes is specific order based on ones next to them can be spawned next to them
            if (PlayerDetails.Instance.NewGame == true)
            {
                LocationsManager.Instance.optionalBiomes = new List<string>();
                if (PlayerDetails.Instance.Loaded == false)
                {
                    LocationsManager.Instance.MapContents = new List<MapContents>();
                }
                LocationsManager.Instance.optionalBiomes.Add("Woodland");
                LocationsManager.Instance.optionalBiomes.Add("Plains");
                LocationsManager.Instance.optionalBiomes.Add("Desert");
                LocationsManager.Instance.optionalBiomes.Add("Hills");
                LocationsManager.Instance.optionalBiomes.Add("Snow");
                LocationsManager.Instance.optionalBiomes.Add("Plains");
                LocationsManager.Instance.optionalBiomes.Add("Hills");
            }
            PlayerDetails.Instance.NewGame = false;
            if (!locManager.locationsCheck(LocationsManager.Instance.MapContents, mc.XLoc, mc.YLoc))
            {
                mc.MapIDs = locManager.locationBuilder(mc.Biome);
                LocationsManager.Instance.MapContents.Add(mc);
            }
            else
            {
                for(int i = 0; i < LocationsManager.Instance.MapContents.Count; i++)
                {

                    mc.MapIDs = locManager.locationMapIdGet(LocationsManager.Instance.MapContents, mc.XLoc, mc.YLoc);
                }
            }
            
            string filename = @"Load\" + mc.Biome + @"\Creatures.jo";
            map.LoadContent(Content, inputManager, mc.Biome, mc.MapIDs);//Might need to change from instance of class to actually creating the class
            background.LoadContent(content, mc.Biome, map.XMax);
            if (PlayerDetails.Instance.FromPortal)//if user came from portal then figure out which portal they should spawn near.
            {
                bool odd = IsOdd(PlayerDetails.Instance.PortalID);

                spawnPortalID = portalSpawnAt(PlayerDetails.Instance.PortalID);
                
                for (int i = 0; i < map.portal.Count; i++)
                {
                    if (map.portal[i] == spawnPortalID)
                    {
                        pPos.Y = (map.portalY[i] * 64);
                        if (odd)
                        {
                            pPos.X = (map.portalX[i] * 64) - 150;
                        }
                        else
                        {
                            pPos.X = (map.portalX[i] * 64) + 150;
                        }
                    }
                }
            }

            entityManager.LoadContent(Content, inputManager, pPos, map);
            uiManager.LoadContent(Content, inputManager, int.Parse(pPos.X.ToString()), int.Parse(pPos.Y.ToString()));
            
            using (StreamReader sr = new StreamReader(filename))
            {
                string cLine;
                while ((cLine = sr.ReadLine()) != null)
                {
                    creatures.Add(cLine);
                }
            }
            Random rand = new Random();
            for (int i = 0; i < map.creature.Count; i++) //random Creature Generator
            {
                randCreature = new RandCreature();
                ID.Add(map.creature[i]);
                randCreature.randCreature(i,creatures.Count, creatures, ID[i], entityManager, rand);
                
            }

            //Check if xml area file exists and if not create it other wise just update it

        }
        //used to check which portal the user needs to come out at
        public static bool IsOdd(int value)
        {
            return value % 2 != 0;
        }

        public static int portalSpawnAt(int value)
        {
            if (value == 1)
                return 6;
            if (value == 2)
                return 5;
            if (value == 3)
                return 4;
            if (value == 4)
                return 3;
            if (value == 5)
                return 2;
            if (value == 6)
                return 1;
            return 0;
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            map.UnloadContent();
            entityManager.UnloadContent();
            uiManager.UnloadContent();
            creatures = null;
            background.UnloadContent();
            
        }

        public override void Update(GameTime gameTime, GraphicsDeviceManager Graphics)
        {
            base.Update(gameTime, Graphics);
            entityManager.Update(gameTime, Graphics);            
            pPosX = Convert.ToInt32(entityManager.Player.PPlayer.X);
            pPosY = Convert.ToInt32(entityManager.Player.PPlayer.Y);
            uiManager.Update(gameTime, pPosX, pPosY);
            background.Update(entityManager.Player.Velocity, entityManager.Player.XCollided, entityManager.Player.YCollided);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            background.Draw(spriteBatch);
            map.Draw(spriteBatch);
            entityManager.Draw(spriteBatch);
            uiManager.Draw(spriteBatch);
            
        }

        #endregion


    }
}
