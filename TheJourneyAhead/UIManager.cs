using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheJourneyAhead
{
    public class UIManager
    {
        #region variables
        InputManager inputManager;
        EscUI escUI;
        BookBoundUI bbUI;
        int pPosX;
        int pPosY;

        #endregion

        #region methods

        public void LoadContent(ContentManager Content, InputManager InputManager, int PPosX, int PPosY)
        {
            this.inputManager = InputManager;
            pPosX = PPosX;
            pPosY = PPosY;
            escUI = new EscUI();
            escUI.LoadContent(Content, inputManager, pPosX, pPosY);
            bbUI = new BookBoundUI();
            bbUI.LoadContent(Content, inputManager, pPosX, pPosY);
        }

        public void UnloadContent()
        {
            escUI.UnloadContent();
            bbUI.UnloadContent();
        }

        public void Update(GameTime gameTime, int PPosX, int PPosY)
        {
            pPosX = PPosX;
            pPosY = PPosY;
            escUI.Update(gameTime, pPosX, pPosY);
            bbUI.Update(gameTime, pPosX, pPosY);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            escUI.Draw(spriteBatch);
            bbUI.Draw(spriteBatch);
        }

        #endregion


    }
}
