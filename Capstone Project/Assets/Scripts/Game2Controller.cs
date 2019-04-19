﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Game2Controller : MonoBehaviour
{

    public AudioSource rewardSoundSource;

    public static Game2Controller instance;

    public Transform arrow;

    public Transform[] positions;

    public int posTracker;

    public bool recognizedNewWord;
    public string wordRecognized;

    public Dictionary<string, Vector3> wordToPosition = new Dictionary<string, Vector3>();


    public TextMeshPro timerText;
    public float timer = 60f;

    public GameObject leftBubble, middleBubble, rightBubble;
    public RectTransform leftT, midT, rightT;

    ///////////////////////////////////////

    enum Game2State { Demand, Negate, Phrase, Correct, Wrong}

    public SpriteRenderer iconImage;
    public GameObject noSign;
    float iconImageAlpha;

    public float iconFadeSpeed;

    Game2State gameState;
    Game2State phraseState;

    public Text phraseText;

    float phraseTimer;
    public float phraseTime;

    bool newState;

    public GameObject speechBubble;

    bool canGuess;

    public string phrase = "";

    public List<ActionObject> listOfActions;

    int i;


    bool gameOver = false;


    public List<CharacterObject> listOfCharacters;

    public List<Game1Player> players;

    bool gameRunning;

    Game1Player personGuessing;

    public AudioSource endGame;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (MenuSelection.instance)
        {
            MenuSelection.instance.FadeIn(1f);
        }


        if (IntroController.instance)
        {
            listOfCharacters = IntroController.instance.charactersInGame;
        }

        if (AudioManager.instance)
        {
            AudioManager.instance.musicSource.clip = AudioManager.instance.game1;
            AudioManager.instance.musicSource.Play();
        }

        for (int i = 0; i < 3; i++)
        {
            print("here");
            players[i].character = listOfCharacters[i];
            players[i].ChangeSprite();
        }


        iconImageAlpha = 0f;
        iconImage.color = new Color(1f, 1f, 1f, 0f);
        ActivateGameObject(noSign, false);

        gameState = Game2State.Demand;
        newState = true;
        canGuess = true;

        i = 1;

        InitGame();
    }


    private void Update()
    {
        if(gameRunning)
        {
            if (recognizedNewWord)
            {
                recognizedNewWord = false;

                if ((gameState == Game2State.Demand || gameState == Game2State.Negate))
                {
                    ActivateGameObject(phraseText.gameObject, true);
                    SetBubbles(1);

                    gameState = Game2State.Phrase;
                    phraseText.text = wordRecognized;
                    phraseTimer = 0f;
                    newState = true;

                }
            }


            if (gameRunning)
            {
                if ((gameState == Game2State.Demand || gameState == Game2State.Negate))
                {
                    if (Time.frameCount % 60 == 0)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (!players[i].waving)
                            {
                                if (Random.value <= 0.1)
                                {
                                    AIGuess(i);
                                }
                            }
                        }
                    }
                }
            }


            if (timer > 0f)
            {
                timer -= Time.deltaTime;
                timerText.text = Mathf.CeilToInt(timer).ToString();
            }
            else
            {
                if (!gameOver)
                {
                    gameOver = true;
                    EndGame();
                }
            }

            #region DEMAND
            if (gameState == Game2State.Demand)
            {
                if (newState)
                {
                    phraseState = Game2State.Demand;
                    iconImage.color = new Color(1f, 1f, 1f, 0f);
                    iconImageAlpha = 0f;
                    phrase = iconImage.GetComponent<ActionIcon>().action.commandSentence;
                    newState = false;
                }

                if (iconImage.color.a != 1f)
                {
                    iconImageAlpha += Time.deltaTime / iconFadeSpeed;
                    iconImage.color = new Color(1f, 1f, 1f, iconImageAlpha);

                }
            }
            #endregion
            #region PHRASE
            else if (gameState == Game2State.Phrase)
            {
                if (newState)
                {
                    phraseTimer = 0f;
                    phraseTime = 1.5f;
                    newState = false;
                }

                if (phraseTimer <= phraseTime)
                    phraseTimer += Time.deltaTime;
                else
                {
                    if (wordRecognized == phrase && canGuess)
                    {
                        newState = true;
                        gameState = Game2State.Correct;
                    }
                    else
                    {
                        //newState = true;
                        gameState = phraseState;
                        SetBubbles(0);
                    }
                }
            }
            #endregion
            #region CORRECT
            else if (gameState == Game2State.Correct)
            {
                if (newState)
                {
                    newState = false;

                    print("correct!");
                    PlayRewardSound();
                    phraseTimer = 0f;
                    phraseTime = 1f;
                    FullAlpha();
                    personGuessing.UpdateScore(10);

                    if (phraseState == Game2State.Negate)
                    {
                        ActivateGameObject(noSign, false);
                        iconImage.GetComponent<ActionAnimate>().animate = false;
                    }
                }

                if (phraseTimer <= phraseTime)
                    phraseTimer += Time.deltaTime;

                else
                {
                    phraseTimer = 0f;

                    SetBubbles(0);

                    newState = true;
                    gameState = phraseState == Game2State.Demand ? Game2State.Negate : Game2State.Demand;
                    iconImage.GetComponent<ActionAnimate>().animate = phraseState == Game2State.Demand ? true : false;

                    if (phraseState == Game2State.Negate)
                    {
                        i++;
                        iconImage.GetComponent<ActionIcon>().Init(listOfActions[i % listOfActions.Count]);
                    }
                }

            }
            #endregion
            #region NEGATE
            else if (gameState == Game2State.Negate)
            {
                if (newState)
                {
                    phraseState = Game2State.Negate;
                    phrase = iconImage.GetComponent<ActionIcon>().action.negateSentence;
                    newState = false;
                    canGuess = false;
                }

                if (phraseTimer <= phraseTime)
                    phraseTimer += Time.deltaTime;
                else
                {
                    ActivateGameObject(noSign, true);
                    canGuess = true;
                }

            }
            #endregion


            if (Input.GetKeyDown(KeyCode.Z))
            {
                recognizedNewWord = false;

                if (gameState == Game2State.Demand || gameState == Game2State.Negate)
                {
                    ActivateGameObject(phraseText.gameObject, true);
                    SetBubbles(1);

                    gameState = Game2State.Phrase;
                    phraseText.text = "????????";
                    phraseTimer = 0f;
                    newState = true;

                }
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                recognizedNewWord = false;

                if (gameState == Game2State.Demand || gameState == Game2State.Negate)
                {
                    ActivateGameObject(phraseText.gameObject, true);
                    SetBubbles(2);

                    gameState = Game2State.Phrase;
                    phraseText.text = "????????";
                    phraseTimer = 0f;
                    newState = true;

                }
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                recognizedNewWord = false;

                if (gameState == Game2State.Demand || gameState == Game2State.Negate)
                {
                    ActivateGameObject(phraseText.gameObject, true);
                    SetBubbles(3);

                    gameState = Game2State.Phrase;
                    phraseText.text = "????????";
                    phraseTimer = 0f;
                    newState = true;

                }
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                AIGuess(1);
            }


            if (Input.GetKeyDown(KeyCode.P))
            {
                timer = 2f;
            }
        }

    }

    public void SetTImer(float time)
    {
        timer = time;
    }

    void AIGuess(int i)
    {
        recognizedNewWord = false;

        if ((gameState == Game2State.Demand || gameState == Game2State.Negate))
        {

            wordRecognized = phrase;
            ActivateGameObject(phraseText.gameObject, true);
            SetBubbles(i+1);

            gameState = Game2State.Phrase;
            phraseText.text = wordRecognized;
            phraseTimer = 0f;
            newState = true;
            personGuessing = players[i];
        }
    }


    void EndGame()
    {
        if (gameRunning)
        {
            gameRunning = false;
            MenuSelection.instance.GoToMenuScene(3f, MenuSelection.instance.selectedMinigame.Scene.name);
            print(MenuSelection.instance.selectedMinigame.Scene.name);
            timerText.text = "Finish!";
            endGame.Play();
        }
        //StaticVariables.minigame.Scores.Add(points[0]);
        //SceneManager.LoadScene(menuScene.name);
    }



    void PlayRewardSound()
    {
        rewardSoundSource.Play();
    }

    void FullAlpha()
    {
        iconImage.color = new Color(1f, 1f, 1f, 1f);
    }

    void ActivateGameObject(GameObject go, bool active)
    {
        go.SetActive(active);
    }

    void SetBubbles(int i)
    {
        if(i == 0)
        {
            leftBubble.SetActive(false);
            middleBubble.SetActive(false);
            rightBubble.SetActive(false);
            ActivateGameObject(phraseText.gameObject, false);
        }
        else if (i == 1)
        {
            leftBubble.SetActive(true);
            middleBubble.SetActive(false);
            rightBubble.SetActive(false);
            phraseText.rectTransform.position = leftT.position;
        }
        else if (i == 2)
        {
            leftBubble.SetActive(false);
            middleBubble.SetActive(true);
            rightBubble.SetActive(false);
            phraseText.rectTransform.position = midT.position;
        }
        else if (i == 3)
        {
            leftBubble.SetActive(false);
            middleBubble.SetActive(false);
            rightBubble.SetActive(true);
            phraseText.rectTransform.position = rightT.position;
        }
    }



    void InitGame()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        #region wait 1 second before starting the game
        // timer for moving the menu
        float journey = 0f;
        float duration = 2f;
        // keep adjusting the position while there is time
        while (journey <= duration)
        {
            // add to timer
            journey = journey + Time.deltaTime;
            yield return null;
        }
        #endregion

        gameRunning = true;

    }


}
