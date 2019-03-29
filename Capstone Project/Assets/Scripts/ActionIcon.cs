using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionIcon : MonoBehaviour
{
    public ActionObject action;


    private void Start()
    {
        //GetComponent<SpriteRenderer>().sprite = action.sprites[0];
    }


    private void OnValidate()
    {
        GetComponent<SpriteRenderer>().sprite = action.sprites[0];
        GetComponent<ActionAnimate>().actionObj = action;
    }

    public void Init(ActionObject action)
    {
        print("INIT");
        this.action = action;

        GetComponent<SpriteRenderer>().sprite = action.sprites[0];
        GetComponent<ActionAnimate>().actionObj = action;
        GetComponent<ActionAnimate>().UpdateSprites();
    }
}
