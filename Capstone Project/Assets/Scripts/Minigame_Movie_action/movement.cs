using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class movement : MonoBehaviour {

    public Vector2 direction;
    public float speed = 0f;

    public ActionObject actionObj;

    Sprite[] frames;
    float framesPerSecond = 5f;

    SpriteRenderer renderer;

    public SpriteRenderer circle;

    private void Start()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        direction = new Vector2(x, y).normalized;

        renderer = GetComponent<SpriteRenderer>();

        SetUp();
    }

    private void Update()
    {
        transform.Translate(direction * speed * 0.05f);

        int index = (int)(Time.time * framesPerSecond);
        index = index % frames.Length;
        renderer.sprite = frames[index];

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        direction = Vector2.Reflect(direction, collision.contacts[0].normal).normalized;
    }


    public void SetUp(ActionObject actionObj)
    {
        this.actionObj = actionObj;
        frames = actionObj.sprites;
        framesPerSecond = actionObj.frameRate;
    }

    public void SetUp()
    {
        frames = actionObj.sprites;
        framesPerSecond = actionObj.frameRate;
    }

    public void ShowCircle(Color color)
    {
        circle.enabled = true;
        circle.color = color;
    }

    public void HideCircle()
    {
        circle.enabled = false;
    }

}
