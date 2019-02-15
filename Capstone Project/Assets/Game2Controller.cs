using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game2Controller : MonoBehaviour
{

    public static Game2Controller instance;

    public Transform arrow;

    public Transform[] positions;

    public int posTracker;

    public bool recognizedNewWord;
    public string wordRecognized;

    public Dictionary<string, Vector3> wordToPosition = new Dictionary<string, Vector3>();


    public Text timerText;
    public float timer = 10f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        posTracker = 0;

        for (int i = 0; i < Game2SpeechController.instance.keywords.Length-1; i++)
        {
            if(Game2SpeechController.instance.keywords[i] != null || positions[i].position != null)
                wordToPosition.Add(Game2SpeechController.instance.keywords[i], positions[i].position);
        }
    }


    private void Update()
    {
        if(recognizedNewWord)
        {
            recognizedNewWord = false;
            arrow.position = wordToPosition[wordRecognized];
        }

        timer -= Time.deltaTime;

        if (timer <= 0f)
            timer = 10f;

        timerText.text = Mathf.CeilToInt(timer).ToString();

    }
}
