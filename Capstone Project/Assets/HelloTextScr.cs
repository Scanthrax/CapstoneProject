using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelloTextScr : MonoBehaviour
{
    public int lifetime;
    private int opac;
    public string myHello;

    // Start is called before the first frame update
    void Start()
    {
        lifetime = 300;
        opac = 0;
        this.GetComponent<Text>().fontSize = Random.Range(40,90);
        this.GetComponent<Text>().text = myHello;
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<RectTransform>().localPosition += Vector3.left/2;

        if (lifetime > 0)
        {
            lifetime--;
            if (opac < 100)
            {
                opac++;
            }
        }
        else
        {
            if (opac > 0)
            {
                opac--;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        this.GetComponent<CanvasGroup>().alpha = 0.95f * ((float)opac / 100.0f);
    }
}
