using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanCamera : MonoBehaviour
{
    // reference to camera
    Camera cam;

    // offset that pulls camera back from capsules
    public Vector3 camOffset = new Vector3(0, 3, -10);

    // offset between each capsule
    float spawnOffset = 10f;

    // amount of capsules to spawn
    public int amtOfPlayers = 3;

    // capsule prefab to spawn
    public GameObject playerPrefab;

    // point to begin spawning capsules
    public Transform spawnPoint;

    // references to each capsule
    public GameObject[] players;

    // curve that controls camera panning
    public AnimationCurve animCurve;

    // is the camera panning?
    bool cameraPanning = false;

    // which capsule does the camera target?
    int camTarget = -1;

    // duration (seconds) between camera transitions
    [SerializeField]
    public float duration = 1f;

    IntroState state = IntroState.CameraPan;

    public KeyCode nextState = KeyCode.Q;

    public CharacterObject[] characters;

    public GameObject textPanel;
    Text text;

    Vector3 spawnPointCamera;

    public string[] adjectives;

    public float timer = 4f;

    bool timerEnd = false;

    bool canGoToNextScene = false;
    void Start ()
    {
        if(StaticVariables.minigame != null)
            amtOfPlayers = StaticVariables.minigame.AmountOfPlayers;


        // get reference to camera
        cam = Camera.main;

        // initialize array
        players = new GameObject[amtOfPlayers];

        text = textPanel.GetComponentInChildren<Text>();

        // spawn the appropriate amount of capsules
        for (int i = 0; i < amtOfPlayers; i++)
        {
            // store reference of each instanciated capsule
            players[i] = Instantiate(playerPrefab,
                // the x-position of spawn is determined by the spawn point's x-coord, the position in the array, and the offset between each capsule
                new Vector3(spawnPoint.transform.position.x + (i * spawnOffset),
                // set y & z
                spawnPoint.transform.position.y,
                spawnPoint.transform.position.z),
                // identity rotation
                Quaternion.identity);

            var capsule = players[i].GetComponent<Character>();
            capsule.character = characters[Random.Range(0, characters.Length)];
            print(capsule.character.name);
        }


        var midpoint = (players[0].transform.position.x + players[players.Length - 1].transform.position.x) / 2f;
        spawnPointCamera.x = midpoint;
        spawnPointCamera.y = amtOfPlayers + 2f;
        spawnPointCamera.z = -35f * (amtOfPlayers/2f);

        cam.transform.position = spawnPointCamera;
    }

    void Update()
    {
        switch(state)
        {
            case IntroState.Applause:
                if (timerEnd)
                {
                    state = IntroState.CameraPan;
                    NextCameraTarget();
                    textPanel.SetActive(false);
                    timerEnd = false;
                }
                break;
            case IntroState.CameraPan:
                // we do not want to execute the coroutine while in a coroutine
                if (!cameraPanning)
                {
                    // pan the camera to the next target
                    if (camTarget >= 0)
                    {
                        MoveTo(cam.gameObject, players[camTarget].transform.position + camOffset, duration);
                    }
                    else
                    {
                        MoveTo(cam.gameObject, spawnPointCamera, duration);
                    }
                }
                break;
        }


        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


    void NextCameraTarget()
    {
        camTarget++;
        if(camTarget == amtOfPlayers)
        {
            camTarget = -1;
        }
    }

    void MoveTo(GameObject obj, Vector3 target, float duration)
    {
        StartCoroutine(AnimateMove(obj, obj.transform.position, target, duration));
    }


    void StartTimer(float duration)
    {
        StartCoroutine(cTimer(duration));
    }

    public IEnumerator cTimer(float _endTime)
    {
        float timer = 0f;
        while (timer < _endTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        timerEnd = true;
    }


    IEnumerator AnimateMove(GameObject obj, Vector3 origin, Vector3 target, float duration)
    {
        cameraPanning = true;
        float journey = 0f;
        while (journey <= duration)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / duration);

            float curvePercent = animCurve.Evaluate(percent);
            obj.transform.position = Vector3.LerpUnclamped(origin, target, curvePercent);

            yield return null;
        }

        cameraPanning = false;
        state = IntroState.Applause;
        StartTimer(timer);
        textPanel.SetActive(true);
        if (camTarget >= 0)
        {
            var charTemp = players[camTarget].GetComponent<Character>().character;
            string s1 = charTemp.name;
            string s2 = " the ";
            string s3 = adjectives[Random.Range(0, adjectives.Length)];
            string s4 = "!";

            string s5 = string.Concat(new string[] {s1,s2,s3,s4});

            text.text = s5;

            if (!canGoToNextScene)
                canGoToNextScene = true;
        }
        else
        {
            textPanel.SetActive(false);

            if (canGoToNextScene)
            {
                GoToMinigame();
            }
        }
    }


    public void GoToMinigame()
    {
        SceneManager.LoadScene(StaticVariables.minigame.Scene.name);
        StaticVariables.minigame.TimesPlayed++;
    }
}
