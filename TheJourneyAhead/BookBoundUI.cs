using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheJourneyAhead
{
    public class BookBoundUI
    {
        #region variables
        ContentManager content;
        InputManager inputManager;
        Texture2D tRibbon;
        Texture2D tBound;
        Rectangle rClickRibbon;
        Rectangle rRibbon;
        Rectangle rBound;
        Rectangle rSBound;
        Rectangle rIBound;
        Rectangle rEBound;
        Rectangle rABound;
        Rectangle rQBound;
        //Variables to calculate position of UI;
        string dimensionsX = ScreenManager.Instance.Dimensions.X.ToString();
        string dimensionsY = ScreenManager.Instance.Dimensions.Y.ToString();
        float cameraX;
        float cameraY;
        //Particles
        List<Texture2D> textures; //Particle System textures
        List<Color> pcolor;
        ParticleEngine menuParticleEngine;

        bool UIOpened;

        #endregion

        #region methods

        public void LoadContent(ContentManager Content, InputManager InputManager, int pPosX, int pPosY)
        {
            this.content = Content;
            this.inputManager = InputManager;
            tRibbon = Content.Load<Texture2D>("UI/bookBound/BoundRibbon");
            tBound = Content.Load<Texture2D>("UI/bookBound/BookBound");

            UIOpened = false;
            cameraX = Camera.Instance.CameraPos.X;
            cameraY = Camera.Instance.CameraPos.Y;
            cameraX = Convert.ToInt32(cameraX);
            cameraY = Convert.ToInt32(cameraY);
            //Load Ribbon
            
            rRibbon = new Rectangle(int.Parse(cameraX.ToString()), (int.Parse(cameraY.ToString()) + (int.Parse(dimensionsY) / 4)) + tRibbon.Height, tRibbon.Width, tRibbon.Height);
            rBound = new Rectangle(int.Parse(cameraX.ToString()) - tBound.Width, (int.Parse(cameraY.ToString()) + (int.Parse(dimensionsY) / 4)) + tBound.Height, tBound.Width, tBound.Height);
            rClickRibbon = new Rectangle(0, (int.Parse(dimensionsY) / 4), tRibbon.Width, tRibbon.Height);

            textures = new List<Texture2D>();
            textures.Add(content.Load<Texture2D>("gParticle"));

            pcolor = new List<Color>();
            pcolor.Add(Color.Blue);
            pcolor.Add(Color.Red);
            pcolor.Add(Color.Gold);
            pcolor.Add(Color.Green);

            menuParticleEngine = new ParticleEngine(textures, new Vector2(400, 240), pcolor);
        }

        public void UnloadContent()
        {
            content.Unload();
        }

        public void Update(GameTime gameTime, int pPosX, int pPosY)
        {
            menuParticleEngine.Update(gameTime, inputManager);

            cameraX = Camera.Instance.CameraPos.X;
            cameraY = Camera.Instance.CameraPos.Y;
            dimensionsX = ScreenManager.Instance.Dimensions.X.ToString();
            dimensionsY = ScreenManager.Instance.Dimensions.Y.ToString();
            cameraX = Convert.ToInt32(cameraX);
            cameraY = Convert.ToInt32(cameraY);
            //Calculated bookUI position
            if (!UIOpened)
            {
                rRibbon = new Rectangle(int.Parse(cameraX.ToString()), (int.Parse(cameraY.ToString()) + (int.Parse(dimensionsY) / 4)), tRibbon.Width, tRibbon.Height);
                rBound = new Rectangle(int.Parse(cameraX.ToString()) - tBound.Width, (int.Parse(cameraY.ToString()) + (int.Parse(dimensionsY) / 4)) + tBound.Height, tBound.Width, tBound.Height);
                rClickRibbon = new Rectangle(0, (int.Parse(dimensionsY) / 4), tRibbon.Width, tRibbon.Height);
            }
            if (rClickRibbon.Contains(inputManager.mousePos.X, inputManager.mousePos.Y))
            {                
                menuParticleEngine.EmitterLocation = new Vector2(rRibbon.X + (tRibbon.Width/2), rRibbon.Y);
                menuParticleEngine.GenerateForMenu();
                if (inputManager.leftClick == true)
                {
                    UIOpened = !UIOpened;
                }
            }

            if (UIOpened)
            {
                rRibbon = new Rectangle(int.Parse(cameraX.ToString()) + tBound.Width - 10, (int.Parse(cameraY.ToString()) + (int.Parse(dimensionsY) / 4)), tRibbon.Width, tRibbon.Height);
                rBound = new Rectangle(int.Parse(cameraX.ToString()), (int.Parse(cameraY.ToString()) + (int.Parse(dimensionsY) / 4)), tBound.Width, tBound.Height);
                rClickRibbon = new Rectangle(0 + tBound.Width - 10, (int.Parse(dimensionsY) / 4), tRibbon.Width, tRibbon.Height);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tRibbon, rRibbon, Color.White);
            spriteBatch.Draw(tBound, rBound, Color.White);
            menuParticleEngine.Draw(spriteBatch);
        }


        #endregion
    }
}
