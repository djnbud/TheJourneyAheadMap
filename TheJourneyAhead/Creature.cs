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
    public class Creature
    {

        #region Variables
        //Texture2D temp;
        protected Texture2D tCreature;
        protected Rectangle rCreature;
        protected Vector2 pCreature;
        protected bool activateGravity;
        protected Vector2 velocity;
        protected float gravity;

        #endregion

        public Vector2 PCreature
        {
            get { return pCreature; }
            set { pCreature = value; }
        }

        public bool ActivateGravity
        {
            get { return activateGravity; }
            set { activateGravity = value; }
        }

        public Rectangle RCreature
        {
            get { return rCreature; }
            set { rCreature = value; }
        }

        #region Methods

        public virtual void LoadContent(ContentManager Content, InputManager inputManager, int Creature, int PosX, int PosY, Random rand)
        {
            //tCreature = new List<Texture2D>();
            //pCreature = new List<Vector2>();
            //rCreature = new List<Rectangle>();

            //for (int i = 0; i < Creature.Count; i++)
            //{
            //    //temp = Content.Load<Texture2D>("DerpChicken2");
            //    tCreature.Add(Content.Load<Texture2D>("DerpChicken2"));
            //    pCreature.Add(new Vector2((PosX[i] * 64), (PosY[i] * 64) + 64 - (tCreature[i].Height)));
            //    rCreature.Add(new Rectangle(Convert.ToInt32(pCreature[i].X), Convert.ToInt32(pCreature[i].Y), tCreature[i].Width, tCreature[i].Height));


            //}

            gravity = 200f;
            velocity = Vector2.Zero;
            //activateGravity = true;
        }

        public virtual void UnloadContent()
        {
            //tCreature.Clear();
            //pCreature.Clear();
            //rCreature.Clear();
        }

        public virtual void Update(GameTime gameTime, GraphicsDeviceManager Graphics)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //for (int i = 0; i < tCreature.Count; i++)
            //{
            //    spriteBatch.Draw(tCreature[i], rCreature[i], Color.White);
                
            //}
        }


        #endregion

    }
}
