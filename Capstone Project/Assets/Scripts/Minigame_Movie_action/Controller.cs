﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public Transform screen;

    public GameObject action;

    public int numberOfActions;
    public Difficulty difficulty;
    public GameObject[] players;
    public Text[] text;
    int[] points = new int[4];
    bool[] aiScore = new bool[4];

    public string wordRecognized;
    public bool recognizedNewWord = false;

    List<GameObject> actions = new List<GameObject>();


    public float timer;
    public Text timerText;
    public Object menuScene;


    public ActionObject[] actionObjects;


    public Dictionary<string, List<GameObject>> wordToObject = new Dictionary<string, List<GameObject>>();

    float duration = 2f;

    float speed;

    bool gameRunning = false;

    public GameObject startText;


    public static Controller instance;


    public Transform arenaContainer;

<<<<<<< HEAD
=======
    public AudioSource correctAnswerSource;

<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> parent of 5440228... push 2/15
=======
>>>>>>> parent of 5440228... push 2/15
=======
>>>>>>> parent of 5440228... push 2/15
    private void Awake()
    {
        instance = this;
    }



    // Use this for initialization
    void Start ()
    {

        
        switch(difficulty)
        {
            case Difficulty.Easy:
                speed = 0.75f;
                break;
            case Difficulty.Intermediate:
                speed = 1f;
                break;
            case Difficulty.Difficult:
                speed = 1.25f;
                break;
            default:
                speed = 1f;
                break;
        }

        foreach (var item in text)
        {
            item.text = 0.ToString();
        }

        InitGame();


        MenuSelection.instance.FadeIn(1f);
    }
	
	// Update is called once per frame
	void Update ()
    {


        //if(Input.GetKeyDown(KeyCode.R))
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //}

        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    timer = 2f;
        //}

        //if (recognizedNewWord)
        //{
        //    if (wordToObject.ContainsKey(wordRecognized))
        //    {
        //        points[0] += RandomPosition(wordRecognized) * 10;

        //        text[0].text = points[0].ToString();
        //    }
        //    recognizedNewWord = false;
        //}

        if (Input.GetKeyDown(KeyCode.Z))
        {
            RandomPosition(actionObjects[0].sentence);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            IncreasePoints(1);
            //RandomPosition();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            IncreasePoints(2);
            //RandomPosition();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            IncreasePoints(3);
            //RandomPosition();
        }

        if (gameRunning)
            timer -= Time.deltaTime;

        timerText.text = Mathf.RoundToInt(timer).ToString();

        if (timer <= 0f)
        {
            EndGame();
        }

        if (gameRunning)
        {
            if (Time.frameCount % 60 == 0)
            {
                for (int i = 1; i < aiScore.Length; i++)
                {
                    if (Random.value <= 0.1)
                    {
                        aiScore[i] = true;
                    }

                    if (aiScore[i])
                    {
                        IncreasePoints(i);
                        aiScore[i] = false;
                    }
                }
            }
        }

    }

    void RandomPosition()
    {
        int rand = Random.Range(0, actions.Count);

        float x = Random.Range(-screen.transform.localScale.x * 5f, screen.transform.localScale.x * 5f);
        float y = Random.Range(-screen.transform.localScale.z * 5f, screen.transform.localScale.z * 5f);

        actions[rand].transform.position = new Vector3(screen.transform.position.x + x, screen.transform.position.y + y, screen.transform.position.z - 0.01f);
    }



    void IncreasePoints(int i)
    {
        points[i] += 10;
        text[i].text = points[i].ToString();
    }

    int RandomPosition(string key)
    {
        int amount = wordToObject[key].Count;
        print(amount);

        foreach (GameObject item in wordToObject[key].ToArray())
        {
            float x = Random.Range(-screen.transform.localScale.x * 5f, screen.transform.localScale.x * 5f);
            float y = Random.Range(-screen.transform.localScale.z * 5f, screen.transform.localScale.z * 5f);

            item.transform.position = new Vector3(x,y);

            var temp = item.GetComponent<movement>().actionObj.sentence;


            wordToObject[temp].Remove(item);

            item.GetComponent<movement>().SetUp(actionObjects[Random.Range(0,actionObjects.Length)]);

            temp = item.GetComponent<movement>().actionObj.sentence;

            wordToObject[temp].Add(item);
        }

        return amount;
    }


    void EndGame()
    {
        gameRunning = false;
        MenuSelection.instance.FadeOut(3f);
        timerText.gameObject.SetActive(false);
        startText.gameObject.SetActive(true);
        startText.GetComponent<Text>().text = "Finish!";
        //StaticVariables.minigame.Scores.Add(points[0]);
        //SceneManager.LoadScene(menuScene.name);
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

        // keep adjusting the position while there is time
        while (journey <= duration)
        {
            // add to timer
            journey = journey + Time.deltaTime;
            yield return null;
        }
        #endregion

        for (int i = 0; i < numberOfActions; i++)
        {
            float x = Random.Range(screen.position.x - 4.5f, screen.position.x + 4.5f);
            float y = Random.Range(screen.position.y - 3f, screen.position.y + 3f);

            var obj = Instantiate(action, new Vector3(screen.transform.position.x + x, screen.transform.position.y + y, screen.transform.position.z - 0.01f), Quaternion.identity, arenaContainer).GetComponent<movement>();

            actions.Add(obj.gameObject);
            obj.speed = speed;
            obj.actionObj = actionObjects[Random.Range(0, actionObjects.Length)];

            //var sentence = obj.actionObj.sentence;

            //if (!wordToObject.ContainsKey(sentence))
            //{
            //    wordToObject.Add(sentence, new List<GameObject>());
            //}
            //wordToObject[sentence].Add(obj.gameObject);

        }

        gameRunning = true;

        startText.SetActive(false);
    }


}
