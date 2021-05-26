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
    public class EntityManager
    {

        #region Variables
        
        Player player;
        ContentManager content;
        InputManager inputManager;
       // List<Tiles> tiles;
        List<Creature> currentCreatures;
        List<Creature> newCreatures;
        List<Portal> Portals;
        int tiles;
        CollisionManager collisionManager;
        bool creatureChange;
        private static EntityManager instance;        
        Map _Map;
        Random rand;
        #endregion

        #region Properties

        public static EntityManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EntityManager();
                }
                return instance;
            }
        }

        public Player Player
        {
            get { return player; }
            set { player = value; }
        }


        #endregion

        #region Methods

        public void AddCreature(Creature creature, int Id)
        {
            //if (newCreatures[Id] != creature)
            //{
            creatureChange = true;
                newCreatures.Add(creature); //use part of Id of creature from Map to define which creature that has changed so you can have multiple creatures
            //}
                
        }

        public void LoadContent(ContentManager Content, InputManager InputManager, Vector2 pPos, Map map)
        {
            this.inputManager = InputManager;
            content = Content;
            //tiles = new List<Tiles();
            //inputManager = InputManager;
            
            _Map = map;
            player = new Player();
            currentCreatures = new List<Creature>();
            newCreatures = new List<Creature>();
            
            collisionManager = new CollisionManager();
            rand = new Random();
            //creature = new Creature();
            player.LoadContent(Content, inputManager, pPos);
            tiles = map.tile.Count;

            Portals = new List<Portal>();

            for (int i = 0; i < _Map.portal.Count; i++)
            {
                Portal p = new Portal();
                p.LoadContent(Content, inputManager, _Map.portal[i], _Map.portalX[i], _Map.portalY[i]);
                Portals.Add(p);

            }

                for (int i = 0; i < currentCreatures.Count; i++)
                {
                    currentCreatures[i].LoadContent(Content, inputManager, _Map.creature[i], _Map.creatureX[i], _Map.creatureY[i], rand); //Need to add way of choosing creature!!!!!!!
                }
            //for (int y = 0; y < notiles; y++)
           // {
           //     tiles.LoadContent(Map.Instance.rTile[y],Map.Instance.tileSolid[y]);
           // }
                collisionManager.LoadContent(tiles, _Map.rTile, _Map.tileSolid);
            

        }

        public void UnloadContent()
        {
            player.UnloadContent();
            currentCreatures.Clear();
            newCreatures.Clear();
            _Map.UnloadContent();
            for (int i = 0; i < currentCreatures.Count; i++)
            {
                currentCreatures[i].UnloadContent();
            }


            
        }

        public void Update(GameTime gameTime, GraphicsDeviceManager Graphics)
        {
            inputManager.Update(gameTime);
            Creature c;
            

            for (int i = 0; i < currentCreatures.Count; i++)
            {
                //collisionManager.UpdateEntityPos(currentCreatures[i].PCreature, currentCreatures[i].RCreature, currentCreatures[i].ActivateGravity);
                c = currentCreatures[i];

                collisionManager.UpdateEntityPos(c);
                //currentCreatures[i] = c;
            }

            player.Update(gameTime,Graphics,inputManager);
            collisionManager.UpdatePlayerPos(player);
            for (int i = 0; i < Portals.Count; i++)
            {
                Portals[i].Update(collisionManager, inputManager);
            }

                if (creatureChange == false)
                {
                    for (int i = 0; i < currentCreatures.Count; i++)
                    {
                        currentCreatures[i].Update(gameTime, Graphics);

                    }
                }
                else
                    CreatureChange(gameTime);


        }

        private void CreatureChange(GameTime gameTime)
        {
            //screenStack.Push(newScreen);
            
            for (int i = 0; i < newCreatures.Count; i++)
            {
                if (currentCreatures.Count >= newCreatures.Count)
                {
                    currentCreatures[i].UnloadContent();
                    currentCreatures.RemoveAt(i);
                    
                }
                else
                {
                    currentCreatures.Add(newCreatures[i]);
                }
                currentCreatures[i].LoadContent(content, inputManager, _Map.creature[i], _Map.creatureX[i], _Map.creatureY[i], rand);
            }
            
            creatureChange = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            player.Draw(spriteBatch);
            for (int i = 0; i < Portals.Count; i++)
            {
                Portals[i].Draw(spriteBatch);
            }
                for (int i = 0; i < currentCreatures.Count; i++)
                {
                    currentCreatures[i].Draw(spriteBatch);
                }

        }


        #endregion

    }
}
