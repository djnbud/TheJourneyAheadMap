using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheJourneyAhead
{
    public class TitleScreen : Screens
    {

        #region Variables

        SpriteFont font;
        MenuManager menu;

        #endregion

        #region Methods

        public override void LoadContent(ContentManager Content, InputManager inputManager)
        {
            base.LoadContent(Content, inputManager);
            if (font == null)
                font = this.content.Load<SpriteFont>("times_new_yorker");
            menu = new MenuManager();
            menu.LoadContent(content, "Title");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            menu.UnloadContent();
        }

        public override void Update(GameTime gameTime, GraphicsDeviceManager Graphics)
        {
            inputManager.Update(gameTime);
            menu.Update(gameTime, inputManager, Graphics);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            menu.Draw(spriteBatch);
        }

        #endregion


    }
}
