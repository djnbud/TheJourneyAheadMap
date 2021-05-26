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
    //Class used to hold the full character customised for loading/drawing.
    public class PlayerCharacter
    {
        #region variables
        //create spritesheet to show player character creation choice
        //Head and mouth are same frame because only one type of head
        SpriteSheetFrame hairFrame, headFrame, eyesFrame, torsoFrameMale, torsoFrameFemale, armsFrame, legFrame;
        SpriteSheetAnimation ssHairAni, ssHeadAni, ssEyesAni, ssTorsoMaleAni, ssTorsoFemaleAni, ssArmsAni, ssLegsAni;
        string gender;        

        public SpriteSheetFrame HairFrame
        {
            get { return hairFrame; }
            set { hairFrame = value; }
        }

        public SpriteSheetFrame HeadFrame
        {
            get { return headFrame; }
            set { headFrame = value; }
        }

        public SpriteSheetFrame EyesFrame
        {
            get { return eyesFrame; }
            set { eyesFrame = value; }
        }

        public SpriteSheetFrame TorsoFrameMale
        {
            get { return torsoFrameMale; }
            set { torsoFrameMale = value; }
        }

        public SpriteSheetFrame TorsoFrameFemale
        {
            get { return torsoFrameFemale; }
            set { torsoFrameFemale = value; }
        }

        public SpriteSheetFrame ArmsFrame
        {
            get { return armsFrame; }
            set { armsFrame = value; }
        }

        public SpriteSheetFrame LegFrame
        {
            get { return legFrame; }
            set { legFrame = value; }
        }

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        #endregion

        public void LoadContent(ContentManager content, Vector2 pos, string _gender)
        {
            gender = _gender;
            hairFrame = new SpriteSheetFrame();
            headFrame = new SpriteSheetFrame();
            eyesFrame = new SpriteSheetFrame();
            torsoFrameMale = new SpriteSheetFrame();
            torsoFrameFemale = new SpriteSheetFrame();
            armsFrame = new SpriteSheetFrame();
            legFrame = new SpriteSheetFrame();


            hairFrame.Frames = new Vector2(9, 20);
            headFrame.Frames = new Vector2(6, 8);
            eyesFrame.Frames = new Vector2(7, 8);
            torsoFrameMale.Frames = new Vector2(6, 24);
            torsoFrameFemale.Frames = new Vector2(6, 24);
            armsFrame.Frames = new Vector2(6, 60);
            legFrame.Frames = new Vector2(8, 24);


            hairFrame.LoadContent(content, @"Player/Hair/HairOptionsGrid3", new Vector2(pos.X, pos.Y));
            headFrame.LoadContent(content, @"Player/Head/HeadOptionsGrid2", new Vector2(pos.X + 35, pos.Y + 75));
            eyesFrame.LoadContent(content, @"Player/Eyes/eyesOptionsGrid2", new Vector2(pos.X + 35, pos.Y + 70));
            torsoFrameMale.LoadContent(content, @"Player/Body/MaleTorsoGrid", new Vector2(pos.X + 68, pos.Y + 155));
            torsoFrameFemale.LoadContent(content, @"Player/Body/FemaleTorsoGrid", new Vector2(pos.X + 68, pos.Y + 155));
            armsFrame.LoadContent(content, @"Player/Arms/ArmsOptionsGrid2", new Vector2(pos.X + 40, pos.Y + 155));
            legFrame.LoadContent(content, @"Player/Legs/LegsOptionsGrid3", new Vector2(pos.X + 58, pos.Y + 215));

            hairFrame.Scale = 0.5f;
            headFrame.Scale = 0.5f;
            eyesFrame.Scale = 0.5f;
            torsoFrameMale.Scale = 1f;
            torsoFrameFemale.Scale = 1f;
            armsFrame.Scale = 1f;
            legFrame.Scale = 1f;

            torsoFrameMale.Animate = true;
            torsoFrameFemale.Animate = true;
            armsFrame.Animate = true;
            legFrame.Animate = false;

            ssHairAni = new SpriteSheetAnimation();
            ssHeadAni = new SpriteSheetAnimation();
            ssEyesAni = new SpriteSheetAnimation();            
            ssTorsoMaleAni = new SpriteSheetAnimation();
            ssTorsoFemaleAni = new SpriteSheetAnimation();
            ssArmsAni = new SpriteSheetAnimation();
            ssLegsAni = new SpriteSheetAnimation();

        }

        public void posUpdate(bool left,Vector2 pos)
        {
            if (left)
            {
                hairFrame.Position = new Vector2(pos.X, pos.Y);
                headFrame.Position = new Vector2(pos.X + 35, pos.Y + 75);
                eyesFrame.Position = new Vector2(pos.X + 35, pos.Y + 70);
                torsoFrameMale.Position = new Vector2(pos.X + 68, pos.Y + 155);
                torsoFrameFemale.Position = new Vector2(pos.X + 68, pos.Y + 155);
                armsFrame.Position = new Vector2(pos.X + 40, pos.Y + 155);
                legFrame.Position = new Vector2(pos.X + 58, pos.Y + 215);
            }
            else
            {
                hairFrame.Position = new Vector2(pos.X - 12, pos.Y);
                headFrame.Position = new Vector2(pos.X + 35, pos.Y + 75);
                eyesFrame.Position = new Vector2(pos.X + 35, pos.Y + 70);
                torsoFrameMale.Position = new Vector2(pos.X + 68, pos.Y + 155);
                torsoFrameFemale.Position = new Vector2(pos.X + 68, pos.Y + 155);
                armsFrame.Position = new Vector2(pos.X + 70, pos.Y + 155);
                legFrame.Position = new Vector2(pos.X + 55, pos.Y + 215);
            }
        }

        public void directionUpdate(bool left, Vector2 pos)
        {
            if (left)
            {
                HairFrame.CurrentFrame = new Vector2(HairFrame.CurrentFrame.X, HairFrame.CurrentFrame.Y - 10);
                HeadFrame.CurrentFrame = new Vector2(HeadFrame.CurrentFrame.X, HeadFrame.CurrentFrame.Y - 4);
                EyesFrame.CurrentFrame = new Vector2(EyesFrame.CurrentFrame.X, EyesFrame.CurrentFrame.Y - 4);
                if (gender == "Male")
                {
                    TorsoFrameMale.CurrentFrame = new Vector2(TorsoFrameMale.CurrentFrame.X, TorsoFrameMale.CurrentFrame.Y - 2);
                }
                else
                {
                    torsoFrameFemale.CurrentFrame = new Vector2(torsoFrameFemale.CurrentFrame.X, torsoFrameFemale.CurrentFrame.Y - 2);
                }
                ArmsFrame.CurrentFrame = new Vector2(ArmsFrame.CurrentFrame.X, ArmsFrame.CurrentFrame.Y - 5);
                LegFrame.CurrentFrame = new Vector2(LegFrame.CurrentFrame.X, LegFrame.CurrentFrame.Y - 2);                
            }
            else
            {
                HairFrame.CurrentFrame = new Vector2(HairFrame.CurrentFrame.X, HairFrame.CurrentFrame.Y + 10);
                HeadFrame.CurrentFrame = new Vector2(HeadFrame.CurrentFrame.X, HeadFrame.CurrentFrame.Y + 4);
                EyesFrame.CurrentFrame = new Vector2(EyesFrame.CurrentFrame.X, EyesFrame.CurrentFrame.Y + 4);
                if (gender == "Male")
                {
                    TorsoFrameMale.CurrentFrame = new Vector2(TorsoFrameMale.CurrentFrame.X, TorsoFrameMale.CurrentFrame.Y + 2);
                }
                else
                {
                    torsoFrameFemale.CurrentFrame = new Vector2(torsoFrameFemale.CurrentFrame.X, torsoFrameFemale.CurrentFrame.Y + 2);
                }
                ArmsFrame.CurrentFrame = new Vector2(ArmsFrame.CurrentFrame.X, ArmsFrame.CurrentFrame.Y + 5);
                LegFrame.CurrentFrame = new Vector2(LegFrame.CurrentFrame.X, LegFrame.CurrentFrame.Y + 2);                
            }
        }

        public void Update(GameTime gameTime)
        {
            ssHairAni.Update(gameTime, ref hairFrame);
            ssHeadAni.Update(gameTime, ref headFrame);
            ssEyesAni.Update(gameTime, ref eyesFrame);
            ssTorsoMaleAni.Update(gameTime, ref torsoFrameMale);
            ssTorsoFemaleAni.Update(gameTime, ref torsoFrameFemale);
            ssArmsAni.Update(gameTime, ref armsFrame);
            ssLegsAni.Update(gameTime, ref legFrame);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (gender == "Male")
            {
                torsoFrameMale.Draw(spriteBatch);
            }
            else
            {
                torsoFrameFemale.Draw(spriteBatch);
            }
            legFrame.Draw(spriteBatch);
            armsFrame.Draw(spriteBatch);
            headFrame.Draw(spriteBatch);
            eyesFrame.Draw(spriteBatch);
            hairFrame.Draw(spriteBatch);
        }

    }
}
