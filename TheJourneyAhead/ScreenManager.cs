using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TheJourneyAhead
{
    //Manages screen player is currently on and where they can go next

    public class ScreenManager
    {
        #region Variables

        ContentManager content;
        Screens currentScreen;
        TitleScreen titleScreen; //Starts up Title Screen first
        public SpriteFont Font;
        Texture2D nullImage;
        Screens newScreen;
        bool transition;
        Vector2 dimensions;
        InputManager inputManager;
        String fileLoad;

        //The static reference to the current instance of this class
        private static ScreenManager instance;

        Stack<Screens> screenStack = new Stack<Screens>();

        #endregion

        #region Properties        

        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScreenManager();
                }
                return instance;
            }
        }

        public ContentManager Content
        {
            get { return content; }
        }

        public Vector2 Dimensions //Holds the Dimensions of the screen
        {
            get { return dimensions; }
            set { dimensions = value; }
        }

        public Texture2D NullImage
        {
            get { return nullImage; }
        }

        public String FileLoad
        {
            get { return fileLoad; }
            set { fileLoad = value; }
        }

        #endregion

        #region Methods

        public void AddScreen(Screens screen, InputManager inputManager)
        {
            transition = true;
            newScreen = screen;
            this.inputManager = inputManager;
        }

        public void Initialise()
        {
            currentScreen = new TitleScreen();
            inputManager = InputManager.Instance;
        }

        public void LoadContent(ContentManager Content)
        {
            content = new ContentManager(Content.ServiceProvider, "Content");
            currentScreen.LoadContent(Content, inputManager);
            nullImage = this.content.Load<Texture2D>("null");
        }

        public void Update(GameTime gameTime, GraphicsDeviceManager Graphics)
        {
            if (!transition)
                currentScreen.Update(gameTime, Graphics);
            else
                Transition(gameTime);
            Camera.Instance.Update();
            
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            currentScreen.Draw(spriteBatch);
        }
        #endregion

        #region privateMethods

        private void Transition(GameTime gameTime)
        {
            screenStack.Push(newScreen);
            currentScreen.UnloadContent();
            currentScreen = newScreen;
            currentScreen.LoadContent(content, this.inputManager);
            transition = false;
        }


        #endregion
    }
}
