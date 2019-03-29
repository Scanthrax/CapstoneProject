using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAnimate : MonoBehaviour
{
    public float framesPerSecond = 10;
    SpriteRenderer spriteRenderer;
    public ActionObject actionObj;
    Sprite[] anim;
    public bool animate;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = actionObj.sprites;
        framesPerSecond = actionObj.frameRate;
        animate = false;
    }

    void Update()
    {
        if (animate)
        {
            int index = Mathf.RoundToInt(Time.time * framesPerSecond) % anim.Length;
            spriteRenderer.sprite = anim[index];
        }
    }

    public void UpdateSprites()
    {
        anim = actionObj.sprites;
        framesPerSecond = actionObj.frameRate;
    }
}
