using UnityEngine;
using System.Collections;

public class Node {

    public int Weight = 0;
    public int PathDistance;
    public bool Marked = false;

    public bool Passable = true;

    public Vector2 Vectorfield;

    public Node Parent;

    public int gridX;
    public int gridY;
}
