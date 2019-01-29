using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmentedSprite : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.transform.tag.Equals("wall"))
        {
            Destroy(gameObject);
        }
    }

}
