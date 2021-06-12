using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    private Vector2 realPos;
    private char intendedDir;
    private List<Collider2D> collidables = new List<Collider2D>();
    private List<GameObject> components = new List<GameObject>();
    public LayerMask collideMask;
    private bool holdingL = false;
    private bool holdingR = false;
    private bool holdingU = false;
    private bool holdingD = false;
    private int level = -2; // -2 is test level, -1 is main menu, 0 is level select, 1 onwards are actual levels

    public Tilemap map;
    public AudioSource sfx;
    public AudioClip step;
    public AudioClip bump;
    public GameObject cam;
    
    void Start()
    {
        Levels.Initiate();

        realPos = transform.position;

        sfx = GetComponent<AudioSource>();
        cam = GameObject.Find("Main Camera");



        //collidables.Add(map.GetComponent<TilemapCollider2D>());
        //foreach (Collider2D box in GameObject.Find("Breakables").transform.GetComponentsInChildren<Collider2D>())
        //{
        //    collidables.Add(box);
        //}
        //foreach (Collider2D box in GameObject.Find("Pushables").transform.GetComponentsInChildren<Collider2D>())
        //{
        //    collidables.Add(box);
        //}
        Respawn();
        ResetComponentList();
    }

    private void FixedUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") == -1 && !holdingL)
        {
            bool canMove = true;
            foreach (GameObject component in components)
            {
                Vector2 rayOrigin;
                if (component.name == "Player")
                {
                    rayOrigin = realPos;
                }
                else
                {
                    rayOrigin = realPos + (Vector2)component.transform.localPosition;
                }
                RaycastHit2D moveChecker = Physics2D.Raycast(
                    rayOrigin,
                    -Vector2.right,
                    0.75f,
                    collideMask,
                    Mathf.Infinity,
                    Mathf.Infinity
                    );
                if (moveChecker.collider != null)
                {
                    canMove = false;
                }
            }
            if (canMove)
            {
                realPos += new Vector2(-1, 0);
                sfx.PlayOneShot(step);
            }
            else
            {
                sfx.PlayOneShot(bump);
            }
            holdingL = true;
        }
        else if (Input.GetAxisRaw("Horizontal") == 1 && !holdingR)
        {
            bool canMove = true;
            foreach (GameObject component in components)
            {
                Vector2 rayOrigin;
                if (component.name == "Player")
                {
                    rayOrigin = realPos;
                }
                else
                {
                    rayOrigin = realPos + (Vector2)component.transform.localPosition;
                }
                RaycastHit2D moveChecker = Physics2D.Raycast(
                    rayOrigin,
                    Vector2.right,
                    0.75f,
                    collideMask,
                    Mathf.Infinity,
                    Mathf.Infinity
                    );
                if (moveChecker.collider != null)
                {
                    canMove = false;
                }
            }
            if (canMove)
            {
                realPos += new Vector2(1, 0);
                sfx.PlayOneShot(step);
            }
            else
            {
                sfx.PlayOneShot(bump);
            }
            holdingR = true;
        }
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            holdingL = false;
            holdingR = false;
        }

        if (Input.GetAxisRaw("Vertical") == 1 && !holdingU)
        {
            bool canMove = true;
            foreach (GameObject component in components)
            {
                Vector2 rayOrigin;
                if (component.name == "Player")
                {
                    rayOrigin = realPos;
                }
                else
                {
                    rayOrigin = realPos + (Vector2)component.transform.localPosition;
                }
                RaycastHit2D moveChecker = Physics2D.Raycast(
                    rayOrigin,
                    Vector2.up,
                    0.75f,
                    collideMask,
                    Mathf.Infinity,
                    Mathf.Infinity
                    );
                if (moveChecker.collider != null)
                {
                    canMove = false;
                }
            }
            if (canMove)
            {
                realPos += new Vector2(0, 1);
                sfx.PlayOneShot(step);
            }
            else
            {
                sfx.PlayOneShot(bump);
            }
            holdingU = true;
        }
        else if (Input.GetAxisRaw("Vertical") == -1 && !holdingD)
        {
            bool canMove = true;
            foreach (GameObject component in components)
            {
                Vector2 rayOrigin;
                if (component.name == "Player")
                {
                    rayOrigin = realPos;
                }
                else
                {
                    rayOrigin = realPos + (Vector2)component.transform.localPosition;
                }
                RaycastHit2D moveChecker = Physics2D.Raycast(
                    rayOrigin,
                    -Vector2.up,
                    0.75f,
                    collideMask,
                    Mathf.Infinity,
                    Mathf.Infinity
                    );
                if (moveChecker.collider != null)
                {
                    canMove = false;
                }
            }
            if (canMove)
            {
                realPos += new Vector2(0, -1);
                sfx.PlayOneShot(step);
            }
            else
            {
                sfx.PlayOneShot(bump);
            }
            holdingD = true;
        }
        else if (Input.GetAxisRaw("Vertical") == 0)
        {
            holdingU = false;
            holdingD = false;
        }
    }

    void Update()
    {
        //if (Input.GetKeyDown("left"))
        //{
        //    intendedDir = 'L';
        //    CheckMove(realPos.x - 1, realPos.y);
        //}
        //else if (Input.GetKeyDown("right"))
        //{
        //    intendedDir = 'R';
        //    CheckMove(realPos.x + 1, realPos.y);
        //}
        //else if (Input.GetKeyDown("up"))
        //{
        //    intendedDir = 'U';
        //    CheckMove(realPos.x, realPos.y + 1);
        //}
        //else if (Input.GetKeyDown("down"))
        //{
        //    intendedDir = 'D';
        //    CheckMove(realPos.x, realPos.y - 1);
        //}

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
                if (transform.GetChild(i).gameObject.activeSelf)
                {
                    Vector2 secondaryTestPoint = map.GetComponent<TilemapCollider2D>().ClosestPoint(new Vector2(
                        x + transform.GetChild(i).localPosition.x, y + transform.GetChild(i).localPosition.y));
                    Vector2 localPos = new Vector2(x + transform.GetChild(i).localPosition.x, y + transform.GetChild(i).localPosition.y);
                    if (Vector2.Distance(secondaryTestPoint, localPos) <= 0.45f || Vector2.Distance(secondaryTestPoint, localPos) == 1)
                    {
                        canMove = false;
                    }
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

    void ResetComponentList()
    {
        components.Clear();
        components.Add(transform.gameObject);
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                components.Add(transform.GetChild(i).gameObject);
            }
        }
    }

    void Respawn()
    {
        int desiredLevel = level;
        if (level == -2)
        {
            desiredLevel = 0;
        }
        cam.transform.position = new Vector3(Levels.camOrientations[desiredLevel].x, Levels.camOrientations[desiredLevel].y, -10);
        cam.GetComponent<Camera>().orthographicSize = Levels.camOrientations[desiredLevel].z;
        MoveWithoutLerp(Levels.playerStartPos[desiredLevel].x, Levels.playerStartPos[desiredLevel].y);
        for (int i = 0; i < 4; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        if (Levels.playerPlugArrangements[desiredLevel].Contains("L"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        if (Levels.playerPlugArrangements[desiredLevel].Contains("R"))
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        if (Levels.playerPlugArrangements[desiredLevel].Contains("U"))
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }
        if (Levels.playerPlugArrangements[desiredLevel].Contains("D"))
        {
            transform.GetChild(3).gameObject.SetActive(true);
        }
    }
}
