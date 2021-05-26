using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.Timers;

namespace TheJourneyAhead
{
    class ListedSaves
    {
        ContentManager content;
        List<string> lFilesdir = new List<string>();
        List<string> lFilesNames = new List<string>();
        List<Vector2> lFilesPos = new List<Vector2>();
        List<Rectangle> lFileRect = new List<Rectangle>();
        String name;
        int xCalc;
        String dispName;
        List<Color> lColor = new List<Color>();
        List<Boolean> lBool = new List<bool>();
        Texture2D textureRect;
        int y;
        SpriteFont font;
        Color LoadColor;
        Rectangle LoadRect;
        Vector2 LoadPos;
        bool loading;
        bool deleting;
        Color DeleteColor;
        Rectangle DeleteRect;
        Vector2 DeletePos;
        string dimensionsX = ScreenManager.Instance.Dimensions.X.ToString();        
        bool progressed; //progress message
        Rectangle progRect;
        Vector2 progPos;
        List<Texture2D> textures; //Particle System textures
        List<Color> pcolor;
        ParticleEngine menuParticleEngine;
        SaveLoad Delete;
        Timer timer1 = new Timer();
        IAsyncResult result;
        StorageDevice device;
        bool GameLoadRequested;

        public void DoEnumerate(StorageDevice device)
        {
            y = 220;
            IAsyncResult result =
                device.BeginOpenContainer("StorageDemo", null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            StorageContainer container = device.EndOpenContainer(result);
            // Close the wait handle.
            result.AsyncWaitHandle.Close();
            lFilesdir.Clear();
            lFilesNames.Clear();
            lFilesPos.Clear();
            lFileRect.Clear();
            lColor.Clear();
            lBool.Clear();
            string[] FileList = container.GetFileNames();
            foreach (string filename in FileList)
            {
                name = filename.Substring(filename.LastIndexOf("\\")+1);
                dispName = name.Substring(0,name.LastIndexOf("."));
                lFilesdir.Add(filename);
                lFilesNames.Add(dispName);
                xCalc = dispName.Length * 15;
                lFilesPos.Add(new Vector2(300, y));
                lFileRect.Add(new Rectangle(300, y, xCalc, 25));
                lColor.Add(Color.Yellow);
                lBool.Add(false);
                    y += 50;
            }

            // Dispose the container.
            container.Dispose();
        }

        public void LoadContent(ContentManager content)
        {
            
            this.content = new ContentManager(content.ServiceProvider, "Content");
            GameLoadRequested = true;
            if (font == null)
                font = this.content.Load<SpriteFont>("times_new_yorker");            
            textureRect = content.Load<Texture2D>("Start");
            LoadColor = Color.Yellow;
            LoadRect = new Rectangle(int.Parse(dimensionsX) / 2 - (150 / 2), 540, 135, 35);
            LoadPos = new Vector2(ScreenManager.Instance.Dimensions.X / 2 - (LoadRect.Width / 2), 540);
            loading = false;
            DeleteColor = Color.Yellow;
            DeleteRect = new Rectangle(int.Parse(dimensionsX) / 2 - (150 / 2), 575, 135, 35);
            DeletePos = new Vector2(ScreenManager.Instance.Dimensions.X / 2 - (DeleteRect.Width / 2), 575);
            deleting = false;
            progressed = false;
            progRect = new Rectangle(int.Parse(dimensionsX) / 2 - (150 / 2), 300, 135, 35);
            progPos = new Vector2(ScreenManager.Instance.Dimensions.X / 2 - (progRect.Width / 2), 300);

            textures = new List<Texture2D>();
            textures.Add(content.Load<Texture2D>("gParticle"));

            pcolor = new List<Color>();
            pcolor.Add(Color.Blue);
            pcolor.Add(Color.Red);
            pcolor.Add(Color.Gold);
            pcolor.Add(Color.Green);

            menuParticleEngine = new ParticleEngine(textures, new Vector2(400, 240), pcolor);
            
        }

        public void Update(GameTime gameTime, InputManager inputManager)
        {
            result = StorageDevice.BeginShowSelector(
                            PlayerIndex.One, null, null);
            if ((GameLoadRequested) && (result.IsCompleted))
            {
                device = StorageDevice.EndShowSelector(result);
                if (device != null && device.IsConnected)
                {
                    DoEnumerate(device);
                    GameLoadRequested = false;
                }
            }
            menuParticleEngine.Update(gameTime, inputManager);
            //hovering over saved games
            for (int i = 0; i < lFileRect.Count; i++)
            {
                if (lBool[i] == false)
                {
                    if (lFileRect[i].Contains(inputManager.mousePos.X, inputManager.mousePos.Y))
                    {
                        menuParticleEngine.EmitterLocation = new Vector2(lFileRect[i].X + lFileRect[i].Width, lFileRect[i].Y);
                        menuParticleEngine.GenerateForMenu();
                        lColor[i] = Color.Blue;
                        if (!deleting)
                        {
                            if (inputManager.leftClick == true)
                            {
                                for (int x = 0; x < lFileRect.Count; x++)
                                {
                                    lBool[x] = false;
                                    lColor[x] = Color.Yellow;
                                }
                                //selecting saved game
                                lBool[i] = true;
                                lColor[i] = Color.Green;
                            }
                        }
                    }
                    else
                    {
                        lColor[i] = Color.Yellow;
                    }
                }
                else
                {
                    menuParticleEngine.EmitterLocation = new Vector2(lFileRect[i].X + lFileRect[i].Width, lFileRect[i].Y);
                    menuParticleEngine.GenerateForMenu();
                }
               
            }
            //Load Save
            if (LoadRect.Contains(inputManager.mousePos.X, inputManager.mousePos.Y))
            {
                menuParticleEngine.EmitterLocation = new Vector2(LoadRect.X + LoadRect.Width, LoadRect.Y);
                menuParticleEngine.GenerateForMenu();
                LoadColor = Color.Blue;
                if (inputManager.leftClick == true)
                {
                    for (int i = 0; i < lFileRect.Count; i++)
                    {
                        if (lBool[i] == true)
                        {
                            loading = true;
                            
                        }
                    }
                }

            }
            else
                LoadColor = Color.Yellow;

            if (loading == true)
            {
                for (int i = 0; i < lFileRect.Count; i++)
                {
                    if (lBool[i] == true)
                    {
                        if (deleting == false)
                        {
                            ScreenManager.Instance.FileLoad = lFilesNames[i];
                            Type newClass = Type.GetType("TheJourneyAhead." + "LoadingScreen");
                            ScreenManager.Instance.AddScreen((Screens)Activator.CreateInstance(newClass), inputManager);                            
                        }
                    }
                }
            }
            //Delete Save
            if (DeleteRect.Contains(inputManager.mousePos.X, inputManager.mousePos.Y))
            {
                menuParticleEngine.EmitterLocation = new Vector2(DeleteRect.X + DeleteRect.Width, DeleteRect.Y);
                menuParticleEngine.GenerateForMenu();
                DeleteColor = Color.Blue;
                if (inputManager.leftClick == true)
                {
                    for (int i = 0; i < lFileRect.Count; i++)
                    {
                        if (lBool[i] == true)
                        {
                            deleting = true;
                            
                        }
                    }
                }

            }
            else
                DeleteColor = Color.Yellow;

            if (deleting == true)
            {
                for (int i = 0; i < lFileRect.Count; i++)
                {
                    if (lBool[i] == true)
                    {
                        Delete = new SaveLoad();
                        Delete.Delete(lFilesNames[i]);
                        if (Delete.Deleted == true)
                        {
                            timer1.Interval = 10; //Sets timer1 interval to 1/2 second                       
                            timer1.Elapsed += new ElapsedEventHandler(timer1_Tick); //Once timer1 has finished go to event handler method below
                            timer1.Start();
                        }
                    }
                }
            }

            if (!progressed)
            {
                menuParticleEngine.EmitterLocation = new Vector2(progRect.X + progRect.Width, progRect.Y);
                menuParticleEngine.GenerateForMenu();
            }
        }

        void timer1_Tick(object sender, EventArgs e) //timer1 event handler
        {
            timer1.Stop(); //stop timer1            
            deleting = false;
            GameLoadRequested = true;
        }  

        public void UnloadContent()
        {
            lFilesNames.Clear();
            lFilesPos.Clear();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < lFilesNames.Count; i++)
            {
                spriteBatch.Draw(textureRect,lFileRect[i],Color.Black);
                spriteBatch.DrawString(font, lFilesNames[i], lFilesPos[i], lColor[i]);
                progressed = true;
            }
            if (lFilesNames.Count < 1 && !GameLoadRequested)
                progressed = true;
            spriteBatch.DrawString(font, "Load Game", LoadPos, LoadColor);
            spriteBatch.DrawString(font, "Delete Save", DeletePos, DeleteColor);
            if (!progressed)
            {
                spriteBatch.DrawString(font, "Loading...", progPos, Color.Red);
            }
            if (deleting)
            {
                spriteBatch.DrawString(font, "Deleting...", progPos, Color.Red);
            }
            menuParticleEngine.Draw(spriteBatch);

        }
    }
}
