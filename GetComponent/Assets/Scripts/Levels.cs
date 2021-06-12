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

        playerStartPos.Add(new Vector2(0.5f, 0.5f));

        playerPlugArrangements.Add("LRUD");
    }
}
