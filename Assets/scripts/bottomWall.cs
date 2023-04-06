using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottomWall : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject.CompareTag("Ball") )
        {
            LivesManager.instance.loseLife();
        }
    }
}
