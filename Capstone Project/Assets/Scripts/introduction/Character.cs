using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterObject character;

    SpriteRenderer mat;

    public Sprite male1, male2;

    void Start()
    {
        mat = GetComponentInChildren<SpriteRenderer>();

        if(character.isMale)
        {
            mat.sprite = male1;
        }
        else
        {
            mat.sprite = male2;
        }
    }
}
