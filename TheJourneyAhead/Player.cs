using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Timers;

namespace TheJourneyAhead
{
    public class Player
    {
        #region Variables

        EntityMovement entityMove;
        //Texture2D tPlayer;
        //SpriteSheetFrame hairFrame;
        //SpriteSheetAnimation ssHairAnimation;
        PlayerCharacter playerChar;
        protected Rectangle rPlayer;
        protected Vector2 pPlayer;
        protected Vector2 pHair;
        protected bool activateGravity;
        protected float gravity;
        protected Vector2 velocity;
        bool left = false;
        bool right = false;
        bool jump = false;
        bool leftFacing = true;
        bool xCollided = false;
        bool yCollided = false;
        float jumpSpeed = 0f;
        bool startJump;
        bool landed;
        Timer timer1;
        int width;
        int height;
       // Rectangle rp2;
        #endregion

        public Vector2 PPlayer
        {
            get { return pPlayer; }
            set { pPlayer = value; }
        }

        public bool ActivateGravity
        {
            get { return activateGravity; }
            set { activateGravity = value; }
        }

        public bool XCollided
        {
            get { return xCollided; }
            set { xCollided = value; }
        }

        public bool YCollided
        {
            get { return yCollided; }
            set { yCollided = value; }
        }

        public Rectangle RPlayer
        {
            get { return rPlayer; }
            set { rPlayer = value; }
        }
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        #region Methods

        public void LoadContent(ContentManager Content, InputManager inputManager, Vector2 Pos)
        {
            pPlayer = Pos;
            playerChar = new PlayerCharacter();
            playerChar.LoadContent(Content, pPlayer, PlayerDetails.Instance.Gender);
            playerChar.HairFrame.CurrentFrame = new Vector2(PlayerDetails.Instance.Hair, PlayerDetails.Instance.HairColour);
            playerChar.HeadFrame.CurrentFrame = new Vector2(PlayerDetails.Instance.Skin, PlayerDetails.Instance.Head);
            playerChar.EyesFrame.CurrentFrame = new Vector2(PlayerDetails.Instance.EyeColour, PlayerDetails.Instance.Eye);
            playerChar.TorsoFrameMale.CurrentFrame = new Vector2(playerChar.TorsoFrameMale.CurrentFrame.X, PlayerDetails.Instance.Skin * 4);
            playerChar.TorsoFrameFemale.CurrentFrame = new Vector2(playerChar.TorsoFrameFemale.CurrentFrame.X, PlayerDetails.Instance.Skin * 4);
            playerChar.ArmsFrame.CurrentFrame = new Vector2(playerChar.ArmsFrame.CurrentFrame.X, PlayerDetails.Instance.Skin * 10);
            playerChar.LegFrame.CurrentFrame = new Vector2(playerChar.LegFrame.CurrentFrame.X, PlayerDetails.Instance.Skin * 4);

            width = playerChar.LegFrame.FrameWidth;
            height = playerChar.TorsoFrameMale.FrameHeight + playerChar.HeadFrame.FrameHeight + playerChar.LegFrame.FrameHeight;
            
            //tPlayer = Content.Load<Texture2D>(@"Player/Body/" + PlayerDetails.Instance.Gender);
            //tPlayer = Content.Load<Texture2D>("Tile1");
            //hairFrame = new SpriteSheetFrame();
            //hairFrame.Frames = new Vector2(9, 4);            
            //ssHairAnimation = new SpriteSheetAnimation();
            //rp2 = new Rectangle((int)pPlayer.X, (int)pPlayer.Y, width, height / 2);
            //pHair = new Vector2(Pos.X - (tPlayer.Width / 2 - 5), pPlayer.Y - (tPlayer.Height/2 - 15));
            //hairFrame.LoadContent(Content, @"Player/Hair/HairOptionsGrid", pHair);            
            //hairFrame.Scale = 0.5f;
            //hairFrame.CurrentFrame = new Vector2(PlayerDetails.Instance.Hair, PlayerDetails.Instance.HairColour);
            //rPlayer = new Rectangle(Convert.ToInt32(pPlayer.X), Convert.ToInt32(pPlayer.Y), tPlayer.Width/2, tPlayer.Height/2); 
            rPlayer = new Rectangle((int)pPlayer.X, (int)pPlayer.Y, width, height / 2);  
            startJump = false;
            gravity = 250f;
            velocity = Vector2.Zero;
            entityMove = new EntityMovement();
            entityMove.LoadContent();
            landed = false;
            timer1 = new Timer();
        }

        public void UnloadContent()
        {

        }
              

        public void Update(GameTime gameTime, GraphicsDeviceManager Graphics, InputManager input)
        {
                 
            if (input.moveright == true)
            {
                if (leftFacing)
                {
                    leftFacing = false;
                    playerChar.directionUpdate(false, pPlayer);
                }
                right = true;
                //state = GameState.Right;
            }
            else
                right = false;
            if (input.moveleft == true)
            {
                if (!leftFacing)
                {
                    leftFacing = true;
                    playerChar.directionUpdate(true, pPlayer);
                }
                left = true;
                //state = GameState.Left;
            }
            else
                left = false;

            if (input.movejump == true && !activateGravity && landed == true)
            {
                jump = true;
                jumpSpeed = -30;//550f; -23
                activateGravity = true;
                startJump = true;
                gravity = 250f;
                landed = false;
            }

            //if (input.movejump == false)
            //{
            //    jump = false;
            //    jumpSpeed = 0;
            //}

            entityMove.Update(gameTime, right, startJump, left, jump, 300f, jumpSpeed);
            startJump = entityMove.startJump;
            jump = entityMove.jump;
            velocity = entityMove.velocity;
            if (activateGravity == true)
            {
                velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds; 
               // velocity.Y += gravity;
                gravity += 5;

                
            }
            else
                velocity.Y = 0;

            if (landed == false)
            {
                timer1.Interval = 10; //Sets timer1 interval to 1/2 second                       
                timer1.Elapsed += new ElapsedEventHandler(timer1_Tick); //Once timer1 has finished go to event handler method below
                timer1.Start();
            }

            pPlayer += velocity;
            playerChar.posUpdate(leftFacing,new Vector2(pPlayer.X - width, pPlayer.Y - (height/2 + 15)));
            //pHair = new Vector2(pPlayer.X - (tPlayer.Width/2 - 5), pPlayer.Y - (tPlayer.Height/2 - 15));
            rPlayer.X = (int)pPlayer.X;
            rPlayer.Y = (int)pPlayer.Y;
            //rp2.X = (int)pPlayer.X;
            //rp2.Y = (int)pPlayer.Y;
            Camera.Instance.SetFocalPoint(new Vector2(pPlayer.X, pPlayer.Y));//ScreenManager.Instance.Dimensions.Y / 2));
            
            playerChar.Update(gameTime);
            //hairFrame.Position = pHair;
            //ssHairAnimation.Update(gameTime, ref hairFrame);
        }

        void timer1_Tick(object sender, EventArgs e) //timer1 event handler
        {
            timer1.Stop(); //stop timer1            
            landed = true;
        }  

        public void Draw(SpriteBatch spriteBatch)
        {
            playerChar.Draw(spriteBatch);
            //spriteBatch.Draw(tPlayer, rPlayer, Color.White);
            //hairFrame.Draw(spriteBatch);
            //spriteBatch.Draw(tPlayer, rp2, Color.White);
        }


        #endregion


    }
}
