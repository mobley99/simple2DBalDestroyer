using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameSession : MonoBehaviour {


    // Prefab arrays
    [SerializeField] private GameObject[] balls;
    [SerializeField] private Sprite[]  digits;

    // Const of ball dimension & position
    const float posx = -2.1f;
    const float posy = 4.3f;
    const int cols = 9;
    const int rows = 3;
    const float offset = 0.5f;

    // caches of img and score
    private int score;
    private GameObject digit1;
    private GameObject digit2;

    // Putting some balls into the scene
    void Start ()
    {
        score = 0;

        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                int index = Random.Range(0, rows);
                GameObject obj = Instantiate(balls[index], 
                    new Vector3(posx + i * offset, posy - j * offset, 0), 
                    Quaternion.identity);
                obj.transform.parent = gameObject.transform;
                obj.GetComponent<Ball>().rest();

            }
        }

        digit1 = GameObject.FindWithTag("digit1");
        digit2 = GameObject.FindWithTag("digit2");
    }


    // Score updating throught sprites
    public void UpdateScore (int points)
    {
        score += points;

        int digit = score / 10;
        digit1.GetComponent<Image>().sprite = digits[digit];

        digit = score % 10;
        digit2.GetComponent<Image>().sprite = digits[digit];
    }

}
