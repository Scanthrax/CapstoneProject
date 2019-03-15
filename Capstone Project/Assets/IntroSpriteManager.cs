using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Characters { Male1, Male2, Female1, Female2 }

public class IntroSpriteManager : MonoBehaviour
{

    public static IntroSpriteManager instance;

    public List<CharacterToAnimation> characterMappings;


    public Dictionary<Characters, CharacterAnimations> characterDict;


    [System.Serializable]
    public struct CharacterToAnimation
    {
        public Characters name;
        public Sprite[] waveHandFront, waveHandBehind;
        public Sprite front, behind;
    }

    [System.Serializable]
    public struct CharacterAnimations
    {
        public Sprite[] waveHandFront, waveHandBehind;
        public Sprite front, behind;

        public CharacterAnimations(Sprite[] s1, Sprite[] s2, Sprite s3, Sprite s4)
        {
            waveHandFront = s1;
            waveHandBehind = s2;
            front = s3;
            behind = s4;
        }
    }


    private void Awake()
    {
        instance = this;



        foreach (var item in characterMappings)
        {
            characterDict.Add(item.name, new CharacterAnimations(item.waveHandFront, item.waveHandBehind, item.front, item.behind));
        }
    }


}
