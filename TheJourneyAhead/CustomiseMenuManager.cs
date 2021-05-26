using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
namespace TheJourneyAhead
{
    public class CustomiseMenuManager
    {
        #region Variables

        ContentManager content;
        List<Color> fontColor;
        Vector2 aChoicePos = new Vector2 (350, 220);
        Vector2 gChoicePos = new Vector2(350, 290);
        Vector2 hChoicePos = new Vector2(350, 360);
        Vector2 hCChoicePos = new Vector2(350, 430);
        Vector2 headChoicePos = new Vector2(350, 500);

        Vector2 skinChoicePos = new Vector2(850, 220);
        Vector2 eyeChoicePos = new Vector2(850, 290);
        Vector2 eyeCChoicePos = new Vector2(850, 360);

        Vector2 nameTextPos = new Vector2(350, 610);
        Vector2 chosenNamePos = new Vector2(350, 660);
        Texture2D leftArrow;
        Texture2D rightArrow;
        List<Vector2> leftPos;
        List<Vector2> rightPos;
        List<Rectangle> leftRect;
        List<Rectangle> rightRect;

        List<Vector2> customTypePos;

        Rectangle create;
        SpriteFont font; //Can be list but currently only one font will be used
        List<string> customType;        
        
        int aChange = 0;
        int gChange = 0;
        int hChange = 0;
        int hCChange = 0;
        int headChange = 0;
        int skinChange = 0;
        int eyeChange = 0;
        int eyeCChange = 0;

        List<string> areaChoice, genderChoice, hairChoice, hairColorChoice, headChoice, skinChoice, eyesChoice, eyeColorChoice;
        
        Vector2 position;
        Vector2 imagePosition;

        List<Texture2D> textures; //Particle System textures
        List<Color> pcolor;
        ParticleEngine menuParticleEngine;        
        Texture2D textureRect; //For rectangles of fonts/Images

        string dimensionsX = ScreenManager.Instance.Dimensions.X.ToString();
        string dimensionsY = ScreenManager.Instance.Dimensions.Y.ToString();

        string pName;

        Timer timer1 = new Timer();
        bool ready;
        String pInput;
        
        FileManager fileManager;

        PlayerCharacter playerChar;        

        List<int> skinColour;

        private static CustomiseMenuManager instance;

