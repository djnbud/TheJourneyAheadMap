using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace TheJourneyAhead
{
    public class MenuManager
    {

        #region Variables
        
        //Lists of contents of menu screens stored in files
        //--------------------------
        List<string> menuItems;
        List<Texture2D> menuImages;
        List<List<string>> attributes, contents;
        List<string> linkType, linkID;
        List<Vector2> pos;
        List<Vector2> imagePos;
        List<Rectangle> rect;
        List<Color> fontColor;
        //--------------------------
        
        List<Texture2D> textures; //Particle System textures
        List<Color> pcolor;
        
        SoundEffect clickSound;
        SpriteFont font; //Can be list but currently only one font will be used
        Texture2D textureRect; //For rectangles of fonts
        ContentManager content;       
        Vector2 position;
        Vector2 imagePosition;
        string dimensionsX = ScreenManager.Instance.Dimensions.X.ToString();
        string dimensionsY = ScreenManager.Instance.Dimensions.Y.ToString();
        private static MenuManager instance;

        FileManager fileManager;
        ParticleEngine menuParticleEngine;

        #endregion


        public static MenuManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MenuManager();
                }
                return instance;
            }
        }

        #region Methods


        public void LoadContent(ContentManager content, string id)
        {
            //Loads all the appropriate menu items and images by using file manager and reading the content of Menus.jo

            this.content = new ContentManager(content.ServiceProvider, "Content");
            menuItems = new List<string>();
            menuImages = new List<Texture2D>();
            attributes = new List<List<string>>();
            contents = new List<List<string>>();
            linkType = new List<string>();
            linkID = new List<string>();
            pos = new List<Vector2>();
            imagePos = new List<Vector2>();
            rect = new List<Rectangle>();
            fontColor = new List<Color>();            
            textures = new List<Texture2D>();
            textures.Add(content.Load<Texture2D>("gParticle"));

            pcolor = new List<Color>();
            pcolor.Add(Color.Blue);
            pcolor.Add(Color.Red);
            pcolor.Add(Color.Gold);
            pcolor.Add(Color.Green);

            menuParticleEngine = new ParticleEngine(textures, new Vector2(400, 240), pcolor);
            
            textureRect = content.Load<Texture2D>("Start");
            imagePosition = Vector2.Zero;
            position = Vector2.Zero;
            
            fileManager = new FileManager();
            fileManager.LoadContent("Load/Menus.jo", attributes, contents, null, id);

            for (int i = 0; i < attributes.Count; i++)
            {
                for (int j = 0; j < attributes[i].Count; j++)
                {
                    switch (attributes[i][j])
                    {
                        case "Position":
                            string[] temp = contents[i][j].Split(' ');
                            pos.Add(new Vector2(float.Parse(temp[0]), float.Parse(temp[1])));
                            break;

                        case "ImagePosition":
                            string[] temp2 = contents[i][j].Split(' ');
                            imagePos.Add(new Vector2(float.Parse(temp2[0]), float.Parse(temp2[1])));
                            break;

                        case "RectPosition":
                            string[] temp3 = contents[i][j].Split(' ');
                            rect.Add(new Rectangle(int.Parse(dimensionsX) / 2 - (int.Parse(temp3[2]) / 2), int.Parse(temp3[1]), int.Parse(temp3[2]), int.Parse(temp3[3])));
                            break;

                        case "Font":
                            font = content.Load<SpriteFont>(contents[i][j]);
                            break;
                        case "FontColor":
                            string[] temp4 = contents[i][j].Split(' ');
                            fontColor.Add(new Color(float.Parse(temp4[0]), float.Parse(temp4[1]), float.Parse(temp4[2])));
                            break;

                        case "Item":
                            menuItems.Add(contents[i][j]);
                            break;

                        case "Image":
                            menuImages.Add(content.Load<Texture2D>(contents[i][j]));
                            break;

                        case "LinkType":
                            linkType.Add(contents[i][j]);
                            break;

                        case "LinkID":
                            linkID.Add(contents[i][j]);
                            break;
                    }
                }
            }
            SetMenuItems();

        }

        private void SetMenuItems()
        {

            for (int i = 0; i < menuItems.Count; i++)
            {
                if (menuItems.Count == i)
                    pos.Add(new Vector2(position.X, position.Y));
                pos.Add(new Vector2(position.X, position.Y));
            }

            for (int j = 0; j < menuImages.Count; j++)
            {
                
                    if (menuImages.Count == j)
                        imagePos.Add(new Vector2(imagePosition.X, imagePosition.Y));
                    imagePos.Add(new Vector2(imagePosition.X, imagePosition.Y));
                
            }

        }

        public void UnloadContent()
        {
            content.Unload();
            fileManager = null;
            menuItems.Clear();
            menuImages.Clear();
        }

        public void Update(GameTime gameTime, InputManager inputManager, GraphicsDeviceManager Graphics)
        {
            menuParticleEngine.Update(gameTime, inputManager);
            


            for (int i = 0; i < menuItems.Count; i++)
            {
                if (rect[i].Contains(inputManager.mousePos.X, inputManager.mousePos.Y))
                {
                    fontColor[i] = Color.Blue;
                    menuParticleEngine.EmitterLocation = new Vector2(rect[i].X + rect[i].Width, rect[i].Y);
                    
                    menuParticleEngine.GenerateForMenu();

                    if (inputManager.leftClick == true)
                    {
                        if (linkID[i] == "FullScreen")
                        {
                            
                            Graphics.ToggleFullScreen();
                            
                            Graphics.ApplyChanges();
                        }
                        if (linkID[i] == "Exit")
                        {
                            System.Environment.Exit(1);
                        }
                        if (linkType[i] == "Screen")
                        {
                            Type newClass = Type.GetType("TheJourneyAhead." + linkID[i]);
                            ScreenManager.Instance.AddScreen((Screens)Activator.CreateInstance(newClass), inputManager);

                        }
                    }
                }
                else                    
                 fontColor[i] = Color.Yellow;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {          
            

            //Draws the menu for current screen
            for (int j = 0; j < menuImages.Count; j++)
            {
                spriteBatch.Draw(menuImages[j], imagePos[j] = new Vector2(ScreenManager.Instance.Dimensions.X / 2 - (menuImages[j].Width / 2), imagePos[j].Y), Color.White);
            }
            
            for (int i = 0; i < menuItems.Count; i++)
            {                
                spriteBatch.Draw(textureRect, rect[i], Color.Black);
                spriteBatch.DrawString(font, menuItems[i], pos[i] = new Vector2(ScreenManager.Instance.Dimensions.X / 2 - (rect[i].Width / 2), pos[i].Y), fontColor[i]);
            }
            menuParticleEngine.Draw(spriteBatch);

            
            
        }
        #endregion
    }
}
