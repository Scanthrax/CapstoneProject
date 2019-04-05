using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LanguageSelectButton : MonoBehaviour, IPointerClickHandler
{
    public Image image;
    public MenuSelection.Language language;

    public void OnPointerClick(PointerEventData eventData)
    {
        MenuSelection.instance.languageSelectedImage.sprite = image.sprite;
        MenuSelection.instance.learningLanguage = language;
    }
}