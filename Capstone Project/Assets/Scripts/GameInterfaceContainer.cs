using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class GameInterfaceContainer : MonoBehaviour {

    public MinigameObject minigame;
    public Text title;
    public Text timesPlayed;
    public Image thumbnail;



    private void OnValidate()
    {
        if (minigame == null) return;
        title.text = minigame.Name;
        thumbnail.sprite = minigame.Thumbnail;
        timesPlayed.text = "Times Played: " + minigame.TimesPlayed;
    }

    public void GoToMinigame()
    {
        MenuSelection.goToScene = MenuSelection.introScene;
        MenuSelection.numPlayers = minigame.AmountOfPlayers;
        MenuSelection.goToMinigameScene = minigame.sceneName;
    }
}
