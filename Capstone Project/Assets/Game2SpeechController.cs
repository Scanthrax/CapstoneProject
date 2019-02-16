

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;


public class Game2SpeechController : MonoBehaviour
{

    public string[] keywords;
    public ConfidenceLevel confidence = ConfidenceLevel.Low;


    KeywordRecognizer recognizer;



    void Start()
    {
        recognizer = new KeywordRecognizer(keywords, confidence);
        recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
        recognizer.Start();
    }



    private void OnApplicationQuit()
    {
        if (recognizer != null && recognizer.IsRunning)
        {
            recognizer.OnPhraseRecognized -= Recognizer_OnPhraseRecognized;
            recognizer.Stop();
        }
    }

    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        //translates the result into text
        var word = args.text;

        Game2Controller.instance.recognizedNewWord = true;
        Game2Controller.instance.wordRecognized = word;

        print("Recognized: " + word);
    }
}
