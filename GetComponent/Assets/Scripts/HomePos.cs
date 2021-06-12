using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePos : MonoBehaviour
{
    private Vector2 homePos;
    
    void Start()
    {
        homePos = transform.position;
    }

    public void ResetObject()
    {
        transform.position = homePos;
        transform.GetComponent<SpriteRenderer>().enabled = true;
        transform.GetComponent<BoxCollider2D>().enabled = true;
    }
}
