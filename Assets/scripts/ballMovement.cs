using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
        Tilemap tilemap = collision.gameObject.GetComponent<Tilemap>();
        if ( tilemap )
        {
            Vector3 hitPosition = Vector3.zero;
            foreach ( ContactPoint2D contact in collision.contacts )
            {
                hitPosition.x = contact.point.x - 0.01f * contact.normal.x;
                hitPosition.y = contact.point.y - 0.01f * contact.normal.y;
                Vector3Int brickPosition = tilemap.WorldToCell(hitPosition);
                if (tilemap.HasTile(brickPosition))
                {
                    ScoreManager.instance.updateScore();
                    tilemap.SetTile(brickPosition, null);
                    ball.velocity = Vector2.zero;
                    float difference = contact.point.x - hitPosition.x;
                    if ( contact.point.x > hitPosition.x )
                    {
                        ball.AddForce(new Vector2((Mathf.Abs(difference * 200)), speed));
                    }
                    else
                    {
                        ball.AddForce(new Vector2(-(Mathf.Abs(difference * 200)), speed));
                    }
                    direction.y *= -1;
                }
                       
            }
        }
        if ( collision.gameObject.CompareTag("paddle") || collision.gameObject.CompareTag("topWall") )
        {
            direction.y *= -1;
        }
        else if ( collision.gameObject.CompareTag("sideWalls") )
        {
            direction.x *= -1;
        }
    }
}
