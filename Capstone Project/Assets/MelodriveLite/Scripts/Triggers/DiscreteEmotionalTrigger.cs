using UnityEngine;
using Melodrive.Emotions;

namespace Melodrive.Triggers
{
    /**
     * The discrete emotional trigger allows you to create emotion changes
     * based on colliding objects in the scene.
     */
    public class DiscreteEmotionalTrigger : MelodriveTrigger
    {
        public Emotion enterEmotion = Emotion.Neutral;
        public Emotion exitEmotion = Emotion.Neutral;

        override protected void TriggerEnter(Collider other)
        {
            md.SetEmotion(enterEmotion);
        }

        override protected void TriggerExit(Collider other)
        {
            if (exitEmotion != enterEmotion)
                md.SetEmotion(exitEmotion);
        }
    }
}
