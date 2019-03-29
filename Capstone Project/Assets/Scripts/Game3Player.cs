using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game3Player : MonoBehaviour
{
    public RectTransform playerNumberTag;
    public bool selectedPath;
    public Text text;
    public int i;
    public Color color;
    private void Start()
    {
        playerNumberTag.SetParent(transform.parent);
    }

    private void OnValidate()
    {
        text.text = i.ToString();
        text.color = color;
    }

}
