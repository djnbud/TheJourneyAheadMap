using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace TheJourneyAhead
{
    public class LoadingScreen : Screens
    {
        #region Variables
        SpriteFont font;
        Rectangle progRect;
        Vector2 progPos;
        string dimensionsX = ScreenManager.Instance.Dimensions.X.ToString();
        List<Texture2D> textures; //Particle System textures
        List<Color> pcolor;
        ParticleEngine menuParticleEngine;
        String fileName;

        #endregion

        #region Methods

        public override void LoadContent(ContentManager Content, InputManager inputManager)
        {
            base.LoadContent(Content, inputManager);
            if (font == null)
                font = this.content.Load<SpriteFont>("times_new_yorker");
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
            fileName = ScreenManager.Instance.FileLoad;            
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime, GraphicsDeviceManager Graphics)
        {
            menuParticleEngine.Update(gameTime, inputManager);
            base.Update(gameTime, Graphics);
            inputManager.Update(gameTime);
            menuParticleEngine.EmitterLocation = new Vector2(progRect.X + progRect.Width, progRect.Y);
            menuParticleEngine.GenerateForMenu();

            menuParticleEngine.EmitterLocation = new Vector2(progRect.X, progRect.Y);
            menuParticleEngine.GenerateForMenu();
            SaveLoad load = new SaveLoad();
            load.Load(fileName, inputManager);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Loading Game...", progPos, Color.Red);
            menuParticleEngine.Draw(spriteBatch);
        }

        #endregion
    }
}
