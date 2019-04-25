using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimpleAnimate : MonoBehaviour
{
    public float framesPerSecond = 10;
    Image renderer;

    public TextMeshProUGUI text;

    public CharacterObject character;

    Sprite[] anim;

    private void Start()
    {
        renderer = GetComponent<Image>();
        anim = character.waveHandFront;
    }

    void Update()
    {
        int index = Mathf.RoundToInt(Time.time * framesPerSecond) % anim.Length;
        renderer.sprite = anim[index];
    }



    public void Init(CharacterObject character)
    {
        this.character = character;
        text.text = character.name;
    }
}
