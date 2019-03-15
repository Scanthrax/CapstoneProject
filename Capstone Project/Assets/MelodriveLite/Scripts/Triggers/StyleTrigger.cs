using UnityEngine;
using Melodrive.Styles;

namespace Melodrive.Triggers
{
    /**
     * The style trigger allows you to create Style changes
     * based on colliding objects in the scene.
     */
    public class StyleTrigger : MelodriveTrigger
    {
        public Style enterStyle = Style.Piano;
        public Style exitStyle = Style.Piano;

        override protected void TriggerEnter(Collider other)
        {
            md.SetStyle(enterStyle);
        }

        override protected void TriggerExit(Collider other)
        {
            if (exitStyle != enterStyle)
                md.SetStyle(exitStyle);
        }
    }
}
