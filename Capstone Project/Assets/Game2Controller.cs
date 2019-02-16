using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2Controller : MonoBehaviour
{

    public static Game2Controller instance;

    public Transform arrow;

    public Transform[] positions;

    public int posTracker;

    public bool recognizedNewWord;
    public string wordRecognized;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        posTracker = 0;
    }


    private void Update()
    {
        if(recognizedNewWord)
        {
            recognizedNewWord = false;

            switch(wordRecognized)
            {
                case "one":
                    arrow.position = positions[0].position;
                    break;
                case "two":
                    arrow.position = positions[1].position;
                    break;
                case "three":
                    arrow.position = positions[2].position;
                    break;
            }
        }
    }
}
