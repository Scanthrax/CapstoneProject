using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Minigame", menuName = "Minigame")]
public class MinigameObject : ScriptableObject
{
    public string Name;
    public string Lesson;
    public Sprite Thumbnail;
    public Object Scene;
    public int AmountOfPlayers;

    public int TimesPlayed;
    private float PlayTime;
    public List<int> Scores = new List<int>();
}
