using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TheJourneyAhead
{
    class EscapeMenuBuilder
    {
        #region Variables
        ContentManager content;
        SpriteFont font;
        string dimensionsX = ScreenManager.Instance.Dimensions.X.ToString();
        string dimensionsY = ScreenManager.Instance.Dimensions.Y.ToString();
        float cameraX = Camera.Instance.CameraPos.X;
        float cameraY = Camera.Instance.CameraPos.Y;
        Texture2D escTexture;
        Rectangle escR;
        Rectangle escRChange;
        Rectangle escRCompare;
        String escText;
        Vector2 escTextV;
        Color textC;
        bool clicked;
        int count;
        //Particles
        List<Texture2D> textures; //Particle System textures
        List<Color> pcolor;
        ParticleEngine menuParticleEngine;

        #endregion

        #region Methods

        public bool Clicked //Returns if item has been clicked
        {
            get { return clicked; }
        }

        public string Text
        {
            get {return escText;}
        }
        public void LoadContent(ContentManager content, InputManager inputManager, String text, int count)
        {
            this.content = content;
            this.count = count; 
            textC = Color.Black;
            if (font == null)
                font = content.Load<SpriteFont>("times_new_yorker");
            
            escTexture = content.Load<Texture2D>(@"EscMenu/EscMenu");
            cameraX = Camera.Instance.CameraPos.X;
            cameraY = Camera.Instance.CameraPos.Y;
            dimensionsX = ScreenManager.Instance.Dimensions.X.ToString();
            dimensionsY = ScreenManager.Instance.Dimensions.Y.ToString();
            cameraX = Convert.ToInt32(cameraX);
            cameraY = Convert.ToInt32(cameraY);
            escR = new Rectangle((int.Parse(dimensionsX) / 2) - (escTexture.Width / 2),(int.Parse(dimensionsY) / 4) + (escTexture.Height * count), escTexture.Width, escTexture.Height);
            escRChange = new Rectangle(int.Parse(cameraX.ToString()) + (int.Parse(dimensionsX) / 2) - (escTexture.Width / 2), int.Parse(cameraY.ToString()) + (int.Parse(dimensionsY) / 4) + (escTexture.Height * count), escTexture.Width, escTexture.Height);
            escText = text;
            escTextV = new Vector2(escRChange.X + escRChange.Width / 4, escRChange.Y + escRChange.Height / 4);
            clicked = false;
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

        public void Update(GameTime gameTime, InputManager inputManager)
        {
            menuParticleEngine.Update(gameTime, inputManager);
            
            cameraX = Camera.Instance.CameraPos.X;
            cameraY = Camera.Instance.CameraPos.Y;
            dimensionsX = ScreenManager.Instance.Dimensions.X.ToString();
            dimensionsY = ScreenManager.Instance.Dimensions.Y.ToString();
            cameraX = Convert.ToInt32(cameraX);
            cameraY = Convert.ToInt32(cameraY);
            escRChange = new Rectangle(int.Parse(cameraX.ToString()) + (int.Parse(dimensionsX) / 2) - (escTexture.Width / 2), (int.Parse(cameraY.ToString()) + (int.Parse(dimensionsY) / 4)) + (escTexture.Height * count), escTexture.Width, escTexture.Height);
            escTextV = new Vector2(escRChange.X + escRChange.Width / 4, escRChange.Y + escRChange.Height / 4);
            //escRCompare = new Rectangle(escR.X, escR.Y, escTexture.Width, escTexture.Height);
            //Hover over Escape item
            if (escR.Contains(inputManager.mousePos.X, inputManager.mousePos.Y))
            {
                menuParticleEngine.EmitterLocation = new Vector2(escRChange.X + escRChange.Width, escRChange.Y);
                
                menuParticleEngine.GenerateForMenu();
                menuParticleEngine.EmitterLocation = new Vector2(escRChange.X, escRChange.Y);
                menuParticleEngine.GenerateForMenu();
                textC = Color.Blue;
                //if (inputManager.leftClick == true)
                //{
                if (inputManager.leftClick == true)
                {
                    clicked = true;                    
                }
                else
                {
                    clicked = false;
                }

            }
            else
                textC = Color.Black;
        }

        

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(escTexture, escRChange, Color.White);
            spriteBatch.DrawString(font, escText, escTextV, textC);
            menuParticleEngine.Draw(spriteBatch);

        }
        #endregion
    }
}
