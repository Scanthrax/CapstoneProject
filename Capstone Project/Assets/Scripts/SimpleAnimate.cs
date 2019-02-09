using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimate : MonoBehaviour
{
    public float framesPerSecond = 10;
    SpriteRenderer renderer;

    public Characters character;

    Sprite[] anim;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        anim = IntroSpriteManager.instance.characterDict[character].waveHandFront;
    }

    void Update()
    {
        anim = IntroSpriteManager.instance.characterDict[character].waveHandFront;
        int index = Mathf.RoundToInt(Time.time * framesPerSecond) % anim.Length;
        renderer.sprite = anim[index];
    }
}
