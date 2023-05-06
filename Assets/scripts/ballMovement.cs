using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class BallMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D ball;
    private GameObject paddleObject;
    private Rigidbody2D paddle;
    public bool isPaddleSizeChanged;
    private float maxRotation = 50f;
    private Vector3 paddleOriginalScale;
    //[SerializeField] public ParticleSystem particles;
    Vector2 direction;
    void Start()
    {
        ball = GetComponent<Rigidbody2D>();
        isPaddleSizeChanged = false;
        float angle = Random.Range(-maxRotation, maxRotation);
        if (angle >= -30f && angle <= 30f)
            angle = (angle < 0) ? -35f : 35f;
        direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
        paddleObject = GameObject.FindGameObjectWithTag("paddle");
        paddle = paddleObject.gameObject.GetComponent<Rigidbody2D>();
        paddleOriginalScale = paddle.transform.localScale;
            //direction = Vector2.one.normalized;
    }

    private void FixedUpdate()
    {
        ball.velocity = direction * speed;
    }

    private void handleScore( Vector3 hitPosition, string brick = "one" )
    {
        Debug.Log("Im in handleScore");
        if (hitPosition.y >= 2 && hitPosition.y < 3)
            brick = "three";
        else if (hitPosition.y >= 3 && hitPosition.y < 4)
            brick = "five";
        else if (hitPosition.y >= 4)
        {
            brick = "seven";
            paddleObject = GameObject.FindGameObjectWithTag("paddle");
            if (paddleObject)
            {
                paddle = paddleObject.GetComponent<Rigidbody2D>();
                if (isPaddleSizeChanged == false)
                {
                    paddle.transform.localScale = new Vector3(paddle.transform.localScale.x / 2f, paddle.transform.localScale.y, paddle.transform.localScale.z);
                    isPaddleSizeChanged = true;
                }
            }
            int randomNbr = Random.Range(1, 3);
            if (randomNbr == 2)
            {
                Debug.Log("POWER UP");
                paddle.transform.localScale = new Vector3(paddle.transform.localScale.x * 2f, paddle.transform.localScale.y, paddle.transform.localScale.z);
            }
            speed = 7.5f;
        }
        ScoreManager.instance.updateScore(brick);
    }
    private void destroyBricks( Tilemap tilemap, Collision2D collision )
    {
        Vector3 hitPosition = Vector3.zero;
        Vector3 particlesPos = Vector3.zero;
        foreach (ContactPoint2D contact in collision.contacts)
        {
            hitPosition.x = contact.point.x - 0.01f * contact.normal.x;
            hitPosition.y = contact.point.y - 0.01f * contact.normal.y;
            particlesPos = contact.point;
            Vector3Int brickPosition = tilemap.WorldToCell(hitPosition);
            Vector3 brickWorldPosition = tilemap.CellToWorld(brickPosition);
            if (tilemap.HasTile(brickPosition))
            {
                hitPosition.y += 1;
                Scene activeScene = SceneManager.GetActiveScene();
                if (activeScene.name != "MainMenu")
                {
                    handleScore(hitPosition);
                }
                tilemap.SetTile(brickPosition, null);
                /*if (activeScene.name != "MainMenu")
                {
                    //particles.transform.localScale = new Vector3(particles.transform.localScale.x, particles.transform.localScale.y, 1);
                    //Instantiate(particles, brickWorldPosition, Quaternion.identity);
                    particles.transform.position = brickWorldPosition;
                    particles.Play();
                }*/
                //particles.Stop();
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject tilemapObject = GameObject.Find("bricks");
        Tilemap tilemap = tilemapObject.GetComponent<Tilemap>();
        if (tilemap)
        {
            destroyBricks(tilemap, collision);
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
