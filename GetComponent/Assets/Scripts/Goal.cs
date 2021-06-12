using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public AudioClip win;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            GetComponent<AudioSource>().PlayOneShot(win);
        }
    }
}
