using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class SetText : MonoBehaviour
{

    public string text;


    private void Awake()
    {
        LocalizationManager.TextLocalized += Localize;
    }


    public void Localize(object source, EventArgs args)
    {
        if (!LocalizationManager.instance.localizedText.ContainsKey(text))
            print("error " + text);
        else
        GetComponent<Text>().text = LocalizationManager.instance.localizedText[text];
    }




}
