using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEmitter : MonoBehaviour
{
    public GameObject laser;
    public GameObject particles;
    public LayerMask collideMask;
    
    void Start()
    {
        laser = transform.GetChild(0).gameObject;
        particles = transform.GetChild(1).gameObject;
    }

    void FixedUpdate()
    {
        RaycastHit2D laserCast = Physics2D.Raycast(
            new Vector2(transform.position.x, transform.position.y + 0.45f),
            Vector2.up,
            Mathf.Infinity,
            collideMask,
            Mathf.Infinity,
            Mathf.Infinity
            );
        switch (transform.eulerAngles.z)
        {
            case 0:
                laserCast = Physics2D.Raycast(
                    new Vector2(transform.position.x, transform.position.y + 0.45f),
                    Vector2.up,
                    Mathf.Infinity,
                    collideMask,
                    Mathf.Infinity,
                    Mathf.Infinity
                    );
                break;
            case 90:
                laserCast = Physics2D.Raycast(
                    new Vector2(transform.position.x - 0.45f, transform.position.y),
                    -Vector2.right,
                    Mathf.Infinity,
                    collideMask,
                    Mathf.Infinity,
                    Mathf.Infinity
                    );
                break;
            case 180:
                laserCast = Physics2D.Raycast(
                    new Vector2(transform.position.x, transform.position.y - 0.45f),
                    -Vector2.up,
                    Mathf.Infinity,
                    collideMask,
                    Mathf.Infinity,
                    Mathf.Infinity
                    );
                break;
            case 270:
                laserCast = Physics2D.Raycast(
                    new Vector2(transform.position.x + 0.45f, transform.position.y),
                    Vector2.right,
                    Mathf.Infinity,
                    collideMask,
                    Mathf.Infinity,
                    Mathf.Infinity
                    );
                break;
        }
        particles.transform.position = laserCast.point;
        laser.transform.localScale = new Vector3(1, (Mathf.RoundToInt(laserCast.distance) + 0.5f) * 2, 1);
    }
}
