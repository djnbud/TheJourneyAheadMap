using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheJourneyAhead
{
    public class ParticleEngine
    {
        #region Variables

        private Random random;
        public Vector2 EmitterLocation { get; set; }
        public List<Color> pColor;
        private List<Particle> particles;
        private List<Texture2D> textures;
        private int particleTimer;

        #endregion

        #region Methods

        public ParticleEngine(List<Texture2D> textures, Vector2 location, List<Color> pColor)
        {
            EmitterLocation = location;
            this.pColor = pColor;
            //this.pColor = new List<Color>();
            this.textures = textures;
            this.particles = new List<Particle>();
            random = new Random();
        }

        private Particle GenerateNewParticleForMenu()
        {
            
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(
                (float)(random.Next(-1, 2)),
                Math.Abs(1f * (float)(random.NextDouble() * 2)));
            float angle = 0;
            float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            Color color = pColor[random.Next(pColor.Count)];
            float size = (float)random.NextDouble();
            int ttl = 25 + random.Next(50);

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);

        }


        public void Update(GameTime gameTime, InputManager input)
        {
            for (int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update();
                if (particles[particle].TTL <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        public void GenerateForMenu()
        {
            particleTimer++;
            int total = 1;
            if (particleTimer < 4)
            {
                for (int i = 0; i < total; i++)
                {
                    particles.Add(GenerateNewParticleForMenu());
                }
            }
            else if (particleTimer > 25)
                particleTimer = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);
            }
        }

        #endregion
    }
}
