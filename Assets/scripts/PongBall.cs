using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PongBall : MonoBehaviour
{
    private Rigidbody2D ball;
    public float maxRotation;
    public float speed;
    private Vector3 worldCenter;
    private Vector2 direction;
    private GameObject player;
    private GameObject AI;
    public TextMeshProUGUI countdown;
    private IEnumerator coroutine;
    private int timer = 0;
    void Start()
    {
        ball = GetComponent<Rigidbody2D>();
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        worldCenter = Camera.main.ScreenToWorldPoint(screenCenter);
        worldCenter.z = 0;
        ball.transform.position = worldCenter;
        StartCoroutine(FirstStart());
        float angle = Random.Range(-maxRotation, maxRotation);
        if (angle >= -30f && angle <= 30f)
            angle = (angle < 0) ? -35f : 35f;
        direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
    }
    private void FixedUpdate()
    {
        if (PongScoreManager.instance.getPlayerScore() == 5 || PongScoreManager.instance.getAIScore() == 5)
            speed = 9f;
        ball.velocity = direction * speed;
    }

    private IEnumerator FirstStart()
    {
        Time.timeScale = 0.0f;
        timer = 3;
        while (timer > 0)
        {
            countdown.SetText(timer.ToString());
            yield return new WaitForSecondsRealtime(1);
            timer--;
        }
        countdown.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }
    private void RespawnBallAndPaddles()
    {
        ball.velocity = Vector2.zero;
        ball.transform.position = worldCenter;
        player = GameObject.FindGameObjectWithTag("Player");
        AI = GameObject.FindGameObjectWithTag("AI");
        player.transform.position = new Vector2(-8.66f, -0.35f);
        AI.transform.position = new Vector2(8.67f, -0.35f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.CompareTag("rightWall"))
        {
            PongScoreManager.instance.updateScore("player");
            RespawnBallAndPaddles();
        }
        if (collision.gameObject.CompareTag("leftWall"))
        {
            PongScoreManager.instance.updateScore("AI");
            RespawnBallAndPaddles();
        }

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("AI") )
            direction.x *= -1;
        if (collision.gameObject.CompareTag("topWall") || collision.gameObject.CompareTag("bottomWall"))
            direction.y *= -1;
    }
}
