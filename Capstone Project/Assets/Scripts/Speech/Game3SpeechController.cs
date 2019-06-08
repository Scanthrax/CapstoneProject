//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Windows.Speech;

//public class Game3SpeechController : MonoBehaviour
//{

//    public static Game3SpeechController instance;

//    public string[] keywords;
//    public ConfidenceLevel confidence = ConfidenceLevel.Low;


//    KeywordRecognizer recognizer;



//    private void Awake()
//    {
//        instance = this;
//    }


//    void Start()
//    {
//        keywords = Game3Controller.instance.answerWords.ToArray();

//        recognizer = new KeywordRecognizer(keywords, confidence);

//        recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
//        recognizer.Start();
//    }



//    private void OnApplicationQuit()
//    {
//        if (recognizer != null && recognizer.IsRunning)
//        {
//            recognizer.OnPhraseRecognized -= Recognizer_OnPhraseRecognized;
//            recognizer.Stop();
//        }
//    }

//    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
//    {
//        //translates the result into text
//        var word = args.text;

//        Game3Controller.instance.MakeGuess(word);

//        print("Recognized: " + word);
//    }



//}