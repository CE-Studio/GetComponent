﻿using System.Collections;
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
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {

                transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
                transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        if (transform.CompareTag("Component"))
        {
            gameObject.layer = 8;
        }
    }
}
