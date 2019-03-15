using UnityEngine;

namespace Melodrive
{
    /**
     * MelodriveObject is a base class for all GameObjects that use Melodrive.
     * It finds the MelodrivePlugin instance added to the scene using 
     * FindObjectOfType<MelodrivePlugin>() and stores it in a protected "md" property.
     */
    public class MelodriveObject : MonoBehaviour
    {
        protected MelodrivePlugin md;
        
        void Awake()
        {
            md = FindMelodrivePlugin();
            if ( ! md)
                Debug.LogError(GetType().Name + " could not find Melodrive! Is a MelodrivePlugin component in the scene?");
        }

        public MelodrivePlugin FindMelodrivePlugin()
        {
            return FindObjectOfType<MelodrivePlugin>();
        }

        public MelodriveListener FindMelodriveListener()
        {
            return FindObjectOfType<MelodriveListener>();
        }
    }
}
