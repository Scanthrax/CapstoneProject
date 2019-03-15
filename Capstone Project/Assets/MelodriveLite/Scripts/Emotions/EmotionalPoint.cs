using UnityEngine;

namespace Melodrive.Emotions
{
    /**
     * EmotionalPoints can be used with Melodrive's "positional" emotion mode.
     * The assigned emotion and the position can affect the emotion of the music.
     */
    public class EmotionalPoint : Axis2DObject
    {
        public Emotion emotion {
            get { return _emotion; }
            set {
                _emotion = value;
                emotionChanged = true;
            }
        }

        [SerializeField]
        private Emotion _emotion = Emotion.Neutral;
        private bool emotionChanged = false;
        private int id = -1;

        public int GetID()
        {
            return id;
        }

        private void Start()
        {
            if (md)
                id = md.AddEmotionalPoint(this);
        }

        private void Update()
        {
            if (id > -1)
            {
                if (transform.hasChanged)
                    md.SetEmotionalPointPosition(this);
                if (emotionChanged)
                    md.SetEmotionAtPoint(this);
            }
        }
    }
}
