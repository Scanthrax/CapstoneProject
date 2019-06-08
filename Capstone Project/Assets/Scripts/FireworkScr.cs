using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkScr : MonoBehaviour
{
    public Vector3 pos;
    public Vector3 pos2;
    public int timer;
    public GameObject explode;

    // Start is called before the first frame update
    void Start()
    {
        timer = 60;

        pos = this.transform.position;
        pos2 = this.transform.position + new Vector3(0.0f, 6.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, pos2, 0.05f);

        if (timer > 0)
        {
            timer--;
            Debug.Log(timer);
        }
        else
        {
            Instantiate(explode, this.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
