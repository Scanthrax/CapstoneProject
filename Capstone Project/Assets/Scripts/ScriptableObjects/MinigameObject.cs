using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum Minigames { Game1,Game2, Game3}
public enum Topic { PresentSimple,Commands, PastSimple}

[CreateAssetMenu(fileName = "New Minigame", menuName = "Minigame")]
public class MinigameObject : ScriptableObject
{
    public Minigames minigame;
    public Topic topic;
    public string Name;
    public string Lesson;
    public Sprite Thumbnail;
    public string sceneName;
    public int AmountOfPlayers;
    public string topicString;

    public int TimesPlayed;
    private float PlayTime;
    public List<int> Scores = new List<int>();
}
