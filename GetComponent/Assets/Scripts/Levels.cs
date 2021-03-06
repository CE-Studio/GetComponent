using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels
{
    public static List<Vector3> camOrientations = new List<Vector3>();
    public static List<Vector2> playerStartPos = new List<Vector2>();
    public static List<string> playerPlugArrangements = new List<string>();

    public static void Initiate()
    {
        camOrientations.Add(new Vector3(0, 0, 8));
        camOrientations.Add(new Vector3(28, 1, 5));
        camOrientations.Add(new Vector3(49, 1, 5));
        camOrientations.Add(new Vector3(70, 1, 5));
        camOrientations.Add(new Vector3(91, 1, 5));
        camOrientations.Add(new Vector3(112, 1, 5));
        camOrientations.Add(new Vector3(133, 1, 5));
        camOrientations.Add(new Vector3(157.5f, 3, 7.25f));
        camOrientations.Add(new Vector3(28, -12, 5));
        camOrientations.Add(new Vector3(49, -12, 5));
        camOrientations.Add(new Vector3(70, -12, 5));
        camOrientations.Add(new Vector3(91, -12, 5));

        playerStartPos.Add(new Vector2(0.5f, 0.5f));
        playerStartPos.Add(new Vector2(23.5f, -1.5f));
        playerStartPos.Add(new Vector2(43.5f, -1.5f));
        playerStartPos.Add(new Vector2(76.5f, -1.5f));
        playerStartPos.Add(new Vector2(84.5f, -0.5f));
        playerStartPos.Add(new Vector2(105.5f, -1.5f));
        playerStartPos.Add(new Vector2(139.5f, 3.5f));
        playerStartPos.Add(new Vector2(148.5f, 2.5f));
        playerStartPos.Add(new Vector2(21.5f, -11.5f));
        playerStartPos.Add(new Vector2(48.5f, -15.5f));
        playerStartPos.Add(new Vector2(75.5f, -12.5f));
        playerStartPos.Add(new Vector2(91.5f, -14.5f));

        playerPlugArrangements.Add("LRUD");
        playerPlugArrangements.Add("");
        playerPlugArrangements.Add("LR");
        playerPlugArrangements.Add("U");
        playerPlugArrangements.Add("U");
        playerPlugArrangements.Add("UR");
        playerPlugArrangements.Add("R");
        playerPlugArrangements.Add("LR");
        playerPlugArrangements.Add("R");
        playerPlugArrangements.Add("LRU");
        playerPlugArrangements.Add("LR");
        playerPlugArrangements.Add("LRD");
    }
}
