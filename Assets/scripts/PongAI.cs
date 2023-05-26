using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongAI : MonoBehaviour
{
    private Rigidbody2D paddle;
    public float speed;
    private GameObject ballObject;
    private Vector3 worldCenter;
    void Start()
    {
        paddle = GetComponent<Rigidbody2D>();
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        worldCenter = Camera.main.ScreenToWorldPoint(screenCenter);
        worldCenter.z = 0f;
        ballObject = GameObject.FindGameObjectWithTag("Ball");
    }

    private void FixedUpdate()
    {
        if (ballObject.transform.position.y > worldCenter.y)
            paddle.velocity = new Vector2(paddle.velocity.x, paddle.velocity.y + 1);
        if (ballObject.transform.position.y < worldCenter.y)
            paddle.velocity = new Vector2(paddle.velocity.x, paddle.velocity.y - 1);
    }
    void Update()
    {
    }
}
