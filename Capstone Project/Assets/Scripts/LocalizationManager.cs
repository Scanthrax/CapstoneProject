using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LocalizationManager : MonoBehaviour {

    public static LocalizationManager instance;

    public Dictionary<string, string> localizedText;


    void Awake()
    {
        if (instance)
        {
            DestroyImmediate(this);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }


    public delegate void LocalizationEventHandler(object source, EventArgs args);
    public static event LocalizationEventHandler TextLocalized;


    protected virtual void OnTextLocalized()
    {

        TextLocalized(this, EventArgs.Empty);
            
    }

	public void LoadLocalizedText(string fileName)
    {
        localizedText = new Dictionary<string, string>();

        string filePath = Application.streamingAssetsPath + @"\Localization\" + fileName;
        print(filePath);

        if(File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
            for (int i = 0; i < loadedData.items.Length; i++)
            {
                localizedText.Add(loadedData.items[i].key, loadedData.items[i].phrase);
            }
            print("Data loaded, dictionary contains " + localizedText.Count + " entries");
            OnTextLocalized();
        }
        else
        {
            print("Cannot find localization file!");
        }
    }
}
