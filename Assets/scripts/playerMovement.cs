using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D paddle;

    void Start()
    {
        paddle = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        paddle.velocity = new Vector2( horizontalInput * speed, paddle.velocity.y );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            paddle.velocity = Vector2.zero;
    }
}
