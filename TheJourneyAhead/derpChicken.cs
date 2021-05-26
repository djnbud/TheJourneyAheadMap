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
    public class derpChicken : Creature
    {
        #region Variables
        EntityMovement entityMove;
        //Texture2D temp;
        Texture2D tCreature;
        int moveOptions;
        bool left = false;
        bool right = false;
        bool jump = false;
        bool moving = false;
        bool wait = false;
        Timer moveTime;
        Timer waitTime;
        Random Rand;
        #endregion

        #region Methods

        public override void LoadContent(ContentManager Content, InputManager inputManager, int Creature, int PosX, int PosY, Random rand)
        {
            base.LoadContent(Content, inputManager, Creature, PosX, PosY, rand);


            //temp = Content.Load<Texture2D>("DerpChicken2");
            tCreature = Content.Load<Texture2D>("Woodland/Creatures/DerpChicken2");            
            pCreature = new Vector2((PosX * 64), (PosY * 64) - ((int)tCreature.Height));
            rCreature = new Rectangle(Convert.ToInt32(pCreature.X), Convert.ToInt32(pCreature.Y), tCreature.Width, tCreature.Height);
            PCreature = pCreature;
            RCreature = rCreature;
            entityMove = new EntityMovement();
            entityMove.LoadContent();
            moveOptions = 3;
            moveTime = new Timer();
            waitTime = new Timer();
            Rand = rand;
        }

        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime, GraphicsDeviceManager Graphics)
        {
            
            velocity.Y = 0;
            if (activateGravity == true)
                velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            
            if (!moving && !wait)
            {
                int moveType = Rand.Next(moveOptions);
                if (moveType == 1)
                {
                    moving = true;
                    left = true;
                    right = false;
                    moveTime.Interval = Rand.Next(500)+1; //Sets timer1 interval to 1/2 second                       
                    moveTime.Elapsed += new ElapsedEventHandler(moveTime_Tick); //Once timer1 has finished go to event handler method below
                    moveTime.Start();
                }
                else if (moveType == 2)
                {
                    moving = true;
                    right = true;
                    left = false;
                    moveTime.Interval = Rand.Next(500)+1; //Sets timer1 interval to 1/2 second                       
                    moveTime.Elapsed += new ElapsedEventHandler(moveTime_Tick); //Once timer1 has finished go to event handler method below
                    moveTime.Start();
                }
            }
            if (wait)
            {
                moving = false;
                right = false;
                left = false;
            }
            entityMove.Update(gameTime, right, false, left, jump, 300f, 0);
            velocity.X = entityMove.velocity.X;
            pCreature += velocity;
            rCreature.X = (int)pCreature.X;
            rCreature.Y = (int)pCreature.Y; 
        }

        void moveTime_Tick(object sender, EventArgs e) //moveTime event handler
        {
            moveTime.Stop(); //stop timer1 
            wait = true;
            waitTime.Interval = Rand.Next(500)+1;
            waitTime.Elapsed += new ElapsedEventHandler(waitTime_Tick);
            waitTime.Start();
        }

        void waitTime_Tick(object sender, EventArgs e)//waitTime event handler
        {
            waitTime.Stop();
            wait = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(tCreature, rCreature, Color.White);


        }


        #endregion
    }
}
