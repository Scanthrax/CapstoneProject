using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GamePanelButton : MonoBehaviour, IPointerDownHandler
{

    public bool right;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (right)
            GameSwipe.instance.SwipeRight();
        else
            GameSwipe.instance.SwipeLeft();
    }

}
