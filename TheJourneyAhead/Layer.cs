using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TheJourneyAhead
{
    //draws map tiles and gets positions from Map.cs
    public class Layer
    {

        #region Variables

        List<int> PosX;
        List<int> PosY;
        public List<int> Tile;
        List<Texture2D> tile_texture;
        
        public List<Rectangle> tile_Rect;
        List<string> Type;
        List<String> solidTile;
        private static Layer instance;
        #endregion

        #region Methods

        public static Layer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Layer();
                }
                return instance;
            }
        }


        public void LoadContent()
        {
            
            solidTile = new List<string>();
            PosX = new List<int>();
            PosY = new List<int>();
            Tile = new List<int>();
            Type = new List<string>();
            tile_Rect = new List<Rectangle>();
            tile_texture = new List<Texture2D>();
        }

        public void AddTiles(ContentManager Content, int posX, int posY, int currentTile, string type)
        {
           
            PosX.Add(posX);
            PosY.Add(posY);
            Tile.Add(currentTile); //collects tile information from Map.cs
            Type.Add(type);
            
        }

        public void LoadTiles(ContentManager Content)
        {
            
            for (int i = 0; i < Tile.Count; i++)
            {

                tile_texture.Add(Content.Load<Texture2D>(Type[i] + "\\Tile" + Tile[i]));
                tile_Rect.Add(new Rectangle((PosX[i] * 64), (PosY[i] * 64), 64, 64));
            }

        }

        
        public void UnloadContent()
        {
            PosX.Clear();
            PosY.Clear();
            Tile.Clear();
            Type.Clear();
            tile_Rect.Clear();
            tile_texture.Clear();
        }


        

        public void Draw(SpriteBatch spriteBatch)
        {

            for(int i = 0; i < Tile.Count; i++)
            {

                
             spriteBatch.Draw(tile_texture[i],tile_Rect[i],Color.White);
                
            }
            
            
        }
        #endregion

                
    }
}
