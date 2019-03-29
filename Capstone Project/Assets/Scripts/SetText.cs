using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class SetText : MonoBehaviour
{

    public string textTag;

    Text text;

    private void Awake()
    {
        LocalizationManager.TextLocalized += Localize;

        text = GetComponent<Text>();
    }


    public void Localize(object source)
    {
        if (!LocalizationManager.instance.localizedText.ContainsKey(textTag))
            print("error " + text);
        else
            text.text = LocalizationManager.instance.localizedText[textTag];

    }




}