        public static CustomiseMenuManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CustomiseMenuManager();
                }
                return instance;
            }
        }

        #endregion

        #region Methods

        public void LoadContent(ContentManager content)
        {
            this.content = new ContentManager(content.ServiceProvider, "Content");
            //Load player creation choice
            //hair animation grids
            playerChar = new PlayerCharacter();
            playerChar.LoadContent(content, new Vector2(600, 150), "Male");            

            //sets the first default options
            PlayerDetails.Instance.Area = "Woodland";
            PlayerDetails.Instance.Gender = "Male";
            PlayerDetails.Instance.Hair = 0;
            PlayerDetails.Instance.HairColour = 1;
            PlayerDetails.Instance.Skin = 0;
            PlayerDetails.Instance.Head = 0;
            PlayerDetails.Instance.Eye = 0;
            PlayerDetails.Instance.EyeColour = 0;
            PlayerDetails.Instance.PPos = new Vector2 (70,500);
            PlayerDetails.Instance.AreaPosX = 1;
            PlayerDetails.Instance.AreaPosY = 1;
            PlayerDetails.Instance.PName = "Name";
            PlayerDetails.Instance.FromPortal = false;
            PlayerDetails.Instance.PortalID = 0;
            PlayerDetails.Instance.NewGame = true;
            PlayerDetails.Instance.Loaded = false;
            //fileManager = new FileManager();
            //fileManager.WriteContent("Load/Char.jo", "Woodland" + " " + "Male" + " " + "HairStyle1" + " " + "70" + " " + "500" + " " + "1" + " " + "1" + " " + "Name" + " " + false + " " + 0);
            //                                        //area              gender         hair                 starting x    starting y    area x      area y     name      spawn at portal    portal id
           
            if (font == null)
                font = this.content.Load<SpriteFont>("times_new_yorker");

            customType = new List<string>();

            customType.Add("Starting Area");
            customType.Add("Gender");
            customType.Add("Hair Style");
            customType.Add("Hair Colour");
            customType.Add("Head");
            customType.Add("Skin Colour");
            customType.Add("Eyes");
            customType.Add("Eye Colour");

            customTypePos = new List<Vector2>();

            customTypePos.Add(new Vector2(350, 190));
            customTypePos.Add(new Vector2(350, 260));
            customTypePos.Add(new Vector2(350, 330));
            customTypePos.Add(new Vector2(350, 400));
            customTypePos.Add(new Vector2(350, 470));
            customTypePos.Add(new Vector2(850, 190));
            customTypePos.Add(new Vector2(850, 260));
            customTypePos.Add(new Vector2(850, 330));

            areaChoice = new List<string>();

            areaChoice.Add("Woodland");
            areaChoice.Add("Desert");
            areaChoice.Add("Plains");
            areaChoice.Add("Hills");
            areaChoice.Add("Snow");

            genderChoice = new List<string>();

            genderChoice.Add("Male");
            genderChoice.Add("Female");

            hairChoice = new List<string>();
            for (int i = 0; i < int.Parse(playerChar.HairFrame.Frames.X.ToString()); i++)
            {
                hairChoice.Add("Hair Style " + i.ToString());
            }

            hairColorChoice = new List<string>();

            hairColorChoice.Add("White");
            hairColorChoice.Add("");
            hairColorChoice.Add("Red");
            hairColorChoice.Add("");
            hairColorChoice.Add("Orange");
            hairColorChoice.Add("");
            hairColorChoice.Add("Yellow");
            hairColorChoice.Add("");
            hairColorChoice.Add("Brown");

            headChoice = new List<string>();
            for (int i = 0; i < int.Parse((playerChar.HeadFrame.Frames.Y/2).ToString()); i++)
            {
                headChoice.Add("Head " + i.ToString());
            }

            eyeColorChoice = new List<string>();

            eyeColorChoice.Add("Black");
            eyeColorChoice.Add("Blue");
            eyeColorChoice.Add("Brown");
            eyeColorChoice.Add("Green");
            eyeColorChoice.Add("Grey");
            eyeColorChoice.Add("Red");

            eyesChoice = new List<string>();

            for (int i = 0; i < int.Parse((playerChar.EyesFrame.Frames.Y/2).ToString()); i++)
            {
                eyesChoice.Add("Eyes " + i.ToString());
            }

                skinChoice = new List<string>();
                for (int i = 0; i < int.Parse(playerChar.HeadFrame.Frames.X.ToString()); i++)
            {
                skinChoice.Add("Skin " + i.ToString());
            }

            leftArrow = content.Load<Texture2D>("lArrow");
            rightArrow = content.Load<Texture2D>("rArrow");

            leftPos = new List<Vector2>();

            leftPos.Add(new Vector2(300, 220));
            leftPos.Add(new Vector2(300, 290));
            leftPos.Add(new Vector2(300, 360));
            leftPos.Add(new Vector2(300, 430));
            leftPos.Add(new Vector2(300, 500));
            leftPos.Add(new Vector2(800, 220));
            leftPos.Add(new Vector2(800, 290));
            leftPos.Add(new Vector2(800, 360));

            rightPos = new List<Vector2>();

            rightPos.Add(new Vector2(500, 220));
            rightPos.Add(new Vector2(500, 290));
            rightPos.Add(new Vector2(500, 360));
            rightPos.Add(new Vector2(500, 430));
            rightPos.Add(new Vector2(500, 500));
            rightPos.Add(new Vector2(1000, 220));
            rightPos.Add(new Vector2(1000, 290));
            rightPos.Add(new Vector2(1000, 360));

            leftRect = new List<Rectangle>();

            leftRect.Add(new Rectangle(300, 220, 25, 25));
            leftRect.Add(new Rectangle(300, 290, 25, 25));
            leftRect.Add(new Rectangle(300, 360, 25, 25));
            leftRect.Add(new Rectangle(300, 430, 25, 25));
            leftRect.Add(new Rectangle(300, 500, 25, 25));
            leftRect.Add(new Rectangle(800, 220, 25, 25));
            leftRect.Add(new Rectangle(800, 290, 25, 25));
            leftRect.Add(new Rectangle(800, 360, 25, 25));

            rightRect = new List<Rectangle>();

            rightRect.Add(new Rectangle(500, 220, 25, 25));
            rightRect.Add(new Rectangle(500, 290, 25, 25));
            rightRect.Add(new Rectangle(500, 360, 25, 25));
            rightRect.Add(new Rectangle(500, 430, 25, 25));
            rightRect.Add(new Rectangle(500, 500, 25, 25));
            rightRect.Add(new Rectangle(1000, 220, 25, 25));
            rightRect.Add(new Rectangle(1000, 290, 25, 25));
            rightRect.Add(new Rectangle(1000, 360, 25, 25));

            fontColor = new List<Color>();

            fontColor.Add(Color.Yellow);
            fontColor.Add(Color.Yellow);
            fontColor.Add(Color.Yellow);
            fontColor.Add(Color.Yellow);
            fontColor.Add(Color.Yellow);
            fontColor.Add(Color.Yellow);
            fontColor.Add(Color.Yellow);
            fontColor.Add(Color.Yellow);
            fontColor.Add(Color.Yellow);

            imagePosition = Vector2.Zero;
            position = Vector2.Zero;

            textures = new List<Texture2D>();
            textures.Add(content.Load<Texture2D>("gParticle"));

            pcolor = new List<Color>();
            pcolor.Add(Color.Blue);
            pcolor.Add(Color.Red);
            pcolor.Add(Color.Gold);
            pcolor.Add(Color.Green);

            menuParticleEngine = new ParticleEngine(textures, new Vector2(400, 240), pcolor);
            textureRect = content.Load<Texture2D>("Start");

            pName = "";
            create = new Rectangle(583, 490, 200, 35);
        }

        

        public void UnloadContent()
        {
            content.Unload();
            areaChoice.Clear();
            genderChoice.Clear();
            hairChoice.Clear();
            hairColorChoice.Clear();
            headChoice.Clear();
            eyesChoice.Clear();
            eyeColorChoice.Clear();
            skinChoice.Clear();            
        }

        public void Update(GameTime gameTime, InputManager inputManager, GraphicsDeviceManager Graphics)
        {
            
            menuParticleEngine.Update(gameTime, inputManager);
            
            inputManager.text_input = true;
            if (inputManager.KeyPressed(Keys.Back))
            {
                if(pName.Length>0)
                pName = pName.Remove(pName.Length - 1);
            }
            else
            if (inputManager.key_press == true)
            {
                pInput = inputManager.keyText;
                if (pName.Length < 10)
                {                    
                    pName = pName + pInput;
                }
            }

            if (create.Contains(inputManager.mousePos.X, inputManager.mousePos.Y))
            {
                PlayerDetails.Instance.Area = areaChoice[aChange];
                PlayerDetails.Instance.Gender = genderChoice[gChange];
                PlayerDetails.Instance.Hair = hChange;
                PlayerDetails.Instance.HairColour = hCChange;
                PlayerDetails.Instance.Skin = skinChange;
                PlayerDetails.Instance.Head = headChange;
                PlayerDetails.Instance.Eye = eyeChange;
                PlayerDetails.Instance.EyeColour = eyeCChange;
                PlayerDetails.Instance.PPos = new Vector2(134, 300);
                PlayerDetails.Instance.AreaPosX = 1;
                PlayerDetails.Instance.AreaPosY = 1;
                PlayerDetails.Instance.PName = pName;
                PlayerDetails.Instance.FromPortal = false;
                PlayerDetails.Instance.PortalID = 0;
                PlayerDetails.Instance.NewGame = true;
                PlayerDetails.Instance.Loaded = false;
            }
            //Was attempting to have the user just type name so no need to click in text box here but need to create 
            //seperate class for text input

            for (int i = 0; i < leftRect.Count; i++)
            {
                if (leftRect[i].Contains(inputManager.mousePos.X, inputManager.mousePos.Y))
                {
                    //fontColor[i] = Color.Blue;
                    menuParticleEngine.EmitterLocation = new Vector2(leftRect[i].X + leftRect[i].Width, leftRect[i].Y);

                    menuParticleEngine.GenerateForMenu();
                    
                    if (inputManager.leftClick == true && ready == false)
                    {
                        ready = true;
                        timer1.Interval = 500; //Sets timer1 interval to 1/2 secondc
                        
                        if (i == 0)
                        {
                            if (aChange == 0)
                            {
                                aChange = areaChoice.Count - 1;
                            }
                            else
                            aChange -= 1;
                        }

                        if (i == 1)
                        {
                            if (gChange == 0)
                            {
                                gChange = genderChoice.Count - 1;
                            }
                            else
                            gChange -= 1;
                        }

                        if (i == 2)
                        {
                            if (hChange == 0)
                            {
                                hChange = hairChoice.Count - 1;
                            }
                            else
                            hChange -= 1;
                        }

                        if (i == 3)
                        {
                            if (hCChange == 0)
                            {
                                hCChange = hairColorChoice.Count - 1;
                            }
                            else
                                hCChange -= 2;
                        }

                        if (i == 4)
                        {
                            if (headChange == 0)
                            {
                                headChange = headChoice.Count - 1;
                            }
                            else
                                headChange -= 1;
                        }

                        if (i == 5)
                        {
                            if (skinChange == 0)
                            {
                                skinChange = skinChoice.Count - 1;
                            }
                            else
                                skinChange -= 1;
                        }

                        if (i == 6)
                        {
                            if (eyeChange == 0)
                            {
                                eyeChange = eyesChoice.Count - 1;
                            }
                            else
                                eyeChange -= 1;
                        }

                        if (i == 7)
                        {
                            if (eyeCChange == 0)
                            {
                                eyeCChange = eyeColorChoice.Count - 1;
                            }
                            else
                                eyeCChange -= 1;
                        }
                    }

                    if (ready == true)
                    {
                        timer1.Elapsed += new ElapsedEventHandler(timer1_Tick); //Once timer1 has finished go to event handler method below

                        timer1.Start();
                    }
                }
                else
                    fontColor[i] = Color.Yellow;
            }
            

            for (int i = 0; i < rightRect.Count; i++)
            {
                if (rightRect[i].Contains(inputManager.mousePos.X, inputManager.mousePos.Y))
                {
                    //fontColor[i] = Color.Blue;
                    menuParticleEngine.EmitterLocation = new Vector2(rightRect[i].X + rightRect[i].Width, rightRect[i].Y);

                    menuParticleEngine.GenerateForMenu();
                    
                    if (inputManager.leftClick == true && ready == false)
                    {
                        ready = true;
                        timer1.Interval = 500; //Sets timer1 interval to 1/2 secondc
                        if (i == 0)
                        {
                            if (aChange == areaChoice.Count - 1)
                            {
                                aChange = 0;
                            }
                            else
                                aChange += 1;
                        }

                        if (i == 1)
                        {
                            if (gChange == genderChoice.Count - 1)
                            {
                                gChange = 0;
                            }
                            else
                                gChange += 1;
                        }

                        if (i == 2)
                        {
                            if (hChange == hairChoice.Count -1)
                            {
                                hChange = 0;
                            }
                            else
                                hChange += 1;
                        }

                        if (i == 3)
                        {
                            if (hCChange == hairColorChoice.Count - 1)
                            {
                                hCChange = 0;
                            }
                            else
                                hCChange += 2;
                        }

                        if (i == 4)
                        {
                            if (headChange == headChoice.Count - 1)
                            {
                                headChange = 0;
                            }
                            else
                                headChange += 1;
                        }

                        if (i == 5)
                        {
                            if (skinChange == skinChoice.Count - 1)
                            {
                                skinChange = 0;
                            }
                            else
                                skinChange += 1;
                        }

                        if (i == 6)
                        {
                            if (eyeChange == eyesChoice.Count - 1)
                            {
                                eyeChange = 0;
                            }
                            else
                                eyeChange += 1;
                        }

                        if (i == 7)
                        {
                            if (eyeCChange == eyeColorChoice.Count - 1)
                            {
                                eyeCChange = 0;
                            }
                            else
                                eyeCChange += 1;
                        }
                        
                    }
                    if (ready == true)
                    {
                        timer1.Elapsed += new ElapsedEventHandler(timer1_Tick); //Once timer1 has finished go to event handler method below

                        timer1.Start();
                    }
                    
                }
                fontColor[i] = Color.Yellow;
            }

            playerChar.HairFrame.CurrentFrame = new Vector2(hChange, hCChange);
            playerChar.HeadFrame.CurrentFrame = new Vector2(skinChange, headChange);
            playerChar.EyesFrame.CurrentFrame = new Vector2(eyeCChange, eyeChange);
            playerChar.TorsoFrameMale.CurrentFrame = new Vector2(playerChar.TorsoFrameMale.CurrentFrame.X, skinChange * 4);
            playerChar.TorsoFrameFemale.CurrentFrame = new Vector2(playerChar.TorsoFrameFemale.CurrentFrame.X, skinChange * 4);
            playerChar.ArmsFrame.CurrentFrame = new Vector2(playerChar.ArmsFrame.CurrentFrame.X, skinChange * 10);
            playerChar.LegFrame.CurrentFrame = new Vector2(playerChar.LegFrame.CurrentFrame.X, skinChange * 4);
            //hairFrame.ImageColor = hairColorOptions[hCChange];
            playerChar.Gender = genderChoice[gChange];
            playerChar.Update(gameTime);
        }

        void timer1_Tick(object sender, EventArgs e) //timer1 event handler
        {
            timer1.Stop(); //stop timer1
            PlayerDetails.Instance.Area = areaChoice[aChange];
            PlayerDetails.Instance.Gender = genderChoice[gChange];
            PlayerDetails.Instance.Hair = hChange;
            PlayerDetails.Instance.HairColour = hCChange;
            PlayerDetails.Instance.Skin = skinChange;
            PlayerDetails.Instance.Head = headChange;
            PlayerDetails.Instance.Eye = eyeChange;
            PlayerDetails.Instance.EyeColour = eyeCChange;
            PlayerDetails.Instance.PPos = new Vector2(134, 300);
            PlayerDetails.Instance.AreaPosX = 1;
            PlayerDetails.Instance.AreaPosY = 1;
            PlayerDetails.Instance.PName = pName;
            PlayerDetails.Instance.FromPortal = false;
            PlayerDetails.Instance.PortalID = 0;
            PlayerDetails.Instance.NewGame = true;
            PlayerDetails.Instance.Loaded = false;
            if (InputManager.Instance.leftClick == false)
            ready = false;

        }
        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(textureRect, create, Color.White);
            for (int j = 0; j < leftRect.Count; j++)
            {
                spriteBatch.Draw(textureRect, leftRect[j], Color.Black);
                spriteBatch.Draw(leftArrow, leftPos[j] = new Vector2(leftPos[j].X, leftPos[j].Y), Color.White);
                
            }

            for (int j = 0; j < rightRect.Count; j++)
            {
                spriteBatch.Draw(textureRect, rightRect[j], Color.Black);
                spriteBatch.Draw(rightArrow, rightPos[j] = new Vector2(rightPos[j].X, rightPos[j].Y), Color.White);
                
            }

            for (int i = 0; i < customType.Count; i++)
            {
                //for (int j = 0; j < choice.Count; j++)
                //{
                spriteBatch.DrawString(font, customType[i], customTypePos[i], Color.Blue);
                spriteBatch.DrawString(font, areaChoice[aChange], aChoicePos, fontColor[i]);
                spriteBatch.DrawString(font, genderChoice[gChange], gChoicePos, fontColor[i]);
                spriteBatch.DrawString(font, hairChoice[hChange], hChoicePos, fontColor[i]);
                spriteBatch.DrawString(font, hairColorChoice[hCChange], hCChoicePos, fontColor[i]);
                spriteBatch.DrawString(font, headChoice[headChange], headChoicePos, fontColor[i]);
                spriteBatch.DrawString(font, eyesChoice[eyeChange], eyeChoicePos, fontColor[i]);
                spriteBatch.DrawString(font, eyeColorChoice[eyeCChange], eyeCChoicePos, fontColor[i]);
                spriteBatch.DrawString(font, skinChoice[skinChange], skinChoicePos, fontColor[i]);
                //}
            }

            playerChar.Draw(spriteBatch);            

            spriteBatch.DrawString(font, "Type Your Name", nameTextPos, Color.Blue);
            spriteBatch.DrawString(font, pName, chosenNamePos, Color.White);
            menuParticleEngine.Draw(spriteBatch);

        }

        #endregion


    }
}
