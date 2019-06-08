using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game1Player : MonoBehaviour
{
    public TextMeshPro scoreText;
    public int score;
    public bool guess, waving;
    public SpriteRenderer characterRender;
    public CharacterObject character;
    public float waveTimer;
    public string guessWord;
    public Color textColor;
    public bool player;
    public int i;
    void Start()
    {
        score = 0;
        scoreText.text = score.ToString();
    }


    public void UpdateScore(int add)
    {
        score += add;
        scoreText.text = score.ToString();
    }

    void Update()
    {
        

        if(waving)
        {
            int index = Mathf.RoundToInt(Time.time * 30) % character.waveHandFront.Length;
            characterRender.sprite = character.waveHandFront[index];
            waveTimer -= Time.deltaTime;


        }

    }



    private void OnValidate()
    {
        ChangeSprite();
    }

    public void ChangeSprite()
    {
        if (characterRender && character)
        {
            characterRender.sprite = character.behind;
        }
        scoreText.color = character.color;
    }

    public void ChangeSprite(CharacterObject charObj)
    {
        character = charObj;
        if (characterRender && character)
        {
            characterRender.sprite = character.behind;
        }
        scoreText.color = character.color;
    }


    public void StartWaving(string word)
    {
        guessWord = word;
        waving = true;
        waveTimer = 2f;
    }

}
