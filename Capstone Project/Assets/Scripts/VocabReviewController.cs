using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VocabReviewController : MonoBehaviour
{

    public static VocabReviewController instance;

    private void Awake()
    {
        instance = this;
    }

    public List<Text> listOfTextBoxes;
    public List<ActionObject> listOfActionObjects;
    public Topic vocabType;

    public Vector2 scrollBounds;
    public Slider slider;
    public RectTransform textBoxContainer;
    public Image vocabImage;
    public Text selectedText;
    public Sprite empty;

    public Game3Items[] game1Items;
    public Game3Items[] game2Items;
    public Game3Items[] game3Items;


    Game3Items[] vocabDisplay;


    public Text topicText;

    [System.Serializable]
    public struct Game3Items
    {
        public string sentence;
        public Sprite image;
    }

    private void Start()
    {
        vocabImage.sprite = empty;
        selectedText.text = "";

        vocabDisplay = game1Items;
    }


    [ContextMenu("function")]
    public void UpdateTextBoxes()
    {



        //switch (vocabType)
        //{
        //    case Topic.PresentSimple:

        //        for (int i = 0; i < listOfActionObjects.Count; i++)
        //        {
        //            listOfTextBoxes[i].text = listOfActionObjects[i].presentSimpleSentence;
        //        }
        //        break;
        //    case Topic.Commands:
        //        for (int i = 0; i < listOfActionObjects.Count; i++)
        //        {
        //            listOfTextBoxes[i].text = listOfActionObjects[i].commandSentence;
        //        }
        //        break;
        //    case Topic.PastSimple:
        //        for (int i = 0; i < listOfActionObjects.Count; i++)
        //        {
        //            listOfTextBoxes[i].text = listOfActionObjects[i].negateSentence;
        //        }
        //        break;
        //    default:
        //        break;
        //}



        switch (vocabType)
        {
            case Topic.PresentSimple:
                vocabDisplay = game1Items;
                topicText.text = "Present Simple";
                break;
            case Topic.Commands:
                vocabDisplay = game2Items;
                topicText.text = "Commands";
                break;
            case Topic.PastSimple:
                vocabDisplay = game3Items;
                topicText.text = "Past Simple";
                break;
            default:
                break;
        }

        for (int i = 0; i < 5; i++)
        {
            listOfTextBoxes[i].text = vocabDisplay[i].sentence;
        }

    }




    public void ScrollTextBoxes()
    {
        textBoxContainer.localPosition = new Vector2(0,scrollBounds.x + (slider.value * (scrollBounds.y - scrollBounds.x)));
        //print(slider.value);
    }

    public void ChangeImage(int i)
    {
        vocabImage.sprite = vocabDisplay[i].image;
        selectedText.text = vocabDisplay[i].sentence;
    }

}
