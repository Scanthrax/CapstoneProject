using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimate : MonoBehaviour
{
    public float framesPerSecond = 10;
    SpriteRenderer renderer;
    public Sprite[] male1, male2;

    public Character character;

    bool male;
    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        male = character.character.isMale ? true : false;
    }

    void Update()
    {
        Sprite[] temp = male ? male1 : male2;
        int index = Mathf.RoundToInt(Time.time * framesPerSecond) % temp.Length;
        renderer.sprite = temp[index];
    }
}
