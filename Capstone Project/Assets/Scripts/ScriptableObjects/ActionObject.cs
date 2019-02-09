using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Action", menuName = "Action")]
public class ActionObject : ScriptableObject
{
    public string sentence;
    SpriteRenderer renderer;
    public Sprite[] sprites;
    public float frameRate;
}
