using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class BallMovement : MonoBehaviour
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
        GameObject tilemapObject = GameObject.Find("bricks");
        Tilemap tilemap = tilemapObject.GetComponent<Tilemap>();
        if (tilemap)
        {
            Vector3 hitPosition = Vector3.zero;
            foreach (ContactPoint2D contact in collision.contacts)
            {
                hitPosition.x = contact.point.x - 0.01f * contact.normal.x;
                hitPosition.y = contact.point.y - 0.01f * contact.normal.y;
                Vector3Int brickPosition = tilemap.WorldToCell(hitPosition);
                if (tilemap.HasTile(brickPosition))
                {
                    hitPosition.y += 1;
                    Debug.Log("y: " + hitPosition.y.ToString());
                    Scene activeScene = SceneManager.GetActiveScene();
                    if (activeScene.name != "MainMenu")
                    {
                        string brick = "one";
                        if (hitPosition.y >= 2 && hitPosition.y < 3)
                            brick = "three";
                        else if (hitPosition.y >= 3 && hitPosition.y < 4)
                            brick = "five";
                        else if (hitPosition.y >= 4)
                        {
                            brick = "seven";
                            speed = 6f;
                        }
                        ScoreManager.instance.updateScore(brick);
                    }
                    tilemap.SetTile(brickPosition, null);
                    float dotProduct = Vector2.Dot(contact.normal, Vector2.right);
                    if (dotProduct > 0f)
                        direction.x = Mathf.Abs(direction.x);
                    else if (dotProduct < 0f)
                        direction.x = -Mathf.Abs(direction.x);
                    else
                        direction.y *= -1;
                }

            }
        }
        if (collision.gameObject.CompareTag("paddle") || collision.gameObject.CompareTag("topWall"))
        {
            direction.y *= -1;
        }
        else if (collision.gameObject.CompareTag("sideWalls"))
        {
            direction.x *= -1;
        }
    }
}
