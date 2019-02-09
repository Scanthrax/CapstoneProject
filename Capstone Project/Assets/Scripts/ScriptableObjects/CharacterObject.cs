using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class CharacterObject : ScriptableObject
{
    public new string name;
    public string nationality;
    public bool isMale;
}
