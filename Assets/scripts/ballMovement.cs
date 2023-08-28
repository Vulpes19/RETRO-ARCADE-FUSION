using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using TMPro;

public class BallMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D ball;
    private GameObject paddleObject;
    private Rigidbody2D paddle;
    public bool isPaddleSizeChanged;
    private float maxRotation = 50f;
    private Vector3 paddleOriginalScale;
    private IEnumerator coroutine;
    public TextMeshProUGUI countdown;
    public AudioClip brickDestroyedAudio;
    //public AudioSource brickDestroyedAudio;
    Vector2 direction;
    private int timer = 3;

    void Start()
    {

        Scene activeScene = SceneManager.GetActiveScene();
        if ( activeScene.name != "MainMenu")
            StartCoroutine(FirstStart());
        ball = GetComponent<Rigidbody2D>();
        isPaddleSizeChanged = false;
        float angle = Random.Range(-maxRotation, maxRotation);
        if (angle >= -30f && angle <= 30f)
            angle = (angle < 0) ? -35f : 35f;
        direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
        paddleObject = GameObject.FindGameObjectWithTag("paddle");
        if (paddleObject)
        {
            paddle = paddleObject.gameObject.GetComponent<Rigidbody2D>();
            paddleOriginalScale = paddle.transform.localScale;
        }
    }

    public IEnumerator FirstStart()
    {
        Time.timeScale = 0.0f;
        timer = 3;
        countdown.gameObject.SetActive(true);
        while (timer > 0)
        {
            countdown.SetText(timer.ToString());
            yield return new WaitForSecondsRealtime(1);
            timer--;
        }
        countdown.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }
    private void FixedUpdate()
    {
        ball.velocity = direction * speed;
    }

    private void handleScore( Vector3 hitPosition, string brick = "one" )
    {
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
                    AudioManager.instance.playSound(brickDestroyedAudio);
                    //brickDestroyedAudio.Play();
                    handleScore(hitPosition);
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
