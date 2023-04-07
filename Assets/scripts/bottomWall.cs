using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottomWall : MonoBehaviour
{
    //public Vector2 ballStartPosition;
    public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("paddle");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ballStartPosition = new Vector2(Screen.width / 2, Screen.height / 2);
        if ( collision.gameObject.CompareTag("Ball") )
        {
            Rigidbody2D ball = collision.gameObject.GetComponent<Rigidbody2D>();
            LivesManager.instance.loseLife();
            ball.velocity = Vector2.zero;
            //ball.transform.position = Vector2.zero;
            ball.transform.position = new Vector2(0f, -1f);
            player.transform.position = new Vector2(0f, -4.79f);
           // ball.velocity = new Vector2(Random.Range(-1f, 1f), 1).normalized * 4f;
            //ball.velocity = new Vector2(0.04f, -3.56f);
        }
    }
}
