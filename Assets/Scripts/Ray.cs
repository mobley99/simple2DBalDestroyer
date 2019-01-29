using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Ray : MonoBehaviour {

    [SerializeField] GameObject ray;
    [SerializeField] private GameObject[] balls;
    [SerializeField] float intensity;

    const float minAngle = -60f;
    const float maxAngle = 60f;
    const float step = 4f;
    const float yOffset = 0.5f;
    const float shootingFrequency = 0.075f;
    readonly Vector3 offset = new Vector3(0, yOffset, 0);

    // Countiniously firing ray for prdicting final collision
    private Coroutine firingCoroutine;
    GameObject nextBall; // Bottom ui sprites indicating next ball
    GameObject next2Ball;
    private float angle;
    private int next; // the bottom sprites
    private int next2;


    // Use this for initialization
    void Start ()
    {
        angle = 0;
        next = -1;

        nextBall = GameObject.FindWithTag("next");
        next2Ball = GameObject.FindWithTag("next2");

        GenerateNext();
    }
	
	// Update is called once per frame
    void Update ()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        float move = CrossPlatformInputManager.GetAxis("Horizontal");
        angle += move * step;
        angle = Mathf.Clamp(angle, minAngle, maxAngle);
        transform.rotation = Quaternion.Euler(new Vector3(0f,0f,-angle));
    }


    private void Fire()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire"))
        {
            firingCoroutine = StartCoroutine(FireContiniously());
        }
        if (CrossPlatformInputManager.GetButtonUp("Fire"))
        {
            StopCoroutine(firingCoroutine);
        }
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 velocity = CalculateVelocity();

            GameObject ball = Instantiate(balls[next],
                transform.parent.position + offset, Quaternion.identity);
            ball.GetComponent<Rigidbody2D>().velocity = velocity;
            GenerateNext();
        }
    }

    IEnumerator FireContiniously()
    {
        // Will be destroyed once the fire button released
        while (true)
        {

            GameObject rayInstance = Instantiate(ray, 
                transform.parent.position + offset, Quaternion.identity);
            Rigidbody2D body = rayInstance.GetComponent<Rigidbody2D>();

            if (body)
            {
                body.velocity = CalculateVelocity();
            }
            yield return new WaitForSeconds(seconds: shootingFrequency);
        }
    }

    private void GenerateNext()
    {
        int index;

        if (next == -1)
        {
            index = UnityEngine.Random.Range(0, 3);
            next = index;
        }
        else
        {
            next = next2;
        }

        index = UnityEngine.Random.Range(0, 3);
        next2 = index;

        // Changing the sprites
        nextBall.GetComponent<Image>().sprite = 
            balls[next].GetComponent<SpriteRenderer>().sprite;
        next2Ball.GetComponent<Image>().sprite = 
            balls[next2].GetComponent<SpriteRenderer>().sprite;

    }

    private Vector2 CalculateVelocity()
    {
        double y = Math.Sin((angle + 90) * Math.PI / 180);
        double x = Math.Cos((angle + 90) * Math.PI / 180);
        Vector2 velocity = new Vector2((float)-x * intensity, (float)y * intensity);
        return velocity;
    }

}
