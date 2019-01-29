using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopCollision : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D other)
    {

        Ball ball = other.gameObject.GetComponent<Ball>();

        if (ball)
        { 
            ball.rest();
        }

    }

}
