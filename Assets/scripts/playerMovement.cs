using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D paddle;

    void Start()
    {
        paddle = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        paddle.velocity = new Vector2( horizontalInput * speed, paddle.velocity.y );
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            float direction;
            if (touch.position.x > Screen.width / 2)
                direction = 1f;
            else
                direction = -1f;
            float xPos = transform.position.x + (direction * speed * Time.deltaTime);
            paddle.velocity = new Vector2(xPos, paddle.velocity.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            paddle.velocity = Vector2.zero;
    }
}
