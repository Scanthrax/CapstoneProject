

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Windows.Speech;
//using UnityEngine.UI;


//public class Game2SpeechController : MonoBehaviour
//{

//    public static Game2SpeechController instance;

//    public string[] keywords;
//    public ConfidenceLevel confidence = ConfidenceLevel.Low;


//    KeywordRecognizer recognizer;



//    private void Awake()
//    {
//        instance = this;
//    }


//    void Start()
//    {
//        //if there are action objects, make an array of strings
//        if (Game2Controller.instance.listOfActions.Count > 0)
//        {
//            List<string> tempList = new List<string>();

//            for (int i = 0; i < Game2Controller.instance.listOfActions.Count; i++)
//            {
//                tempList.Add(Game2Controller.instance.listOfActions[i].commandSentence);
//                tempList.Add(Game2Controller.instance.listOfActions[i].negateSentence);
//            }

//            keywords = tempList.ToArray();
//            recognizer = new KeywordRecognizer(keywords, confidence);
//            recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
//            recognizer.Start();
//        }
//        else
//            print("NO KEYWORDS");
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

//        Game2Controller.instance.MakeGuess(word);

//        print("Recognized: " + word);
//    }
//}
