using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChoosePath : MonoBehaviour, IPointerDownHandler
{
    public Game3Controller ctrl;
    public bool left;
    public void OnPointerDown(PointerEventData eventData)
    {
        if(!ctrl.players[0].selectedPath)
        {
            RectTransform trans = left ? ctrl.leftDoor : ctrl.rightDoor;
            var x = left ? ctrl.amtInLeft : ctrl.amtInRight;
            ctrl.players[0].playerNumberTag.localPosition = trans.localPosition + new Vector3(x * 100, 0, 0);
            ctrl.players[0].selectedPath = true;
            if (left)
            {
                ctrl.amtInLeft++;
            }
            else
            {
                ctrl.amtInRight++;
            }
            ctrl.amtLeft--;
        }
    }
}
