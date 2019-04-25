using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;
using UnityEngine.UI;
using TMPro;

public class Controller : MonoBehaviour
{
    public Transform screen;

    public GameObject action;

    public int numberOfActions;
    public Difficulty difficulty;
    public Game1Player[] players;


    public string[] wordbank;


    public string wordRecognized;
    public bool recognizedNewWord = false;



    public float timer;
    public TextMeshPro timerText;


    public ActionObject[] actionObjects;


    public Dictionary<string, List<GameObject>> wordToObject;

    float duration = 2f;

    float speed;

    bool gameRunning = false;

    public TextMeshPro startText;


    public static Controller instance;


    public Transform arenaContainer;

    public AudioSource correctAnswerSource;


    public Image fadeScreen;


    public List<CharacterObject> listOfCharacters;

    public AudioSource endGame;

    public ParticleSystem correctParticles;


    public SpeechBubble[] bubbles;

    [System.Serializable]
    public struct SpeechBubble
    {
        public GameObject speechBubble;
        public TextMeshPro bubbleText;
    }

    private void Awake()
    {
        instance = this;
    }



    // Use this for initialization
    void Start ()
    {
        wordToObject = new Dictionary<string, List<GameObject>>();

        switch (difficulty)
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


        


        if (MenuSelection.instance)
        {
            MenuSelection.instance.FadeIn(1f);
        }


        if(IntroController.instance)
        {
            listOfCharacters = IntroController.instance.charactersInGame;
        }

        if(AudioManager.instance)
        {
            AudioManager.instance.musicSource.clip = AudioManager.instance.game1;
            AudioManager.instance.musicSource.Play();
        }


        wordbank = new string[5];
        for (int i = 0; i < 5; i++)
        {
            wordbank[i] = actionObjects[i].presentSimpleSentence;
        }



        for (int i = 0; i < 4; i++)
        {
            print("here");
            players[i].character = listOfCharacters[i];
            players[i].ChangeSprite();
        }

        InitGame();


        //var outline = Instantiate(new GameObject(), players[0].transform.position, Quaternion.identity, players[0].transform);
        //outline.transform.localScale += Vector3.one * 0.1f;
        //outline.transform.position += Vector3.back * 0.1f + Vector3.down * 0.12f;
        //var outlineRend = outline.AddComponent<SpriteRenderer>();
        //outlineRend.sprite = players[0].character.silhouette;
        //outlineRend.color = Color.yellow;



        foreach (var item in bubbles)
        {
            item.speechBubble.SetActive(false);
        }
    }
	

    void PlaySoundCorrect()
    {
        correctAnswerSource.Play();
    }



    public void SetTimer(float time)
    {
        timer = time;
    }

	// Update is called once per frame
	void Update ()
    {


        if (Input.GetKeyDown(KeyCode.P))
        {
            timer = 2f;
        }

        if (recognizedNewWord)
        {
            recognizedNewWord = false;
            if (wordToObject.ContainsKey(wordRecognized))
            {
                PlaySoundCorrect();
                for (int i = 0; i < wordToObject[wordRecognized].Count; i++)
                {
                    players[0].StartWaving(wordbank[Random.Range(0,5)]);
                }
                
            }

        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            players[0].StartWaving(wordbank[Random.Range(0, 5)]);
            bubbles[0].speechBubble.SetActive(true);
            bubbles[0].bubbleText.text = players[0].guessWord;

        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            players[1].StartWaving(wordbank[Random.Range(0, 5)]);
            bubbles[1].speechBubble.SetActive(true);
            bubbles[1].bubbleText.text = players[1].guessWord;

        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            players[2].StartWaving(wordbank[Random.Range(0, 5)]);
            bubbles[2].speechBubble.SetActive(true);
            bubbles[2].bubbleText.text = players[2].guessWord;

        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            players[3].StartWaving(wordbank[Random.Range(0, 5)]);
            bubbles[3].speechBubble.SetActive(true);
            bubbles[3].bubbleText.text = players[3].guessWord;
        }

        if (gameRunning)
            timer -= Time.deltaTime;

        if(timerText.gameObject.activeSelf || timerText != null)
            timerText.text = Mathf.RoundToInt(timer).ToString();

        if (timer <= 0f)
        {
            EndGame();
        }

        if (gameRunning)
        {
            if (Time.frameCount % 60 == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (!players[i].waving)
                    {
                        if (Random.value <= 0.1)
                        {
                            players[i].StartWaving(wordbank[Random.Range(0, 5)]);
                            bubbles[i].speechBubble.SetActive(true);
                            bubbles[i].bubbleText.text = players[i].guessWord;
                        }
                    }
                }
            }
        }

        foreach (var item in players)
        {
            if(item.waving)
            {
                if(item.waveTimer <= 0f)
                {
                    item.waving = false;
                    item.characterRender.sprite = item.character.behind;

                    bubbles[item.i].speechBubble.SetActive(false);

                    if (wordToObject.ContainsKey(item.guessWord))
                    {
                        var tempList = new List<GameObject>();
                        foreach (var action in wordToObject[item.guessWord])
                        {
                            item.score += 10;
                            var particles = Instantiate(correctParticles, action.transform.position, Quaternion.Euler(-90,0,0),action.transform.parent);
                            particles.Play();
                            Destroy(particles.gameObject, 2f);
                            tempList.Add(action);
                        }

                        if(wordToObject[item.guessWord].Count > 0)
                            PlaySoundCorrect();

                        foreach (var it in tempList)
                        {
                            wordToObject[item.guessWord].Remove(it);
                            Destroy(it.gameObject);
                            SpawnAction();
                        }
                    }

                    item.scoreText.text = item.score.ToString();
                }
            }
        }

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

            var temp = item.GetComponent<movement>().actionObj.presentSimpleSentence;


            wordToObject[temp].Remove(item);

            item.GetComponent<movement>().SetUp(actionObjects[Random.Range(0,actionObjects.Length)]);

            temp = item.GetComponent<movement>().actionObj.presentSimpleSentence;

            wordToObject[temp].Add(item);
        }

        return amount;
    }


    void EndGame()
    {
        if (gameRunning)
        {
            gameRunning = false;
            MenuSelection.instance.GoToMenuScene(3f, MenuSelection.instance.selectedMinigame.sceneName);
            print(MenuSelection.instance.selectedMinigame.sceneName);
            timerText.gameObject.SetActive(false);
            startText.gameObject.SetActive(true);
            startText.text = "Finish!";
            MenuSelection.instance.selectedMinigame.Scores.Add(players[0].score);
            endGame.Play();
        }
        
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
            SpawnAction();

        }

        gameRunning = true;

        startText.gameObject.SetActive(false);
    }


    void SpawnAction()
    {
        float x = Random.Range(screen.position.x - 2f, screen.position.x + 2f);
        float y = Random.Range(screen.position.y - 3f, screen.position.y + 0.5f);

        var obj = Instantiate(action, new Vector3(screen.transform.position.x + x, screen.transform.position.y + y, screen.transform.position.z - 0.01f), Quaternion.identity, arenaContainer).GetComponent<movement>();

        obj.speed = speed;
        obj.actionObj = actionObjects[Random.Range(0, actionObjects.Length)];

        var sentence = obj.actionObj.presentSimpleSentence;

        if (!wordToObject.ContainsKey(sentence))
        {
            wordToObject.Add(sentence, new List<GameObject>());
        }
        wordToObject[sentence].Add(obj.gameObject);
    }

}
