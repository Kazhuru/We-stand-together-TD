using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2Int coordinates;
    public bool isWalkable;
    public bool isExplored;
    public bool isPath;
    public Node connectedTo;

    public Node(Vector2Int coordinates, bool isWalkable)
    {
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
        this.isExplored = false;
        this.isPath = false;
        this.connectedTo = null;
    }

    public Node(Vector2Int coordinates, bool isWalkable, bool isExplored, bool isPath, Node connectedTo)
    {
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
        this.isExplored = isExplored;
        this.isPath = isPath;
        this.connectedTo = connectedTo;
    }


    public void Reset()
    {
        isExplored = false;
        isPath = false;
        connectedTo = null;
    }
}
