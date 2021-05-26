using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheJourneyAhead
{
    public class RandCreature
    {
       
        public void randCreature(int i, int cCount, List<string>creatures, int ID, EntityManager eM, Random rand)
        {
            
            int creatureInd;
            //entityManager = new EntityManager();
            

            creatureInd = rand.Next(cCount);
            //ID.Add(Map.Instance.creature[i]);

            Type newClass = Type.GetType("TheJourneyAhead." + creatures[creatureInd]);
            eM.AddCreature((Creature)Activator.CreateInstance(newClass), ID);

        }

    }
}
