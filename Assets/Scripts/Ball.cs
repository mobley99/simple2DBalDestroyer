using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    // Ball access to game session object in order to update core game features.
    private GameSession gameSession;
    private Rigidbody2D rigidBody;
    private bool isSleep;

    // First method on life cycle
    void Awake ()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        isSleep = false;
    }

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        bool destroy = gameObject.tag == other.transform.tag;

        // Destroy and count only when same color balls collide
        if (destroy)
        {
            Destroy(gameObject);
            gameSession.UpdateScore(1);
        }

        Ball ball = other.gameObject.GetComponent<Ball>();

        if (ball)
        {
            if (ball.IsSleep())
            {
                rest();
            }
        }
    }

    public void rest()
    {
        rigidBody.Sleep(); // Deactivating the rigid body and keep it static
        rigidBody.bodyType = RigidbodyType2D.Static;
        isSleep = true;
    }

    public bool IsSleep()
    {
        return isSleep;
    }

}
