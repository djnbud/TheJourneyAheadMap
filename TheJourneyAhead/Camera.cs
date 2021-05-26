using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheJourneyAhead
{
    public class Camera
    {
        #region Variables

        private static Camera instance;
        public Vector2 position;
        private static Vector2 cameraPos;
        Matrix viewMatrix;

        #endregion

        #region Properties


        public Matrix ViewMatrix
        {
            get { return viewMatrix; }
        }

        public static Camera Instance
        {
            get
            {
                if (instance == null)
                    instance = new Camera();
                return instance;
            }
        }

        public Vector2 CameraPos
        {
            get { return cameraPos; }
            set { cameraPos = value; }
        }

       

        #endregion


        #region Methods

        public void LoadContent()
        { }

        public void SetFocalPoint(Vector2 focalPosition)
        {
            position = new Vector2(focalPosition.X - ScreenManager.Instance.Dimensions.X / 2,
            focalPosition.Y - ScreenManager.Instance.Dimensions.Y / 2);

            if (position.X < 0) //Scroll after the half way point
                position.X = 0;
            //if (position.Y < 0)
              //  position.Y = 0;
            
            if (position.X > Map.Instance.XMax - ScreenManager.Instance.Dimensions.X)
                position.X = Map.Instance.XMax - ScreenManager.Instance.Dimensions.X;
           if (position.Y > ScreenManager.Instance.Dimensions.Y)
                position.Y = ScreenManager.Instance.Dimensions.Y;
           cameraPos = position;
        }

        public void Update()
        {
            viewMatrix = Matrix.CreateTranslation(new Vector3(-position, 0));
            cameraPos = position;
        }

        #endregion


    }
}
