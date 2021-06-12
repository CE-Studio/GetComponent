using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    private Vector2 realPos;
    private char intendedDir;
    private List<GameObject> components = new List<GameObject>();
    private List<GameObject> tamperedObjs = new List<GameObject>();
    public LayerMask collideMask;
    private bool holdingL = false;
    private bool holdingR = false;
    private bool holdingU = false;
    private bool holdingD = false;
    public int level = -1; // -2 is test level, -1 is main menu, 0 is level select, 1 onwards are actual levels
    private bool canControl = true;

    public Tilemap map;
    public AudioSource sfx;
    public AudioClip step;
    public AudioClip bump;
    public AudioClip connect;
    public GameObject cam;
    public GameObject transitionMask;
    public GameObject player;

    private bool warpToDebug = true;
    
    void Start()
    {
        Levels.Initiate();

        realPos = transform.position;

        sfx = GetComponent<AudioSource>();
        cam = GameObject.Find("View");
        transitionMask = cam.transform.Find("Transition Mask").gameObject;
        player = gameObject;

        if (warpToDebug)
        {
            level = -2;
        }

        Respawn();
        ResetComponentList();
    }

    private void FixedUpdate()
    {
        if (canControl)
        {
            if (Input.GetAxisRaw("Horizontal") == -1 && !holdingL)
            {
                bool canMove = true;
                bool canConnect = false;
                GameObject componentToConnect = null;
                foreach (GameObject component in components)
                {
                    if (component.activeSelf)
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
                        //RaycastHit2D moveChecker = Physics2D.Raycast(
                        //    rayOrigin,
                        //    -Vector2.right,
                        //    0.75f,
                        //    collideMask,
                        //    Mathf.Infinity,
                        //    Mathf.Infinity
                        //    );
                        RaycastHit2D moveChecker = Physics2D.BoxCast(
                            rayOrigin,
                            component.transform.GetComponent<BoxCollider2D>().size,
                            component.transform.rotation.z,
                            -Vector2.right,
                            0.75f,
                            collideMask,
                            Mathf.Infinity,
                            Mathf.Infinity
                            );
                        if (moveChecker.collider != null)
                        {
                            if (component.name.Contains("Plug") && moveChecker.collider.name.Contains("Outlet"))
                            {
                                if (component.name.Contains("Left") && moveChecker.collider.name.Contains("Right"))
                                {
                                    canConnect = true;
                                    componentToConnect = moveChecker.collider.transform.parent.gameObject;
                                }
                                else
                                {
                                    canMove = false;
                                }
                            }
                            else if (moveChecker.collider.CompareTag("Component"))
                            {
                                if (!moveChecker.collider.transform.IsChildOf(player.transform))
                                {
                                    canMove = false;
                                }
                            }
                            else
                            {
                                canMove = false;
                            }
                        }
                    }
                }
                if (canMove)
                {
                    if (canConnect)
                    {
                        sfx.PlayOneShot(connect);
                        componentToConnect.transform.position = new Vector2(
                            componentToConnect.transform.position.x + 1,
                            componentToConnect.transform.position.y);
                        componentToConnect.transform.parent = player.transform;
                        componentToConnect.GetComponent<BoxCollider2D>().enabled = false;
                        for (int i = 0; i < componentToConnect.transform.childCount; i++)
                        {
                            componentToConnect.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = false;
                        }
                    }
                    else
                    {
                        sfx.PlayOneShot(step);
                    }
                    realPos += new Vector2(-1, 0);
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
                bool canConnect = false;
                GameObject componentToConnect = null;
                foreach (GameObject component in components)
                {
                    if (component.activeSelf)
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
                        //RaycastHit2D moveChecker = Physics2D.Raycast(
                        //    rayOrigin,
                        //    Vector2.right,
                        //    0.75f,
                        //    collideMask,
                        //    Mathf.Infinity,
                        //    Mathf.Infinity
                        //    );
                        RaycastHit2D moveChecker = Physics2D.BoxCast(
                            rayOrigin,
                            component.transform.GetComponent<BoxCollider2D>().size,
                            component.transform.rotation.z,
                            Vector2.right,
                            0.75f,
                            collideMask,
                            Mathf.Infinity,
                            Mathf.Infinity
                            );
                        if (moveChecker.collider != null)
                        {
                            if (component.name.Contains("Plug") && moveChecker.collider.name.Contains("Outlet"))
                            {
                                if (component.name.Contains("Right") && moveChecker.collider.name.Contains("Left"))
                                {
                                    canConnect = true;
                                    componentToConnect = moveChecker.collider.transform.parent.gameObject;
                                }
                                else
                                {
                                    canMove = false;
                                }
                            }
                            else if (moveChecker.collider.CompareTag("Component"))
                            {
                                if (!moveChecker.collider.transform.IsChildOf(player.transform))
                                {
                                    canMove = false;
                                }
                            }
                            else
                            {
                                canMove = false;
                            }
                        }
                    }
                }
                if (canMove)
                {
                    if (canConnect)
                    {
                        sfx.PlayOneShot(connect);
                        componentToConnect.transform.position = new Vector2(
                            componentToConnect.transform.position.x - 1,
                            componentToConnect.transform.position.y);
                        componentToConnect.transform.parent = player.transform;
                        componentToConnect.GetComponent<BoxCollider2D>().enabled = false;
                        for (int i = 0; i < componentToConnect.transform.childCount; i++)
                        {
                            componentToConnect.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = false;
                        }
                    }
                    else
                    {
                        sfx.PlayOneShot(step);
                    }
                    realPos += new Vector2(1, 0);
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
                bool canConnect = false;
                GameObject componentToConnect = null;
                foreach (GameObject component in components)
                {
                    if (component.activeSelf)
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
                        //RaycastHit2D moveChecker = Physics2D.Raycast(
                        //    rayOrigin,
                        //    Vector2.up,
                        //    0.75f,
                        //    collideMask,
                        //    Mathf.Infinity,
                        //    Mathf.Infinity
                        //    );
                        RaycastHit2D moveChecker = Physics2D.BoxCast(
                            rayOrigin,
                            component.transform.GetComponent<BoxCollider2D>().size,
                            component.transform.rotation.z,
                            Vector2.up,
                            0.75f,
                            collideMask,
                            Mathf.Infinity,
                            Mathf.Infinity
                            );
                        if (moveChecker.collider != null)
                        {
                            if (component.name.Contains("Plug") && moveChecker.collider.name.Contains("Outlet"))
                            {
                                if (component.name.Contains("Top") && moveChecker.collider.name.Contains("Bottom"))
                                {
                                    canConnect = true;
                                    componentToConnect = moveChecker.collider.transform.parent.gameObject;
                                }
                                else
                                {
                                    canMove = false;
                                }
                            }
                            else if (moveChecker.collider.CompareTag("Component"))
                            {
                                if (!moveChecker.collider.transform.IsChildOf(player.transform))
                                {
                                    canMove = false;
                                }
                            }
                            else
                            {
                                canMove = false;
                            }
                        }
                    }
                }
                if (canMove)
                {
                    if (canConnect)
                    {
                        sfx.PlayOneShot(connect);
                        componentToConnect.transform.position = new Vector2(
                            componentToConnect.transform.position.x,
                            componentToConnect.transform.position.y - 1);
                        componentToConnect.transform.parent = player.transform;
                        componentToConnect.GetComponent<BoxCollider2D>().enabled = false;
                        for (int i = 0; i < componentToConnect.transform.childCount; i++)
                        {
                            componentToConnect.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = false;
                        }
                    }
                    else
                    {
                        sfx.PlayOneShot(step);
                    }
                    realPos += new Vector2(0, 1);
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
                bool canConnect = false;
                GameObject componentToConnect = null;
                foreach (GameObject component in components)
                {
                    if (component.activeSelf)
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
                        //RaycastHit2D moveChecker = Physics2D.Raycast(
                        //    rayOrigin,
                        //    -Vector2.up,
                        //    0.75f,
                        //    collideMask,
                        //    Mathf.Infinity,
                        //    Mathf.Infinity
                        //    );
                        RaycastHit2D moveChecker = Physics2D.BoxCast(
                            rayOrigin,
                            component.transform.GetComponent<BoxCollider2D>().size,
                            component.transform.rotation.z,
                            -Vector2.up,
                            0.75f,
                            collideMask,
                            Mathf.Infinity,
                            Mathf.Infinity
                            );
                        if (moveChecker.collider != null)
                        {
                            if (component.name.Contains("Plug") && moveChecker.collider.name.Contains("Outlet"))
                            {
                                if (component.name.Contains("Bottom") && moveChecker.collider.name.Contains("Top"))
                                {
                                    canConnect = true;
                                    componentToConnect = moveChecker.collider.transform.parent.gameObject;
                                }
                                else
                                {
                                    canMove = false;
                                }
                            }
                            else if (moveChecker.collider.CompareTag("Component"))
                            {
                                if (!moveChecker.collider.transform.IsChildOf(player.transform))
                                {
                                    canMove = false;
                                }
                            }
                            else
                            {
                                canMove = false;
                            }
                        }
                    }
                }
                if (canMove)
                {
                    if (canConnect)
                    {
                        sfx.PlayOneShot(connect);
                        componentToConnect.transform.position = new Vector2(
                            componentToConnect.transform.position.x,
                            componentToConnect.transform.position.y + 1);
                        componentToConnect.transform.parent = player.transform;
                        componentToConnect.GetComponent<BoxCollider2D>().enabled = false;
                        for (int i = 0; i < componentToConnect.transform.childCount; i++)
                        {
                            componentToConnect.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = false;
                        }
                    }
                    else
                    {
                        sfx.PlayOneShot(step);
                    }
                    realPos += new Vector2(0, -1);
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

            if (Input.GetAxisRaw("Reset") == 1 && level > 0)
            {
                StartCoroutine(Transition(level, false));
            }
            else if (Input.GetAxisRaw("Reset") == -1 && level > -1)
            {
                StartCoroutine(Transition(-1, false));
            }
        }
    }

    void Update()
    {
        transform.position = new Vector2(
            Mathf.Lerp(transform.position.x, realPos.x, 20f * Time.deltaTime),
            Mathf.Lerp(transform.position.y, realPos.y, 20f * Time.deltaTime));
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

    void UnparentComponents()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).name.Contains("Plug"))
            {
                GameObject component = transform.GetChild(i).gameObject;
                component.transform.parent = null;
                component.GetComponent<HomePos>().ResetObject();
            }
        }
    }

    public void Respawn()
    {
        int desiredLevel = level;
        if (level == -1)
        {
            cam.transform.position = new Vector3(0.5f, 20.5f, -10);
            cam.transform.Find("Main Camera").GetComponent<Camera>().orthographicSize = 8;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(false);
            MoveWithoutLerp(0.5f, 20.5f);
        }
        else
        {
            if (level == -2)
            {
                desiredLevel = 0;
            }
            cam.transform.position = new Vector3(Levels.camOrientations[desiredLevel].x, Levels.camOrientations[desiredLevel].y, -10);
            cam.transform.Find("Main Camera").GetComponent<Camera>().orthographicSize = Levels.camOrientations[desiredLevel].z;
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

        foreach (GameObject obj in tamperedObjs)
        {
            obj.GetComponent<HomePos>().ResetObject();
        }
        tamperedObjs.Clear();
    }

    private IEnumerator Transition(int desiredLevel, bool transitionDelay)
    {
        canControl = false;
        if (transitionDelay)
        {
            yield return new WaitForSeconds(0.25f);
        }
        while (transitionMask.transform.localScale.x > 0)
        {
            transitionMask.transform.localScale = new Vector2(
                Mathf.Clamp(transitionMask.transform.localScale.x - (3 * Time.deltaTime), 0, 3),
                Mathf.Clamp(transitionMask.transform.localScale.y - (3 * Time.deltaTime), 0, 3));
            yield return new WaitForEndOfFrame();
        }
        level = desiredLevel;
        Respawn();
        yield return new WaitForSeconds(0.5f);
        while (transitionMask.transform.localScale.x < 3)
        {
            transitionMask.transform.localScale = new Vector2(
                Mathf.Clamp(transitionMask.transform.localScale.x + (3 * Time.deltaTime), 0, 3),
                Mathf.Clamp(transitionMask.transform.localScale.y + (3 * Time.deltaTime), 0, 3));
            yield return new WaitForEndOfFrame();
        }
        canControl = true;
    }

    public void StartTransition(int desiredLevel, bool transitionDelay)
    {
        StartCoroutine(Transition(desiredLevel, transitionDelay));
    }
}
