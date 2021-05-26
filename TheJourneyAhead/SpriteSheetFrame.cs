using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace TheJourneyAhead
{
    public class SpriteSheetFrame
    {
        #region variables
        ContentManager content;
        Texture2D spriteSheet; //spriteSheet image
        Vector2 frames; //x and y of amount of frames there are to calculate width and height of each frame
        Vector2 currentFrame;
        Rectangle sourceRect;
        Vector2 origin;
        Vector2 position;
        Boolean animate; //when true then Rectangle moves across sprite sheet creating animation
        String imgLoc;
        Color imageColor;
        float scale;

        #endregion

        #region method

        public Texture2D SpriteSheet
        {
            get { return spriteSheet; }
        }

        public Rectangle SourceRect
        {
            set { sourceRect = value; }
        }

        public Vector2 Frames
        {            
            set { frames = value; } //allows user to change frames
            get { return frames; }
        }

        public Vector2 CurrentFrame
        {
            set { currentFrame = value; }
            get { return currentFrame; }
        }

        public int FrameWidth
        {
            get { return spriteSheet.Width / (int)frames.X; } // grab width of each frame depending on frame value
        }

        public int FrameHeight
        {
            get { return spriteSheet.Height / (int)frames.Y; }
        }

        public bool Animate
        {
            set { animate = value; }
            get { return animate; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Color ImageColor
        {
            get { return imageColor; }
            set { imageColor = value; }
        }

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public void LoadContent(ContentManager Content, string ImgLoc, Vector2 Position)
        {
            this.content = Content;
            imgLoc = "";
            animate = false;
            currentFrame = new Vector2(0, 0);
            position = Position;            
            imgLoc = ImgLoc;
            imageColor = Color.White;
            scale = 0f;
            spriteSheet = Content.Load<Texture2D>(imgLoc);
            
            if (spriteSheet != null && frames != Vector2.Zero)
                sourceRect = new Rectangle((int)currentFrame.X * FrameWidth, (int)currentFrame.Y * FrameHeight,
                        FrameWidth, FrameHeight);
            else
                sourceRect = new Rectangle(0, 0, spriteSheet.Width, spriteSheet.Height);
            
            
        }

        public void UnloadContent()
        {
            content.Unload();
            position = Vector2.Zero;
            sourceRect = Rectangle.Empty;
            spriteSheet = null;
        }


        public virtual void Update(GameTime gameTime, ref SpriteSheetFrame a)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (spriteSheet != null)
            {
                origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
                spriteBatch.Draw(spriteSheet, position + origin, sourceRect, imageColor, 0.0f, origin, scale, SpriteEffects.None, 0.0f);
            }
        }

        #endregion

    }
}
