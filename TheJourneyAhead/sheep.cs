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
    public class sheep : Creature
    {
        #region Variables
        //Texture2D temp;
        Texture2D tCreature;
        

        #endregion

        #region Methods

        public override void LoadContent(ContentManager Content, InputManager inputManager, int Creature, int PosX, int PosY, Random rand)
        {
            base.LoadContent(Content, inputManager, Creature, PosX, PosY, rand);


             
                //temp = Content.Load<Texture2D>("DerpChicken2");
            tCreature = Content.Load<Texture2D>("Woodland/Creatures/Sheep");
                pCreature = new Vector2((PosX * 64), (PosY * 64) + 64 - (tCreature.Height));
                rCreature = new Rectangle(Convert.ToInt32(pCreature.X), Convert.ToInt32(pCreature.Y), tCreature.Width, tCreature.Height);
                PCreature = pCreature;
                RCreature = rCreature;
            
        }

        public override void UnloadContent()
        {
            
        }

        public override void Update(GameTime gameTime, GraphicsDeviceManager Graphics)
        {
            velocity.Y = 0;
            if (activateGravity == true)
                velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;


            pCreature += velocity;
            rCreature.X = (int)pCreature.X;
            rCreature.Y = (int)pCreature.Y; 
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
                spriteBatch.Draw(tCreature, rCreature, Color.White);

            
        }


        #endregion

    }
}
