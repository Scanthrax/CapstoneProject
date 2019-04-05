using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloTextController : MonoBehaviour
{
    public int timerMax;
    public int screenWidth;
    public int screenHeight;
    public GameObject go;

    private int timer;
    private string[] helloList = new string[5];
    private Vector3 center = new Vector3(481.5f,228f,0f);

    // Start is called before the first frame update
    void Start()
    {
        timerMax = 90;
        timer = timerMax;

        helloList[0] = "Hello";
        helloList[1] = "Bonjour";
        helloList[2] = "Hola";
        helloList[3] = "Salve";
        helloList[4] = "Nǐn hǎo";
        helloList[5] = "Guten Tag";
        helloList[6] = "Olá";
        helloList[7] = "Anyoung";
        helloList[8] = "Ahlan";
        helloList[9] = "Konnichiwa";

        makeText();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer == 0)
        {
            makeText();
            timer = timerMax;
        }
        else
        {
            timer--;
        }
    }

    void makeText()
    {
        center = new Vector3(481.5f + screenWidth * Random.Range(-1.0f, 1.2f), 228f + screenHeight * Random.Range(-1.0f, 1.2f), 0f);
        GameObject o = Instantiate(go, center, Quaternion.identity);
        int h = Random.Range(0, 10);
        o.GetComponent<HelloTextScr>().myHello = helloList[h];
        o.transform.SetParent(this.transform.parent);
    }
}
