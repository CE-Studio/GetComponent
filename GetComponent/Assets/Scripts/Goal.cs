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
            if (collision.GetComponent<Player>().level != -2)
            {
                switch (collision.GetComponent<Player>().level)
                {
                    case -1:
                        if (transform.position.x == 9.5f && transform.position.y == 20.5f)
                        {

                            collision.GetComponent<Player>().StartTransition(1, true);
                        }
                        break;
                    default:
                        collision.GetComponent<Player>().StartTransition(collision.GetComponent<Player>().level + 1, true);
                        break;
                }
            }
        }
    }
}
