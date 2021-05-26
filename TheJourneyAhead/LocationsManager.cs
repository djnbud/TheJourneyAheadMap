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
    public class LocationsManager //stores x, y and biome name of locations visited
    {
        #region variables
        private static LocationsManager instance;
        List<MapContents> mapContents;
        public List<string> optionalBiomes;
        //potential for more info such as creatures stored, 
        public static LocationsManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LocationsManager();
                }
                return instance;
            }
        }
        public List<MapContents> MapContents
        {
            get { return mapContents; }
            set { mapContents = value; }
        }

        #endregion

        #region Methods
        public void LoadContent(ContentManager Content, InputManager inputManager)
        {
            optionalBiomes = new List<string>();
            mapContents = new List<MapContents>();
        }

        public void UnloadContent()
        {
            optionalBiomes.Clear();
            mapContents.Clear();
        }
        //to check locations already visited
        public bool locationsCheck(List<MapContents> mc, int currentAreaX, int currentAreaY)
        {

            for (int i = 0; i < mc.Count; i++)
            {

                if (mc[i].XLoc == currentAreaX)
                {
                    if (mc[i].YLoc == currentAreaY)
                    {
                        return true;
                    }
                }                    
                
            }
                return false;
        }

        public string locationGet(List<MapContents> mc, int currentAreaX, int currentAreaY)
        {

            for (int i = 0; i < mc.Count; i++)
            {
                if (mc[i].XLoc == currentAreaX)
                {
                    if (mc[i].YLoc == currentAreaY)
                    {
                        return mc[i].Biome;
                    }
                }
            }
            return "";
        }

        public List<int> locationMapIdGet(List<MapContents> mc, int currentAreaX, int currentAreaY)
        {
            List<int> mapIds = new List<int>();

            for (int i = 0; i < mc.Count; i++)
            {
                if (mc[i].XLoc == currentAreaX)
                {
                    if (mc[i].YLoc == currentAreaY)
                    {
                        return mc[i].MapIDs;
                    }
                }
            }
                return mapIds;
        }

        //changes location based on current location randomly but only with locations that make sense e.g desert would not be next to snow
        public string locationChange(string currentArea, List<string> locationOptions)
        {
            string newArea = currentArea;
            Random rand = new Random();
            List<string> potentialAreas = new List<string>();            
                
            for (int i = 0; i < locationOptions.Count; i++)
            {
                if (currentArea == locationOptions[i])
                {
                    if (!potentialAreas.Contains(locationOptions[i]))
                    potentialAreas.Add(locationOptions[i]);
                    if (i == (locationOptions.Count - 1))
                    {
                        potentialAreas.Add(locationOptions[i - 1]);
                        potentialAreas.Add(locationOptions[0]);
                    }
                    else if (i == 0)
                    {
                        potentialAreas.Add(locationOptions[i + 1]);
                        potentialAreas.Add(locationOptions[locationOptions.Count - 1]);
                    }
                    else
                    {
                        potentialAreas.Add(locationOptions[i - 1]);
                        potentialAreas.Add(locationOptions[i + 1]);
                    }
                }
            }

            int location;
            //entityManager = new EntityManager();

            location = rand.Next(potentialAreas.Count);
            newArea = potentialAreas[location];
            potentialAreas.Clear();
            return newArea;
        }

        public List<int> locationBuilder(string area)//puts together random list of map ids for location
        {
            List<int> mapIDs = new List<int>();
            int count;
            Random randCount = new Random();//randomises amount of map files to use
            
            string path = "Load\\" + area;
            int maxCount = 3; //max amount of maps can use for location
            int fileCount = Directory.GetFiles(path + "\\", "Map*", SearchOption.TopDirectoryOnly).Length;
            if (maxCount > fileCount)
                maxCount = fileCount;

            count = randCount.Next(maxCount) + 1;//chosen amount of map files to use

            for (int i = 0; i < count; i++)
            {
                Random randID = new Random();//randomises map id to use
                mapIDs.Add(randID.Next(fileCount)+1);//add map id
            }

            return mapIDs;
        }

        public void Update(GameTime gameTime, GraphicsDeviceManager Graphics)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }

        #endregion
    }
}
