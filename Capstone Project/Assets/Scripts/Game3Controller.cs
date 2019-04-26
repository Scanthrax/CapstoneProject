using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game3Controller : MonoBehaviour
{

    public enum Game3State { Choosing, ChoosingPause, DisplayAnswer}

    public SpriteRenderer action;

    float timer;
    public TextMeshPro timerText;

    public Game3State state;



    public Game1Player[] players;



    //public int amtInLeft, amtInRight;

    public int amtInLeft
    {
        get
        {
            return leftPath.Count;
        }
    }
    public int amtInRight
    {
        get
        {
            return rightPath.Count;
        }
    }



    public Transform leftDoor, rightDoor;


    public int amtLeft;

    bool newState;


    public AnimationCurve movePlayersCurve;
    public float movePlayerSpeed;



    public bool winningSide;
    public SpriteRenderer leftImage, rightImage;
    public Sprite correctImage, incorrectImage;
    public Transform checkmark;

    public Vector3[] startPositions;

    public TextMeshPro roundText;
    int roundNumber, maxRounds;

    bool endGame, gameRunning;

    public List<ActionObject> listOfNouns;
    public PoseToNoun correctObj;


    public List<PoseToNoun> poseToNouns;

    public List<Game1Player> leftPath, rightPath;

    public List<Sprite> answerPanels;

    public AudioSource endGameSource;

    public TextMeshPro startText;

    [System.Serializable]
    public struct PoseToNoun
    {
        public Sprite pose;
        public List<Sprite> correctAnswers;
    }


    private void Start()
    {




        state = Game3State.Choosing;
        amtLeft = 4;
        newState = true;
        roundNumber = 1;
        maxRounds = 5;

        endGame = false;
        gameRunning = false;

        if (MenuSelection.instance)
        {
            MenuSelection.instance.FadeIn(1f);
        }


        if (IntroController.instance)
        {
            var listOfplayers = IntroController.instance.charactersInGame;

            for (int i = 0; i < listOfplayers.Count; i++)
            {
                players[i].character = listOfplayers[i];
                players[i].ChangeSprite();
            }
        }

        if (AudioManager.instance)
        {
            AudioManager.instance.musicSource.clip = AudioManager.instance.game1;
            AudioManager.instance.musicSource.Play();
        }

        startPositions = new Vector3[4];
        foreach (var item in players)
        {
            startPositions[item.i] = item.transform.position;
        }

        InitGame();

    }


    private void Update()
    {
        if (gameRunning)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                roundNumber = 6;
            }


            if (state == Game3State.Choosing)
            {
                if (newState)
                {
                    newState = false;
                    timer = 10f;


                    leftPath.Clear();
                    rightPath.Clear();
                    amtLeft = 4;

                    foreach (var item in players)
                    {
                        item.guess = false;
                    }

                    PlaceImages();

                    roundText.text = "Round " + roundNumber;

                    PlacePlayers();

                }

                UpdateTimer();



                for (int i = 1; i < players.Length; i++)
                {
                    if (!players[i].guess)
                    {
                        var randomChance = Random.value;
                        if (randomChance < 0.005f)
                        {
                            var randomSide = Random.value < 0.5f ? true : false;

                            if (randomSide)
                            {
                                players[i].guess = true;

                                StartCoroutine(MoveCharacters(players[i], leftDoor.position + Vector3.right * (amtInLeft * 0.45f)));

                                //players[i].transform.position = leftDoor.position + Vector3.right * (amtInLeft * 0.45f);
                                //players[i].transform.localScale = Vector3.one * 0.35f;
                                leftPath.Add(players[i]);
                                
                            }
                            else
                            {

                                players[i].guess = true;

                                StartCoroutine(MoveCharacters(players[i], rightDoor.position + Vector3.left * (amtInRight * 0.45f)));

                                //players[i].transform.position = rightDoor.position + Vector3.left * (amtInRight * 0.45f);
                                //players[i].transform.localScale = Vector3.one * 0.35f;
                                rightPath.Add(players[i]);
                                
                            }
                            amtLeft--;
                        }
                    }
                }

                if (amtLeft == 0)
                {
                    state = Game3State.ChoosingPause;
                    newState = true;
                    timerText.text = "";

                }



            }
            else if (state == Game3State.ChoosingPause)
            {
                if (newState)
                {
                    newState = false;


                    timer = 2f;

                }

                timer -= Time.deltaTime;

                if (timer <= 0f)
                {
                    state = Game3State.DisplayAnswer;
                    newState = true;
                }


            }
            else if (state == Game3State.DisplayAnswer)
            {
                if (newState)
                {
                    newState = false;
                    if (AudioManager.instance)
                        AudioManager.instance.PlayCorrectSound();
                    timer = 2f;

                    checkmark.gameObject.SetActive(true);
                    checkmark.transform.position = winningSide ? leftImage.transform.position : rightImage.transform.position;

                    foreach (var item in winningSide? leftPath:rightPath)
                    {
                        item.UpdateScore(10);
                    }

                }

                timer -= Time.deltaTime;

                if (timer <= 0f)
                {
                    if (roundNumber > 4 && !endGame)
                    {
                        endGame = true;
                        print("ENDING GAME");
                        EndGame();
                    }

                    if (!endGame)
                    {
                        roundNumber++;
                        state = Game3State.Choosing;
                        newState = true;
                    }

                }

            }
        }
    }

    public void SetRound(int round)
    {
        roundNumber = round;
    }

    void EndGame()
    {
        if (gameRunning)
        {
            gameRunning = false;
            MenuSelection.instance.GoToMenuScene(3f, MenuSelection.instance.selectedMinigame.sceneName);
            print(MenuSelection.instance.selectedMinigame.sceneName);
            startText.gameObject.SetActive(false);
            startText.text = "Finish!";
            MenuSelection.instance.selectedMinigame.Scores.Add(players[0].score);
            endGameSource.Play();
        }

    }


    void PlacePlayers()
    {
        foreach (var item in players)
        {
            item.transform.localScale = Vector3.one;
            item.transform.position = startPositions[item.i];
        }
    }




    void UpdateTimer()
    {
        timer -= Time.deltaTime;

        if(timer >= 0f)
            timerText.text = Mathf.CeilToInt(timer).ToString();
        else
        {
            timerText.text = "Time's Up!";
            state = Game3State.ChoosingPause;
            newState = true;


            for (int i = 0; i < players.Length; i++)
            {
                if (!players[i].guess)
                {
                    var randomSide = Random.value < 0.5f ? true : false;

                    if (randomSide)
                    {
                        players[i].guess = true;

                        StartCoroutine(MoveCharacters(players[i], leftDoor.position + Vector3.right * (amtInLeft * 0.45f)));

                        //players[i].transform.position = leftDoor.position + Vector3.right * (amtInLeft * 0.45f);
                        //players[i].transform.localScale = Vector3.one * 0.35f;
                        leftPath.Add(players[i]);
                    }
                    else
                    {
                        players[i].guess = true;

                        StartCoroutine(MoveCharacters(players[i], rightDoor.position + Vector3.left * (amtInRight * 0.45f)));

                        //players[i].transform.position = rightDoor.position + Vector3.left * (amtInRight * 0.45f);
                        //players[i].transform.localScale = Vector3.one * 0.35f;
                        rightPath.Add(players[i]);
                    }
                    amtLeft--;
                }
            }
        }
    }





    public IEnumerator MoveCharacters(Game1Player player, Vector3 dest)
    {

        Vector3 startingPos = player.transform.position;

        Vector3 endPos = dest;

        //Vector3 endPos = ????

        float moveTimer = 0f;
        float percent = 0f;

        while (moveTimer < movePlayerSpeed)
        {
            moveTimer += Time.deltaTime;

            percent = moveTimer / movePlayerSpeed;

            player.transform.position = Vector3.Lerp(startingPos, endPos, movePlayersCurve.Evaluate(percent));
            player.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 0.35f, movePlayersCurve.Evaluate(percent));

            yield return null;
        }


    }


    void PlaceImages()
    {
        winningSide = Random.value < 0.5f ? true : false;

        correctObj = poseToNouns[Random.Range(0, poseToNouns.Count)];

        correctImage = correctObj.correctAnswers[Random.Range(0, correctObj.correctAnswers.Count)];

        incorrectImage = answerPanels[Random.Range(0, answerPanels.Count)];

        for (int i = 0; i < correctObj.correctAnswers.Count; i++)
        {
            if (incorrectImage == correctImage)
            {
                incorrectImage = answerPanels[Random.Range(0, answerPanels.Count)];
                i = 0;
            }
        }


        leftImage.sprite = winningSide ? correctImage : incorrectImage;
        rightImage.sprite = winningSide ? incorrectImage : correctImage;

        checkmark.gameObject.SetActive(false);

        action.sprite = correctObj.pose;
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

        startText.gameObject.SetActive(false);
    }

}
