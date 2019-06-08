using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pointer : MonoBehaviour
{
    Image imageRenderer;


    private void Start()
    {
        imageRenderer = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        imageRenderer.color = new Color(1, 1, 1, Mathf.Sin(Time.time * 2f)* 0.5f + 0.5f);
        transform.localScale = Vector3.one * (Mathf.Sin(Time.time * 2f) * 0.2f + 0.8f);
    }
}
