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
    public class NewGameScreen : Screens
    {
        #region Variables

        SpriteFont font;
        
        MenuManager menu; //For Screen Navigation such as Back, and Options       

        CustomiseMenuManager customMenu; //For customise options
        #endregion

        #region Methods

        public override void LoadContent(ContentManager Content, InputManager inputManager)
        {
            base.LoadContent(Content, inputManager);
            if (font == null)
                font = this.content.Load<SpriteFont>("times_new_yorker");
            menu = new MenuManager();
            menu.LoadContent(content, "NewGameScreen");
            
            
            

            
                customMenu = new CustomiseMenuManager();
                customMenu.LoadContent(content);
            
            //rArrowRect.Add(new Rectangle(rArrow.Width,rArrow.Height));

        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            menu.UnloadContent();
            customMenu.UnloadContent();
        }

        public override void Update(GameTime gameTime, GraphicsDeviceManager Graphics)
        {
            base.Update(gameTime,Graphics);
            inputManager.Update(gameTime);
            menu.Update(gameTime, inputManager, Graphics);
            customMenu.Update(gameTime, inputManager, Graphics);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            
            customMenu.Draw(spriteBatch);
            menu.Draw(spriteBatch);
        }

        #endregion

    }
}
