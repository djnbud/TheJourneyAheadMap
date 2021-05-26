using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Timers;

namespace TheJourneyAhead
{
    class InGameUI
    {
        #region Variables

        InputManager inputManager;
        ContentManager content;
        bool escMenu;
        List<EscapeMenuBuilder> escMenuBuilder;
        List<String> escItems;
        bool saving;
        bool savingText;
        String Area;
        String Gender;
        String Hair;
        int PPosX;
        int PPosY;
        int AreaPosX; 
        int AreaPosY;
        String PName;
        bool FromPortal;
        SpriteFont font;
        string dimensionsX = ScreenManager.Instance.Dimensions.X.ToString();
        string dimensionsY = ScreenManager.Instance.Dimensions.Y.ToString();
        float cameraX;
        float cameraY;
        Vector2 saveV;
        Timer timer1 = new Timer();
        SaveLoad save;
        #endregion

        #region Methods

        public void LoadContent(ContentManager Content, InputManager InputManager, String area, String gender, String hair, int pPosX, int pPosY, int areaPosX, int areaPosY, String pName, bool fromPortal)
        {
            this.inputManager = InputManager;
            savingText = false;
            content = Content;
            if (font == null)
                font = content.Load<SpriteFont>("times_new_yorker");
            Area = area;
            Gender = gender;
            Hair = hair;
            PPosX = pPosX;
            PPosY = pPosY;
            AreaPosX = areaPosX;
            AreaPosY = areaPosY;
            PName = pName;
            FromPortal = fromPortal;
            saving = false;
            escMenuBuilder = new List<EscapeMenuBuilder>();
            escItems = new List<String>();
            escMenu = false;
            escItems.Add("Save Game");
            escItems.Add("Back to Menu");
            escItems.Add("Exit Game");
            cameraX = Camera.Instance.CameraPos.X;
            cameraY = Camera.Instance.CameraPos.Y;
            for (int i = 0; i < escItems.Count; i++)
            {
                EscapeMenuBuilder esc = new EscapeMenuBuilder();
                esc.LoadContent(Content, inputManager, escItems[i], i);
                escMenuBuilder.Add(esc);
            }
            cameraX = Convert.ToInt32(cameraX);
            cameraY = Convert.ToInt32(cameraY);
            saveV = new Vector2(int.Parse(cameraX.ToString()) + (int.Parse(dimensionsX) / 2) - 50, int.Parse(cameraY.ToString()) + 100);
        }
        public void UnloadContent()
        {
            if (escMenu)
            {
                escMenuBuilder.Clear();
            }
        }
        public void Update(GameTime gameTime,int pPosX, int pPosY)
        {
            cameraX = Camera.Instance.CameraPos.X;
            cameraY = Camera.Instance.CameraPos.Y;
            dimensionsX = ScreenManager.Instance.Dimensions.X.ToString();
            dimensionsY = ScreenManager.Instance.Dimensions.Y.ToString();
            cameraX = Convert.ToInt32(cameraX);
            cameraY = Convert.ToInt32(cameraY);
            saveV = new Vector2(int.Parse(cameraX.ToString()) + (int.Parse(dimensionsX) / 2) - 50, int.Parse(cameraY.ToString()) + 100);
            PPosX = pPosX;
            PPosY = pPosY;
            if (inputManager.KeyPressed(Keys.Escape))
            {
                escMenu = !escMenu;
            }

            if (escMenu)
            {
                for (int i = 0; i < escMenuBuilder.Count; i++)
                {
                    escMenuBuilder[i].Update(gameTime, inputManager);
                    if (escMenuBuilder[i].Clicked == true)
                    {
                        

                        if (escMenuBuilder[i].Text == "Save Game")
                        {
                            saving = true;
                        }
                        if (escMenuBuilder[i].Text == "Back to Menu")
                        {
                            Camera.Instance.SetFocalPoint(new Vector2(ScreenManager.Instance.Dimensions.X / 2, ScreenManager.Instance.Dimensions.Y / 2));
                            Type newClass = Type.GetType("TheJourneyAhead.TitleScreen");
                            ScreenManager.Instance.AddScreen((Screens)Activator.CreateInstance(newClass), inputManager);
                        }
                        if (escMenuBuilder[i].Text == "Exit Game")
                        {
                            System.Environment.Exit(1);
                        }
                    }
                }
            }

            if (saving == true)
            {                              
                save = new SaveLoad();
                save.Save(Area, Gender, Hair, PPosX, PPosY, AreaPosX, AreaPosY, PName, FromPortal, 0);
                savingText = true;
                if (save.Saved == true)
                {
                    timer1.Interval = 10; //Sets timer1 interval to 1/2 second                       
                    timer1.Elapsed += new ElapsedEventHandler(timer1_Tick); //Once timer1 has finished go to event handler method below
                    timer1.Start();
                }
            }            
        }

        void timer1_Tick(object sender, EventArgs e) //timer1 event handler
        {
            timer1.Stop(); //stop timer1            
            savingText = false;
            saving = false;
        }  

        public void Draw(SpriteBatch spriteBatch)
        {
            if (savingText == true)
            {
                spriteBatch.DrawString(font, "Saving Game", saveV, Color.Yellow);
            }
            if (escMenu == true)
            {
                for (int i = 0; i < escMenuBuilder.Count; i++)
                    escMenuBuilder[i].Draw(spriteBatch);
            }
            
        }
        #endregion

    }
}
