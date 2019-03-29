using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game3Controller : MonoBehaviour
{

    public enum Game3State { Choosing, ChoosingPause, DisplayAnswer}

    public Image action;

    float timer;
    public Text timerText;

    public Game3State state;



    public Game3Player[] players;



    public int amtInLeft, amtInRight;
    public RectTransform leftDoor, rightDoor;


    public int amtLeft;

    bool newState;


    public AnimationCurve movePlayersCurve;
    public float movePlayerSpeed;



    public bool winningSide;
    public Image leftImage, rightImage;
    public Sprite correctImage, incorrectImage;
    public GameObject checkmark;

    public RectTransform playerStart;

    public Text roundText;
    int roundNumber, maxRounds;

    private void Start()
    {
        state = Game3State.Choosing;
        amtLeft = 4;
        newState = true;
        roundNumber = 1;
        maxRounds = 5;
        
    }


    private void Update()
    {
        if (state == Game3State.Choosing)
        {
            if(newState)
            {
                newState = false;
                amtInLeft = 4;
                timer = 10f;


                amtInLeft = 0;
                amtInRight = 0;
                amtLeft = 4;

                foreach (var item in players)
                {
                    item.selectedPath = false;
                }

                PlaceImages();

                roundText.text = "Round " + roundNumber;

                PlacePlayers();

            }

            UpdateTimer();

                

            for (int i = 1; i < players.Length; i++)
            {
                if(!players[i].selectedPath)
                {
                    var randomChance = Random.value;
                    if (randomChance < 0.005f)
                    {
                        var randomSide = Random.value < 0.5f ? true : false;

                        if(randomSide)
                        {
                            
                            players[i].playerNumberTag.localPosition = leftDoor.localPosition + new Vector3(amtInLeft * 100,0,0);
                            players[i].selectedPath = true;
                            amtInLeft++;
                        }
                        else
                        {
                            
                            players[i].playerNumberTag.localPosition = rightDoor.localPosition + new Vector3(amtInRight * 100, 0, 0);
                            players[i].selectedPath = true;
                            amtInRight++;
                        }
                        amtLeft--;
                    }
                }
            }

            if(amtLeft == 0)
            {
                state = Game3State.ChoosingPause;
                newState = true;
                timerText.text = "";

            }



        }
        else if (state == Game3State.ChoosingPause)
        {
            if(newState)
            {
                newState = false;

                for (int i = 0; i < players.Length; i++)
                {
                    StartCoroutine(MoveCharacters(players[i]));
                }

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
            if(newState)
            {
                newState = false;
                AudioManager.instance.PlayCorrectSound();
                timer = 2f;

                checkmark.SetActive(true);
                checkmark.transform.localPosition = winningSide ? leftImage.rectTransform.localPosition : rightImage.rectTransform.localPosition;
            }

            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                state = Game3State.Choosing;
                newState = true;
                roundNumber++;
            }

        }
    }


    void PlacePlayers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.localPosition = playerStart.localPosition + new Vector3(i * 100, 0);
            players[i].playerNumberTag.localPosition = players[i].transform.localPosition + new Vector3(0, 200, 0);
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
                if (!players[i].selectedPath)
                {
                    var randomSide = Random.value < 0.5f ? true : false;

                    if (randomSide)
                    {

                        players[i].playerNumberTag.localPosition = leftDoor.localPosition + new Vector3(amtInLeft * 100, 0, 0);
                        players[i].selectedPath = true;
                        amtInLeft++;
                    }
                    else
                    {

                        players[i].playerNumberTag.localPosition = rightDoor.localPosition + new Vector3(amtInRight * 100, 0, 0);
                        players[i].selectedPath = true;
                        amtInRight++;
                    }
                    amtLeft--;
                }
            }
        }
    }





    IEnumerator MoveCharacters(Game3Player player)
    {
        var rect = player.GetComponent<RectTransform>();

        Vector3 startingPos = rect.localPosition;

        Vector3 endPos = player.playerNumberTag.localPosition + Vector3.down * 500f;

        float moveTimer = 0f;
        float percent = 0f;

        while (moveTimer < movePlayerSpeed)
        {
            moveTimer += Time.deltaTime;

            percent = moveTimer / movePlayerSpeed;

            rect.localPosition = Vector3.Lerp(startingPos, endPos, movePlayersCurve.Evaluate(percent));

            yield return null;
        }


    }


    void PlaceImages()
    {
        winningSide = Random.value < 0.5f ? true : false;
        leftImage.sprite = winningSide ? correctImage : incorrectImage;
        rightImage.sprite = winningSide ? incorrectImage : correctImage;

        checkmark.SetActive(false);
    }

}
