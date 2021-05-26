using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TheJourneyAhead
{
    public class InputManager
    {

        #region Variables
        //public static extern short GetKeyState(int keyCode);
        private static InputManager instance;
        KeyboardState prevKeyState, keyState;
        public bool leftClick = false;
        public bool rightClick = false;
        public bool moveright;
        public bool moveleft;
        public bool movejump;
        public bool text_input;
        bool shift;
        bool capslock;
        bool numlock;
        public bool key_press;
        public string keyText;
        private MouseState mouseState;
        public Point mousePos;
        public Vector2 mPos;        
        Keys[] pressedKeys;
        Keys lastPressedKey;
        TimeSpan nextRepeatTime;
        int repeatSignal;
        static TimeSpan keyDelayTime;
        static TimeSpan keyRepeatTime;
        
        #endregion

        #region Properties

        public static InputManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new InputManager();
                return instance;

            }

        }

        public bool KeyPressed(Keys key)  // checks for single key presses
        {
            if (keyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key))
                return true;
            return false;
        }

        public bool KeyPressed(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (keyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key))
                    return true;
            }
            return false;
        }

        public KeyboardState PrevKeyState
        {
            get { return prevKeyState; }
            set { prevKeyState = value; }

        }

        public KeyboardState KeyState
        {
            get { return keyState; }
            set { keyState = value; }
        }

        public MouseState _MouseState
        {
            get { return mouseState; }
            set { mouseState = value; }
        }

        public bool KeyDown(Keys key)
        {
            if (keyState.IsKeyDown(key))
                return true;
            return false;
        }

        public bool KeyDown(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (keyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }

        #endregion

        #region Methods

        public void Update(GameTime gameTime)
        {
            key_press = false;
            keyText = "";
            keyDelayTime = new TimeSpan(0, 0, 0, 0, 250 + 250);
            keyRepeatTime = new TimeSpan(0, 0, 0, 0, 33 + (400 - 33));

            Microsoft.Xna.Framework.Input.GamePadState currentState = Microsoft.Xna.Framework.Input.GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.One);
           
            
            prevKeyState = keyState;
            keyState = Keyboard.GetState(); //Updates key presses
            mouseState = Mouse.GetState(); //Updates Mouse Pos

            mousePos.X = mouseState.X;
            mousePos.Y = mouseState.Y;

            mPos.X = mouseState.X;
            mPos.Y = mouseState.Y;

            if (mouseState.LeftButton == ButtonState.Pressed && leftClick == false)
            {
                leftClick = true;
            }

            if (mouseState.LeftButton == ButtonState.Released && leftClick == true)
            {
                leftClick = false;
            }

            if (mouseState.RightButton == ButtonState.Pressed && rightClick == false)
            {
                rightClick = true;
            }

            if (mouseState.RightButton == ButtonState.Released && rightClick == true)
            {
                rightClick = false;
            }

            if (KeyDown(Keys.D, Keys.Right) || currentState.IsButtonDown(Buttons.LeftThumbstickRight) || currentState.IsButtonDown(Buttons.DPadRight))
            {
                moveright = true;
            }
            else
            {
                moveright = false;
            }

            if (KeyDown(Keys.A, Keys.Left) || currentState.IsButtonDown(Buttons.LeftThumbstickLeft) || currentState.IsButtonDown(Buttons.DPadLeft))
            {
                moveleft = true;
            }
            else
            {
                moveleft = false;
            }

            if (KeyDown(Keys.W, Keys.Up) || currentState.IsButtonDown(Buttons.A))
            {
                movejump = true;
            }
            else
            {
                movejump = false;
            }

            if (KeyDown(Keys.LeftShift, Keys.RightShift))
                shift = true;
            else
                shift = false;

            capslock = capsLock.capslockCheck();

            numlock = capsLock.numlockCheck();
            if (text_input == true)
            {
                pressedKeys = keyState.GetPressedKeys();

                for (int i = 0; i < pressedKeys.Length; i++)
                {                    
                    if (KeyPressed(pressedKeys[i]))
                    {
                        key_press = true;
                        keyText = TextInput.TranslateChar(pressedKeys[i], shift, capslock, numlock).ToString();
                        if (keyText == "\0")
                            keyText = "";
                    }
                }
            }
        }
        #endregion
    }
}
