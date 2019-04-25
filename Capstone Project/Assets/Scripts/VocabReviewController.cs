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



    private void Start()
    {
        vocabImage.sprite = empty;
        selectedText.text = "";
    }


    [ContextMenu("function")]
    public void UpdateTextBoxes()
    {



        switch (vocabType)
        {
            case Topic.PresentSimple:
                for (int i = 0; i < listOfActionObjects.Count; i++)
                {
                    listOfTextBoxes[i].text = listOfActionObjects[i].presentSimpleSentence;
                }
                break;
            case Topic.Commands:
                for (int i = 0; i < listOfActionObjects.Count; i++)
                {
                    listOfTextBoxes[i].text = listOfActionObjects[i].commandSentence;
                }
                break;
            case Topic.PastSimple:
                for (int i = 0; i < listOfActionObjects.Count; i++)
                {
                    listOfTextBoxes[i].text = listOfActionObjects[i].negateSentence;
                }
                break;
            default:
                break;
        }




    }




    public void ScrollTextBoxes()
    {
        textBoxContainer.localPosition = new Vector2(0,scrollBounds.x + (slider.value * (scrollBounds.y - scrollBounds.x)));
        //print(slider.value);
    }

    public void ChangeImage(int i)
    {
        string temp = "";
        switch (vocabType)
        {
            case Topic.PresentSimple:
                    temp = listOfActionObjects[i].presentSimpleSentence;
                break;
            case Topic.Commands:
                    temp = listOfActionObjects[i].commandSentence;
                break;
            case Topic.PastSimple:
                    temp = listOfActionObjects[i].negateSentence;
                break;
            default:
                break;
        }


        vocabImage.sprite = listOfActionObjects[i].sprites[0];
        selectedText.text = temp;
    }

}
