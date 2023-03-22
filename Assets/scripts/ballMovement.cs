using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballMovement : MonoBehaviour
{
    public float speed = 4f;
    private Rigidbody2D ball;
    Vector2 direction;
    void Start()
    {
        ball = GetComponent<Rigidbody2D>();
        direction = Vector2.one.normalized;
    }

    private void FixedUpdate()
    {
        ball.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.CompareTag("paddle") || collision.gameObject.CompareTag("topWall") )
        {
            direction.y *= -1;
        }
        else if ( collision.gameObject.CompareTag("sideWalls") )
        {
            direction.x *= -1;
            //ContactPoint2D contact = collision.contacts[0];
            //Vector2 normal = contact.normal;
            //ball.velocity = Vector2.Reflect(ball.velocity, normal);
        }
    }
}
