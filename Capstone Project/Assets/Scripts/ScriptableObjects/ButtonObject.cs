using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "New Button Mapping", menuName = "Button")]
public class ButtonObject : ScriptableObject
{
    public GameObject from;
    public GameObject to;
    public Button button;
}
