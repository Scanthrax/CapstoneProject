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

            //if(waveTimer <= 0f)
            //{
            //    waving = false;
            //    characterRender.sprite = character.behind;

            //    score += 10;
            //    scoreText.text = score.ToString();
            //}
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
    }


    public void StartWaving()
    {
        waving = true;
        waveTimer = Random.Range(1f, 3f);
    }

}
