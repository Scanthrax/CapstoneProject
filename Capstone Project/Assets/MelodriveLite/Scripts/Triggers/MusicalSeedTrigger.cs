using UnityEngine;

namespace Melodrive.Triggers
{
    /**
     * The musical seed trigger allows you to change Musical Seed
     * based on colliding objects in the scene.
     */
    public class MusicalSeedTrigger : MelodriveTrigger
    {
        public string enterMusicalSeed = "";
        public string exitMusicalSeed = "";

        override protected void TriggerEnter(Collider other)
        {
            if (enterMusicalSeed != "")
                md.SetMusicalSeed(enterMusicalSeed);
        }

        override protected void TriggerExit(Collider other)
        {
            if (exitMusicalSeed != "" && exitMusicalSeed != enterMusicalSeed)
                md.SetMusicalSeed(exitMusicalSeed);
        }
    }
}
