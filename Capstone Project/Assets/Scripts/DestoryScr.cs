using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryScr : MonoBehaviour
{
    public int timer;

    // Start is called before the first frame update
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        if (timer == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            timer--;
        }
    }
}
