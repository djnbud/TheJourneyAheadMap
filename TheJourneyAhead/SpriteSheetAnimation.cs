using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TheJourneyAhead
{
    public class SpriteSheetAnimation : SpriteSheetFrame
    {
        private int frameCounter;
        private int switchFrame;
        Vector2 currentFrame;
        //Class moves frame across the spritesheet stored/drawn in SpriteSheetFrame class
        //Creating animated image
        public SpriteSheetAnimation()
        {
            frameCounter = 11;
            switchFrame = 40; // speed of frame changing
        }

        public override void Update(GameTime gameTime, ref SpriteSheetFrame a)
        {
            currentFrame = a.CurrentFrame;
            if (a.Animate) //If we want spritesheet to animate
            {
                frameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (frameCounter >= switchFrame) // reset frame counter if it exceeds the switch frame number
                {
                    frameCounter = 0;
                    currentFrame.X++; // go to next frame in animation

                    if ((currentFrame.X + 1) * a.FrameWidth >= a.SpriteSheet.Width) //<--Animation problem was because currentFrame.X was a.CurrentFrame.X
                        currentFrame.X = 0;

                }
            }
            //else // reset frame counter when active is false
            //{
            //    frameCounter = 0;
            //    currentFrame.X = 11;
            //}
            a.CurrentFrame = currentFrame;
            a.SourceRect = new Rectangle((int)currentFrame.X * a.FrameWidth, (int)currentFrame.Y * a.FrameHeight,
                        a.FrameWidth, a.FrameHeight); //ensure the source rect updates no matter what happens

        }

    }
}
