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
        if(!ctrl.players[0].guess)
        {
            Vector3 trans = left ? ctrl.leftDoor.position : ctrl.rightDoor.position;
            var x = left ? ctrl.amtInLeft : ctrl.amtInRight;
            ctrl.players[0].guess = true;
            if (left)
            {
                ctrl.players[0].guess = true;
                ctrl.players[0].transform.position = ctrl.leftDoor.position + Vector3.right * (ctrl.amtInLeft * 0.45f);
                ctrl.players[0].transform.localScale = Vector3.one * 0.35f;
                ctrl.leftPath.Add(ctrl.players[0]);
            }
            else
            {
                ctrl.players[0].guess = true;
                ctrl.players[0].transform.position = ctrl.rightDoor.position + Vector3.left * (ctrl.amtInRight * 0.45f);
                ctrl.players[0].transform.localScale = Vector3.one * 0.35f;
                ctrl.rightPath.Add(ctrl.players[0]);
            }
            ctrl.amtLeft--;
        }
    }
}
