using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBall : MonoBehaviour
{
    private Rigidbody2D ball;
    public float maxRotation;
    public float speed;
    private Vector2 direction;
    void Start()
    {
        ball = GetComponent<Rigidbody2D>();
        float angle = Random.Range(-maxRotation, maxRotation);
        if (angle >= -30f && angle <= 30f)
            angle = (angle < 0) ? -35f : 35f;
        direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
    }
    private void FixedUpdate()
    {
        ball.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("rightWall") || collision.gameObject.CompareTag("leftWall"))
            direction.x *= -1;
        if (collision.gameObject.CompareTag("topWall") || collision.gameObject.CompareTag("bottomWall"))
            direction.y *= -1;
    }
}
