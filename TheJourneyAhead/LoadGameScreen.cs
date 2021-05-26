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
    public class LoadGameScreen : Screens
    {
        #region Variables

        SpriteFont font;
        MenuManager menu;
        ListedSaves lSaves;
        //IAsyncResult result;
        //StorageDevice device;
        //bool GameLoadRequested;
        #endregion

        #region Methods

        public override void LoadContent(ContentManager Content, InputManager inputManager)
        {
            //GameLoadRequested = true;
            base.LoadContent(Content, inputManager);
            if (font == null)
                font = this.content.Load<SpriteFont>("times_new_yorker");
            menu = new MenuManager();
            menu.LoadContent(content, "LoadGameScreen");
            lSaves = new ListedSaves();           

            lSaves.LoadContent(Content);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            menu.UnloadContent();
            lSaves.UnloadContent();
        }

        public override void Update(GameTime gameTime, GraphicsDeviceManager Graphics)
        {
            base.Update(gameTime, Graphics);
            inputManager.Update(gameTime);
            menu.Update(gameTime, inputManager, Graphics);
            lSaves.Update(gameTime, inputManager);

            //result = StorageDevice.BeginShowSelector(
            //                PlayerIndex.One, null, null);
            //if ((GameLoadRequested) && (result.IsCompleted))
            //{
            //    device = StorageDevice.EndShowSelector(result);
            //    if (device != null && device.IsConnected)
            //    {
            //        lSaves.DoEnumerate(device);
            //        GameLoadRequested = false;
            //    }
            //}
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            menu.Draw(spriteBatch);
            lSaves.Draw(spriteBatch);
        }

        #endregion

    }
}
