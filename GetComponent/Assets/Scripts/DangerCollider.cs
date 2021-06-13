using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerCollider : MonoBehaviour
{
    public GameObject player;
    
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        transform.position = player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Deadly"))
        {
            player.GetComponent<Player>().Die();
        }
    }
}
