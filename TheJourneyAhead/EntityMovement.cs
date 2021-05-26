using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace TheJourneyAhead
{
    public class EntityMovement
    {

        bool right;
        bool left;
        public bool jump;
        bool jumping;
        public bool startJump;
        float moveSpeed;
        float jumpSpeed;
        
        public Vector2 velocity;        
        Timer timer1 = new Timer(); //Creates throw animation timer so user just needs to tap space bar and player will animate once
        

        public void LoadContent()
        {
            
            right = false;
            left = false;
            jump = false;
            jumping = false;
            moveSpeed = 0f;
            //jumpSpeed = 0f;
            jumpSpeed = 0;
            velocity = new Vector2(0, 0);
        }

        public void Update(GameTime gametime, bool Right, bool StartJump, bool Left, bool Jump, float MoveSpeed, float JumpSpeed)
        {            
            right = Right;
            left = Left;
            jump = Jump;
            moveSpeed = MoveSpeed;
            startJump = StartJump;
            velocity.X = 0;
            velocity.Y = 0;
            

            if (right == true)
            { velocity.X = +moveSpeed * (float)gametime.ElapsedGameTime.TotalSeconds; }
            if(left==true)
            { velocity.X = -moveSpeed * (float)gametime.ElapsedGameTime.TotalSeconds; }
            if (jump == true)
            {
                if (startJump == true)
                {
                    jumpSpeed = JumpSpeed;
                    startJump = false;
                    timer1.Interval = 500; //Sets timer1 interval to 1/2 second   300
                    jumping = true;
                    timer1.Elapsed += new ElapsedEventHandler(timer1_Tick); //Once timer1 has finished go to event handler method below
                    timer1.Start();
                }
                if (jumping == true)
                {

                    velocity.Y = +jumpSpeed;
                    jumpSpeed += 1;

                    //velocity.Y = -jumpSpeed * (float)gametime.ElapsedGameTime.TotalSeconds;
                    //jumpSpeed += JumpSpeed;
                }
            }

            
            //else
           //{
            //   JumpSpeed = 0;                
           // }
            
       
        }
        
        void timer1_Tick(object sender, EventArgs e) //timer1 event handler
        {
            timer1.Stop(); //stop timer1            
            reset();
        }        

        public void reset()
        {            
            timer1.Stop(); //stop timer1  
            right = false;
            left = false;
            jump = false;
            jumping = false;
            jumpSpeed = 0;
        }

    }
}
