

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Windows.Speech;
//using UnityEngine.UI;


//public class SpeechController : MonoBehaviour
//{

//    string[] keywords;
//    public ConfidenceLevel confidence = ConfidenceLevel.Low;


//    KeywordRecognizer recognizer;



//    void Start()
//    {
//        //if there are action objects, make an array of strings
//        if (Controller.instance.actionObjects.Length > 0)
//        {
//            keywords = new string[Controller.instance.actionObjects.Length];

//            for (int i = 0; i < keywords.Length; i++)
//            {
//                keywords[i] = Controller.instance.actionObjects[i].presentSimpleSentence;
//                print(keywords[i]);
//            }

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

//        Controller.instance.MakeGuess(word);

//        print("Recognized: " + word);
//    }
//}
