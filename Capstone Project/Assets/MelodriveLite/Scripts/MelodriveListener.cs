using Melodrive.Emotions;

namespace Melodrive
{
    /**
     * A MelodriveListener can be used in the "positional" emotion mode.
     * Melodrive's emotion will be calulated between the EmotionalPoints in the Scene and this object.
     */
    public class MelodriveListener : Axis2DObject
    {
        private void Start()
        {
            if (md)
                md.SetListenerPosition(this);
        }

        private void Update()
        {
            if (md && transform.hasChanged)
            {
                md.SetListenerPosition(this);
            }
        }

    }
}