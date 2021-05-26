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
    public class Map
    {
        #region variables

        private static Map instance;
        Layer layer;
        public List<int> tile;
        public List<int> posX;
        public List<int> posY;
        public List<Rectangle> rTile;
        public List<int> creature;
        public List<int> creatureX;
        public List<int> creatureY;
        public List<string> tileSolid;
        public List<int> portal;
        public List<int> portalX;
        public List<int> portalY;
        static int xMax;

        int tilecount = 0;
        public static Map Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Map();
                }
                return instance;
            }
        }
        public int XMax
        {
            get { return xMax; }
            set { xMax = value; }
        }

        #endregion

        #region Methods

        public void LoadContent(ContentManager Content, InputManager inputManager, string area, List<int> mapIDs)
        {
            layer = new Layer();
            rTile = new List<Rectangle>();            
            tile = new List<int>();
            posX = new List<int>();
            posY = new List<int>();
            creature = new List<int>();
            creatureX = new List<int>();
            creatureY = new List<int>();
            tileSolid = new List<string>();
            portal = new List<int>();
            portalX = new List<int>();
            portalY = new List<int>();
            int y = 0;
            int x = 0;
            int currentx = 0;
            string path = "Load\\" + area;
            
            //string Area = "Woodland"; //Need to fix this!!!!
            for (int i = 0; i < mapIDs.Count; i++)
            {
                string filename = @"Load\" + area + @"\Map" + mapIDs[i] + ".jo";
                using (StreamReader sr = new StreamReader(filename))
                {
                    y = 0;
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        x = currentx;

                        string[] numbers = line.Split(',');

                        foreach (string e in numbers)
                        {

                            tilecount++;
                            if (e.Contains("M"))
                            {
                                creature.Add(Int32.Parse(e.Substring(1, 1)));
                                creatureX.Add(x);
                                creatureY.Add(y);
                                tile.Add(0);
                                tileSolid.Add("");
                            }
                            else if (e.Contains("P"))
                            {
                                int pId = Int32.Parse(e.Substring(1, 1));
                                if (i == 0 && (pId == 1 || pId == 3))
                                {
                                    portal.Add(pId);
                                    portalX.Add(x);
                                    portalY.Add(y);
                                }else
                                if (i == mapIDs.Count - 1 && (pId == 2 || pId == 4))
                                {
                                    portal.Add(pId);
                                    portalX.Add(x);
                                    portalY.Add(y);
                                }
                                tile.Add(0);
                                tileSolid.Add("");
                            }


                            else
                            {
                                if (e.Contains("0"))
                                {
                                    tileSolid.Add("");
                                }
                                else
                                {
                                    tileSolid.Add("Solid");
                                }
                                tile.Add(int.Parse(e));
                            }
                            posX.Add(x);
                            posY.Add(y);

                            x++;

                        }
                        y++;
                    }
                    currentx = x + 5;
                }
            }
            xMax = currentx * 64;
            layer.LoadContent();
            for (int i = 0; i < tilecount; i++)
            {
            layer.AddTiles(Content, posX[i], posY[i], tile[i], area); //Adds tile read from file to Layer class list
                
            }
            layer.LoadTiles(Content);
            rTile = layer.tile_Rect;
        }


        public void UnloadContent()
        {
            tilecount = 0;
            tile.Clear();
            posX.Clear();
            posY.Clear();

            layer.UnloadContent();
        }

        public void Update(GameTime gameTime, GraphicsDeviceManager Graphics)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            layer.Draw(spriteBatch);
        }
        #endregion


    }
}
