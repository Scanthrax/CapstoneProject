

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;

public struct Sentence
{
    private string[] _sentence;

    public int Length { get { return _sentence.Length; } }

    public Sentence(int i)
    {
        _sentence = new string[i];
    }

    void ClearSentence()
    {
        for (int i = 0; i < _sentence.Length - 1; i++)
        {
            _sentence[i] = "";
        }
    }

    bool isEmpty()
    {
        return _sentence[0] == "";
    }
    bool isEmpty(int i)
    {
        return _sentence[i] == "";
    }

    public void SetWord(int i, string s)
    {
        _sentence[i] = s;
    }

    public string Print()
    {
        var temp = "";

        for (int i = 0; i < _sentence.Length; i++)
        {
            if (isEmpty(i)) break;
            var space = " ";
            if (i == 0 || i == _sentence.Length) space = "";
            temp = string.Concat(temp, space, _sentence[i]);
        }
        return temp;
    }
}

public class SpeechController : MonoBehaviour {

    string[] keywords;
    public ConfidenceLevel confidence = ConfidenceLevel.Low;


    KeywordRecognizer recognizer;

    

    void Start ()
    {
        //if there are action objects, make an array of strings
        if (Controller.instance.actionObjects.Length > 0)
        {
            keywords = new string[Controller.instance.actionObjects.Length];

            for (int i = 0; i < keywords.Length; i++)
            {
                keywords[i] = Controller.instance.actionObjects[i].sentence;
                print(keywords[i]);
            }

            recognizer = new KeywordRecognizer(keywords, confidence);
            recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
            recognizer.Start();
        }
        else
            print("NO KEYWORDS");
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

        Controller.instance.recognizedNewWord = true;
        Controller.instance.wordRecognized = word;

        print("Recognized: " + word);
    }
}
