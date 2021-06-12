using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    private Vector2 realPos;

    public Tilemap map;
    public AudioSource sfx;
    public AudioClip step;
    public AudioClip bump;
    
    void Start()
    {
        realPos = transform.position;

        sfx = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown("left"))
        {
            CheckMove(realPos.x - 1, realPos.y);
        }
        else if (Input.GetKeyDown("right"))
        {
            CheckMove(realPos.x + 1, realPos.y);
        }
        else if (Input.GetKeyDown("up"))
        {
            CheckMove(realPos.x, realPos.y + 1);
        }
        else if (Input.GetKeyDown("down"))
        {
            CheckMove(realPos.x, realPos.y - 1);
        }

        transform.position = new Vector2(
            Mathf.Lerp(transform.position.x, realPos.x, 20f * Time.deltaTime),
            Mathf.Lerp(transform.position.y, realPos.y, 20f * Time.deltaTime));
    }

    void CheckMove(float x, float y)
    {
        Vector2 collisionTestPoint = map.GetComponent<TilemapCollider2D>().ClosestPoint(new Vector2(x, y));
        if (Vector2.Distance(collisionTestPoint, realPos) > 0.45f && Vector2.Distance(collisionTestPoint, realPos) != 1)
        {
            bool canMove = true;
            for (int i = 0; i < transform.childCount; i++)
            {
                Vector2 secondaryTestPoint = map.GetComponent<TilemapCollider2D>().ClosestPoint(new Vector2(
                    x + transform.GetChild(i).localPosition.x, y + transform.GetChild(i).localPosition.y));
                Vector2 localPos = new Vector2(x + transform.GetChild(i).localPosition.x, y + transform.GetChild(i).localPosition.y);
                if (Vector2.Distance(secondaryTestPoint, localPos) <= 0.45f || Vector2.Distance(secondaryTestPoint, localPos) == 1)
                {
                    canMove = false;
                }
            }
            if (canMove)
            {
                realPos = new Vector2(x, y);
                sfx.PlayOneShot(step);
            }
            else
            {
                sfx.PlayOneShot(bump);
            }
        }
        else
        {
            sfx.PlayOneShot(bump);
        }
    }

    void MoveWithoutLerp(float x, float y)
    {
        realPos = new Vector2(x, y);
        transform.position = new Vector2(x, y);
    }
}
