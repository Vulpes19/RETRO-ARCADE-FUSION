using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottomWall : MonoBehaviour
{
    public GameObject player;
    private BallMovement ballClass;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("paddle");
        ballClass = GetComponent<BallMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject.CompareTag("Ball") )
        {
            ballClass = GetComponent<BallMovement>();
            Rigidbody2D ball = collision.gameObject.GetComponent<Rigidbody2D>();
            LivesManager.instance.loseLife();
            ball.velocity = Vector2.zero;
            ball.transform.position = new Vector2(0f, -1f);
            player.transform.position = new Vector2(0f, -4.79f);
            StartCoroutine(ballClass.FirstStart());
        }
    }
}
