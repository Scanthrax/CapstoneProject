using System.Collections;
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
    public Game1Player[] players;
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

    public AudioSource correctAnswerSource;


    public Image fadeScreen;


    public List<CharacterObject> listOfCharacters;

    public AudioSource endGame;

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



        for (int i = 0; i < 4; i++)
        {
            print("here");
            players[i].character = listOfCharacters[i];
            players[i].ChangeSprite();
        }

        InitGame();


        var outline = Instantiate(new GameObject(), players[0].transform.position, Quaternion.identity, players[0].transform);
        outline.transform.localScale += Vector3.one * 0.1f;
        outline.transform.position += Vector3.back * 0.1f + Vector3.down * 0.12f;
        var outlineRend = outline.AddComponent<SpriteRenderer>();
        outlineRend.sprite = players[0].character.silhouette;
        outlineRend.color = Color.yellow;
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
                    players[0].StartWaving();
                }
                
            }

        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            players[0].StartWaving();
            //RandomPosition(actionObjects[0].presentSimpleSentence);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            players[1].StartWaving();
            //RandomPosition();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            players[2].StartWaving();
            //RandomPosition();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            players[3].StartWaving();
            //RandomPosition();
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
                for (int i = 1; i < 4; i++)
                {
                    if (!players[i].waving)
                    {
                        if (Random.value <= 0.1)
                        {
                            players[i].StartWaving();
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
                    item.score += 10;
                    item.scoreText.text = item.score.ToString();
                    PlaySoundCorrect();
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
            MenuSelection.instance.GoToMenuScene(3f, MenuSelection.instance.selectedMinigame.Scene.name);
            print(MenuSelection.instance.selectedMinigame.Scene.name);
            timerText.gameObject.SetActive(false);
            startText.gameObject.SetActive(true);
            startText.GetComponent<Text>().text = "Finish!";
            endGame.Play();
        }
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

            var sentence = obj.actionObj.presentSimpleSentence;

            if (!wordToObject.ContainsKey(sentence))
            {
                wordToObject.Add(sentence, new List<GameObject>());
            }
            wordToObject[sentence].Add(obj.gameObject);

        }

        gameRunning = true;

        startText.SetActive(false);
    }


}
