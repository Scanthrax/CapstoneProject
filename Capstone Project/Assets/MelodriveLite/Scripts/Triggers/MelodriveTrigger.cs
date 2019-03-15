using UnityEngine;

namespace Melodrive.Triggers
{
    /**
     * The MelodriveTrigger is the bass class for the various triggers.
     */
    public class MelodriveTrigger : MelodriveObject
    {
        public GameObject triggerObject;

        void Start()
        {
            if (!triggerObject)
            {
                MelodriveListener listener = FindObjectOfType<MelodriveListener>();
                if (listener)
                    triggerObject = listener.gameObject;
            }
        }

        protected virtual void TriggerEnter(Collider other) { }

        protected virtual void TriggerExit(Collider other) { }

        private void OnTriggerEnter(Collider other)
        {
            if (md && other.gameObject == triggerObject)
                TriggerEnter(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (md && other.gameObject == triggerObject)
                TriggerExit(other);
        }
    }
}
