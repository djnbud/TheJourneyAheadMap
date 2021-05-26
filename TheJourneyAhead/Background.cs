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
    public class Background
    {

        #region variables
        Texture2D tBackgroundSky;
        Vector2 vBackgroundSky;
        Rectangle rBackgroundSky;

        Texture2D tBackgroundBack;
        Vector2 vBackgroundBack;
        Rectangle rBackgroundBack;

        Texture2D tBackgroundUp;
        Vector2 vBackgroundUp;
        Rectangle rBackgroundUp;

        List<Texture2D> tLBackgroundBack;
        List<Vector2> vLBackgroundBack;
        List<Rectangle> rLBackgroundBack;

        List<Texture2D> tLBackgroundUp;
        List<Vector2> vLBackgroundUp;
        List<Rectangle> rLBackgroundUp;

        List<Texture2D> tRBackgroundBack;
        List<Vector2> vRBackgroundBack;
        List<Rectangle> rRBackgroundBack;

        List<Texture2D> tRBackgroundUp;
        List<Vector2> vRBackgroundUp;
        List<Rectangle> rRBackgroundUp;

        int Count;

        String tLocation;
        #endregion

        #region methods

        public void LoadContent(ContentManager Content, String area, int mapMaxX)
        {
            tLBackgroundBack = new List<Texture2D>();
            vLBackgroundBack = new List<Vector2>();
            rLBackgroundBack = new List<Rectangle>();

            tLBackgroundUp = new List<Texture2D>();
            vLBackgroundUp = new List<Vector2>();
            rLBackgroundUp = new List<Rectangle>();

            tRBackgroundBack = new List<Texture2D>();
            vRBackgroundBack = new List<Vector2>();
            rRBackgroundBack = new List<Rectangle>();

            tRBackgroundUp = new List<Texture2D>();
            vRBackgroundUp = new List<Vector2>();
            rRBackgroundUp = new List<Rectangle>();

            tLocation = area + @"/Backgrounds/" + area;
            //sky
            tBackgroundSky = Content.Load<Texture2D>(tLocation +  @"BackgroundSky.png");
            vBackgroundSky = new Vector2(0, 0);
            rBackgroundSky = new Rectangle(Convert.ToInt32(vBackgroundSky.X), Convert.ToInt32(vBackgroundSky.Y), tBackgroundSky.Width, tBackgroundSky.Height);
            //background next level
            tBackgroundBack = Content.Load<Texture2D>(tLocation + @"BackgroundBack.png");
            vBackgroundBack = new Vector2(0, 0);
            rBackgroundBack = new Rectangle(Convert.ToInt32(vBackgroundBack.X), Convert.ToInt32(vBackgroundBack.Y), tBackgroundBack.Width, tBackgroundBack.Height);
            //background top level
            tBackgroundUp = Content.Load<Texture2D>(tLocation + @"BackgroundUp.png");
            vBackgroundUp = new Vector2(0, 0);
            rBackgroundUp = new Rectangle(Convert.ToInt32(vBackgroundUp.X), Convert.ToInt32(vBackgroundUp.Y), tBackgroundUp.Width, tBackgroundUp.Height);

            Count = (mapMaxX / tBackgroundSky.Width) + 1;
            for (int i = 0; i < Count; i++)
            {
                //background next level
                //Left
                tLBackgroundBack.Add(Content.Load<Texture2D>(tLocation + @"BackgroundBack.png"));
                vLBackgroundBack.Add(new Vector2(0, 0));
                rLBackgroundBack.Add(new Rectangle(Convert.ToInt32(vLBackgroundBack[i].X), Convert.ToInt32(vLBackgroundBack[i].Y), tLBackgroundBack[i].Width, tLBackgroundBack[i].Height));                
                //Right
                tRBackgroundBack.Add(Content.Load<Texture2D>(tLocation + @"BackgroundBack.png"));
                vRBackgroundBack.Add(new Vector2(0, 0));
                rRBackgroundBack.Add(new Rectangle(Convert.ToInt32(vRBackgroundBack[i].X), Convert.ToInt32(vRBackgroundBack[i].Y), tRBackgroundBack[i].Width, tRBackgroundBack[i].Height));
                
                //background top level
                 //Left
                tLBackgroundUp.Add(Content.Load<Texture2D>(tLocation + @"BackgroundUp.png"));
                vLBackgroundUp.Add(new Vector2(0, 0));
                rLBackgroundUp.Add(new Rectangle(Convert.ToInt32(vLBackgroundUp[i].X), Convert.ToInt32(vLBackgroundUp[i].Y), tLBackgroundUp[i].Width, tLBackgroundUp[i].Height));
                //Right
                tRBackgroundUp.Add(Content.Load<Texture2D>(tLocation + @"BackgroundUp.png"));
                vRBackgroundUp.Add(new Vector2(0, 0));
                rRBackgroundUp.Add(new Rectangle(Convert.ToInt32(vRBackgroundUp[i].X), Convert.ToInt32(vRBackgroundUp[i].Y), tRBackgroundUp[i].Width, tRBackgroundUp[i].Height));
            }
        
        }

        public void UnloadContent()
        {
            tLBackgroundBack.Clear();
            vLBackgroundBack.Clear();
            rLBackgroundBack.Clear();

            tLBackgroundUp.Clear();
            vLBackgroundUp.Clear();
            rLBackgroundUp.Clear();

            tRBackgroundBack.Clear();
            vRBackgroundBack.Clear();
            rRBackgroundBack.Clear();

            tRBackgroundUp.Clear();
            vRBackgroundUp.Clear();
            rRBackgroundUp.Clear();
        }

        public void Update(Vector2 pVelocity, bool xCollided, bool yCollided)
        {
            //Sky
            vBackgroundSky = Camera.Instance.CameraPos;
            rBackgroundSky.X = Convert.ToInt32(vBackgroundSky.X);
            rBackgroundSky.Y = Convert.ToInt32(vBackgroundSky.Y);
            //Back
            if (!xCollided)
                vBackgroundBack.X -= pVelocity.X / 10;
            if(!yCollided)
                vBackgroundBack.Y += pVelocity.Y / 2;
            rBackgroundBack.X = Convert.ToInt32(vBackgroundBack.X);
            rBackgroundBack.Y = Convert.ToInt32(vBackgroundBack.Y);
            //Up
            if (!xCollided)
                vBackgroundUp -= pVelocity / 7;
            rBackgroundUp.X = Convert.ToInt32(vBackgroundUp.X);
            for (int i = 0; i < Count; i++)
            {
                //BackLeft
                vLBackgroundBack[i] = new Vector2(vBackgroundBack.X - (tBackgroundBack.Width * (i + 1)), vBackgroundBack.Y);
                rLBackgroundBack[i] = new Rectangle(Convert.ToInt32(vLBackgroundBack[i].X), Convert.ToInt32(vLBackgroundBack[i].Y), rLBackgroundBack[i].Width, rLBackgroundBack[i].Height);
                //BackRight
                vRBackgroundBack[i] = new Vector2(vBackgroundBack.X + (tBackgroundBack.Width * (i + 1)), vBackgroundBack.Y);
                rRBackgroundBack[i] = new Rectangle(Convert.ToInt32(vRBackgroundBack[i].X), Convert.ToInt32(vRBackgroundBack[i].Y), rRBackgroundBack[i].Width, rRBackgroundBack[i].Height);

                //UpLeft
                vLBackgroundUp[i] = new Vector2(vBackgroundUp.X - (tBackgroundUp.Width * (i + 1)), vLBackgroundUp[i].Y);
                rLBackgroundUp[i] = new Rectangle(Convert.ToInt32(vLBackgroundUp[i].X), Convert.ToInt32(vLBackgroundUp[i].Y), rLBackgroundUp[i].Width, rLBackgroundUp[i].Height);
                //UpRight
                vRBackgroundUp[i] = new Vector2(vBackgroundUp.X + (tBackgroundUp.Width * (i + 1)), vRBackgroundUp[i].Y);
                rRBackgroundUp[i] = new Rectangle(Convert.ToInt32(vRBackgroundUp[i].X), Convert.ToInt32(vRBackgroundUp[i].Y), rRBackgroundUp[i].Width, rRBackgroundUp[i].Height);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects se = SpriteEffects.FlipHorizontally; 

            spriteBatch.Draw(tBackgroundSky, rBackgroundSky, Color.White);
            spriteBatch.Draw(tBackgroundBack, rBackgroundBack, Color.White);
            for (int i = 0; i < Count; i++)
            {
                if (i % 2 == 0)
                {
                    spriteBatch.Draw(tRBackgroundBack[i], rRBackgroundBack[i], null, Color.White, 0.0f, new Vector2(0, 0), se, 0);
                    spriteBatch.Draw(tLBackgroundBack[i], rLBackgroundBack[i], null, Color.White, 0.0f, new Vector2(0, 0), se, 0);
                }
                else
                {
                    spriteBatch.Draw(tRBackgroundBack[i], rRBackgroundBack[i], Color.White);
                    spriteBatch.Draw(tLBackgroundBack[i], rLBackgroundBack[i], Color.White);
                }
            }
            spriteBatch.Draw(tBackgroundUp, rBackgroundUp, Color.White);
            for (int i = 0; i < Count; i++)
            {
                if (i % 2 == 0)
                {
                    spriteBatch.Draw(tRBackgroundUp[i], rRBackgroundUp[i], null, Color.White, 0.0f, new Vector2(0, 0), se, 0);
                    spriteBatch.Draw(tLBackgroundUp[i], rLBackgroundUp[i], null, Color.White, 0.0f, new Vector2(0, 0), se, 0);
                }
                else
                {
                    spriteBatch.Draw(tRBackgroundUp[i], rRBackgroundUp[i], Color.White);
                    spriteBatch.Draw(tLBackgroundUp[i], rLBackgroundUp[i], Color.White);
                }
            }
        }
        #endregion

    }
}
