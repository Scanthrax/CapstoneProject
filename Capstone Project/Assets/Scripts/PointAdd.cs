using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointAdd : MonoBehaviour
{
    TextMeshPro tmpro;
    Color color;

    private void Start()
    {
        tmpro = GetComponent<TextMeshPro>();
        color = tmpro.color;
    }

    // Update is called once per frame
    void Update()
    {
        color.a -= Time.deltaTime * 0.8f;

        tmpro.color = color;

        transform.Translate(0, Time.deltaTime * 0.65f, 0);

        if (color.a <= 0f)
            Destroy(gameObject);
    }
}
