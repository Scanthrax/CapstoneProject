﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    public Text amtOfPlayers, titleOfGame, topicOfGame;
    public Image image;
    public int position;

    public MinigameObject minigame;

    public void NextPosition(bool right)
    {
        position += right ? 1 : -1;

        if (position >= 5)
            position = 0;
        else if (position < 0)
            position = 4;
    }



    public void UpdateMinigame(MinigameObject newMinigame)
    {
        minigame = newMinigame;
    }
}