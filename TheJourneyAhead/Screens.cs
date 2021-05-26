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
    //What every screen has
    public class Screens
    {

        #region Variables

        protected ContentManager content;
        protected List<List<string>> attributes, contents;
        protected InputManager inputManager;

        #endregion

        #region Methods

        public virtual void Initialise()
        {

        }

        public virtual void LoadContent(ContentManager Content, InputManager inputManager)
        {
            content = new ContentManager(Content.ServiceProvider, "Content"); //Links to Folder
            attributes = new List<List<string>>();
            contents = new List<List<string>>();
            this.inputManager = inputManager;
        }

        public virtual void UnloadContent()
        {
            content.Unload();
            inputManager = null;
            attributes.Clear();
            contents.Clear();
        }

        public virtual void Update(GameTime gameTime, GraphicsDeviceManager Graphics) { }

        public virtual void Draw(SpriteBatch spriteBatch) { }

        #endregion
    }
}
