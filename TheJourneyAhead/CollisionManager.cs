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
    public class CollisionManager
    {

        #region Variables

        List<Rectangle> rectTile;
        List<String> rectSolid;
        List<Rectangle> rectTileSolids;
        Rectangle RPlayer;
        Vector2 PPlayer;
        public bool pGrav;
        #endregion

        #region Methods


        public void LoadContent(int tileCount, List<Rectangle> rTile, List<String> rSolid)
        {
        rectTile = new List<Rectangle>();

            for (int i = 0; i < tileCount; i++)
            {
                rectTile.Add(rTile[i]);
            }
        rectSolid = new List<String>();

            for (int i = 0; i < tileCount; i++)
            {
                rectSolid.Add(rSolid[i]);
            }


            rectTileSolids = new List<Rectangle>();
            
            for (int i = 0; i < rectTile.Count; i++ )
            {
                if (rectSolid[i] == "Solid")
                {
                    rectTileSolids.Add(rectTile[i]);
                }
               
            }



        }

        public void UpdatePlayerPos(Player player)
        {            
            bool tempGrav = true;
            bool tempXCollided = false;
            bool tempYCollided = false;
            for (int i = 0; i < rectTileSolids.Count; i++)
            {
                if(player.RPlayer.Intersects(rectTileSolids[i]))
                {
                    if (player.RPlayer.Bottom >= rectTileSolids[i].Top)
                    {
                        if (player.RPlayer.Bottom < rectTileSolids[i].Bottom)
                        {
                            //if ((player.RPlayer.Left < rectTileSolids[i].Right && player.RPlayer.Left > rectTileSolids[i].Left) || (player.RPlayer.Right > rectTileSolids[i].Left && player.RPlayer.Right < rectTileSolids[i].Right))
                            //{

                            if ((player.RPlayer.Right > rectTileSolids[i].Left + 30 && player.RPlayer.Left < rectTileSolids[i].Right - 30) || (player.RPlayer.Left < rectTileSolids[i].Right - 30 && player.RPlayer.Right > rectTileSolids[i].Left + 30))
                            {
                                player.PPlayer = new Vector2(player.PPlayer.X, (rectTileSolids[i].Y - player.RPlayer.Height + 1));
                                tempGrav = false;
                                tempYCollided = true;
                            }
                            //}
                        }
                    }


                    if (player.RPlayer.Left <= rectTileSolids[i].Right && player.PPlayer.X >= rectTileSolids[i].X )
                    {
                        if (player.RPlayer.Bottom > rectTileSolids[i].Bottom && player.RPlayer.Top < rectTileSolids[i].Top)
                        {
                            player.PPlayer = new Vector2((rectTileSolids[i].X + rectTileSolids[i].Width + 1), player.PPlayer.Y);
                            tempXCollided = true;
                        }
                    }

                    if (player.RPlayer.Right >= rectTileSolids[i].Left && player.PPlayer.X <= rectTileSolids[i].X)
                    {
                        if (player.RPlayer.Bottom > rectTileSolids[i].Bottom && player.RPlayer.Top < rectTileSolids[i].Top)
                        {
                            player.PPlayer = new Vector2((rectTileSolids[i].X - player.RPlayer.Width-1), player.PPlayer.Y);
                            tempXCollided = true;
                        }
                    }

                    if (player.RPlayer.Top <= rectTileSolids[i].Bottom)
                    {
                        if (player.RPlayer.Top > rectTileSolids[i].Top)
                        {
                            if ((player.RPlayer.Right > rectTileSolids[i].Left + 30 && player.RPlayer.Left < rectTileSolids[i].Right - 30) || (player.RPlayer.Left < rectTileSolids[i].Right - 30 && player.RPlayer.Right > rectTileSolids[i].Left + 30))
                            {
                                //if ((player.RPlayer.Left < rectTileSolids[i].Right && player.RPlayer.Left > rectTileSolids[i].Left) || (player.RPlayer.Right > rectTileSolids[i].Left && player.RPlayer.Right < rectTileSolids[i].Right))
                                player.PPlayer = new Vector2(player.PPlayer.X, (rectTileSolids[i].Y + rectTileSolids[i].Height + 1));
                                tempYCollided = true;
                            }
                        }
                    }
                }
                
            }
            if (player.RPlayer.Left < 0)
            {
               player.PPlayer = new Vector2(0, player.PPlayer.Y);
               tempXCollided = true;
            }
            if (player.RPlayer.Right > Map.Instance.XMax)
            {
               player.PPlayer = new Vector2(Map.Instance.XMax - player.RPlayer.Width, player.PPlayer.Y);
               tempXCollided = true;
            }

            RPlayer = new Rectangle(player.RPlayer.X,player.RPlayer.Y,player.RPlayer.Width, player.RPlayer.Height);
            PPlayer = new Vector2(player.PPlayer.X, player.PPlayer.Y);
            player.ActivateGravity = tempGrav;
            player.XCollided = tempXCollided;
            player.YCollided = tempYCollided;
        }

        public bool checkPPos(Rectangle rect)
        {
            if (rect.Intersects(RPlayer))
            {
                return true;
            }
            else
                return false;
            
        }
        public void UpdateEntityPos(Creature c)//Vector2 ePos, Rectangle eRect, bool activateGravity)
        {
            bool tempGrav = true;
            for (int i = 0; i < rectTileSolids.Count; i++)
            {
                if (c.RCreature.Intersects(rectTileSolids[i]))
                {
                    if (c.RCreature.Bottom >= rectTileSolids[i].Top)
                    {
                        if (c.RCreature.Bottom < rectTileSolids[i].Bottom)
                        {
                        //if ((c.RCreature.Left < rectTileSolids[i].Right && c.RCreature.Left > rectTileSolids[i].Left) || (c.RCreature.Right > rectTileSolids[i].Left && c.RCreature.Right < rectTileSolids[i].Right))
                        //{

                            if ((c.RCreature.Right > rectTileSolids[i].Left + 30 && c.RCreature.Left < rectTileSolids[i].Right - 30) || (c.RCreature.Left < rectTileSolids[i].Right - 30 && c.RCreature.Right > rectTileSolids[i].Left + 30))
                            {
                                c.PCreature = new Vector2(c.PCreature.X, (rectTileSolids[i].Y - c.RCreature.Height + 1));
                                tempGrav = false;
                            }
                        //}
                        }
                    }


                    if (c.RCreature.Left <= rectTileSolids[i].Right && c.PCreature.X >= rectTileSolids[i].X)
                    {
                        if (c.RCreature.Bottom > rectTileSolids[i].Bottom && c.RCreature.Top < rectTileSolids[i].Top)
                        {
                           c.PCreature = new Vector2((rectTileSolids[i].X + rectTileSolids[i].Width + 1), c.PCreature.Y);                       
                        }
                    }

                    if (c.RCreature.Right >= rectTileSolids[i].Left && c.PCreature.X <= rectTileSolids[i].X)
                    {
                        if (c.RCreature.Bottom > rectTileSolids[i].Bottom && c.RCreature.Top < rectTileSolids[i].Top)
                        {
                            c.PCreature = new Vector2((rectTileSolids[i].X - c.RCreature.Width - 1), c.PCreature.Y);
                        }
                    }

                    if (c.RCreature.Top <= rectTileSolids[i].Bottom)
                    {
                        if (c.RCreature.Top > rectTileSolids[i].Top)
                        {
                            if ((c.RCreature.Right > rectTileSolids[i].Left + 30 && c.RCreature.Left < rectTileSolids[i].Right - 30) || (c.RCreature.Left < rectTileSolids[i].Right - 30 && c.RCreature.Right > rectTileSolids[i].Left + 30))
                            {
                                //if ((c.RCreature.Left < rectTileSolids[i].Right && c.RCreature.Left > rectTileSolids[i].Left) || (c.RCreature.Right > rectTileSolids[i].Left && c.RCreature.Right < rectTileSolids[i].Right))
                                c.PCreature = new Vector2(c.PCreature.X, (rectTileSolids[i].Y + rectTileSolids[i].Height + 1));
                            }
                        }
                    }
                }
            }
            if (c.RCreature.Left < 0)
            {
                c.PCreature = new Vector2(0, c.PCreature.Y);
            }
            if (c.RCreature.Right > Map.Instance.XMax)
            {
                c.PCreature = new Vector2(Map.Instance.XMax - c.RCreature.Width, c.PCreature.Y);                
            }
                       
            c.ActivateGravity = tempGrav;
            
            //for (int i = 0; i < rectTile.Count; i++ )
            //{
            //    if (eRect.Contains(rectTile[i]) && rectSolid[i] == "Solid")
            //    {
            //        if (eRect.Bottom >= rectTile[i].Top)
            //        {
            //            ePos = new Vector2(ePos.X, rectTile[i].Y + rectTile[i].Height);
            //            activateGravity = false;

            //        }
            //        else
            //            activateGravity = true;
                    
            //    }

            //}
           
        }

        #endregion


    }
}
