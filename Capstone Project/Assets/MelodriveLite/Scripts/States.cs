using UnityEngine;
using Melodrive.Styles;

namespace Melodrive.States
{
    /**
     * A Melodrive State is a combination of style, musicalSeed and ensemble.
     */
    public class State
    {
        public Style style;
        public string musicalSeed;
        public string ensemble;
    }
}
