using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public AudioClip win;
    public int targetLevel = -1;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            GetComponent<AudioSource>().PlayOneShot(win);
            if (collision.GetComponent<Player>().level != -2)
            {
                if (targetLevel != -1)
                {
                    if (targetLevel == -3)
                    {

                    }
                    else if (targetLevel == -4)
                    {
                        Application.OpenURL("https://itch.io/profile/ce-studio");
                    }
                    else
                    {
                        collision.GetComponent<Player>().StartTransition(targetLevel, true);
                    }
                }
                else
                {
                    if (Levels.playerStartPos.Count > collision.GetComponent<Player>().level + 1)
                    {
                        collision.GetComponent<Player>().StartTransition(collision.GetComponent<Player>().level + 1, true);
                    }
                    else
                    {
                        collision.GetComponent<Player>().StartTransition(0, true);
                    }
                }
            }
        }
    }
}
