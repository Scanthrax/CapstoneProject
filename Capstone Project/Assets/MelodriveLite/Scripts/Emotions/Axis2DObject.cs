using UnityEngine;

namespace Melodrive.Emotions
{
    /**
     * Lists the ways Melodrive can change between 2 and 3 dimensions.
     */
    public enum EmotionalAxis
    {
        XZ,
        XY,
        YZ
    };

    /**
     * Melodrive's emotional "positional" mode is in 2D. This class provides a helper
     * method for converting between 3D and 2D modes.
     */
    public class Axis2DObject : MelodriveObject
    {
        private Vector2 pos2D = new Vector2();

        public Vector2 Get2DPosition()
        {
            EmotionalAxis axis = md.emotionalAxis;

            if (axis == EmotionalAxis.XY)
            {
                pos2D.x = transform.position.x;
                pos2D.y = transform.position.y;
            }
            else if (axis == EmotionalAxis.XZ)
            {
                pos2D.x = transform.position.x;
                pos2D.y = transform.position.z;
            }
            else
            {
                pos2D.x = transform.position.y;
                pos2D.y = transform.position.z;
            }

            return pos2D;
        }
    }
}