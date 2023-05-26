using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongPlayer : MonoBehaviour
{
    private Rigidbody2D player;
    public float speed;
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float verticalInput = Input.GetAxis("Vertical");
        player.velocity = new Vector2(player.velocity.x, verticalInput * speed);
    }
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("topWall") || collision.gameObject.CompareTag("bottomWall"))
            player.velocity = Vector2.zero;
    }
}
