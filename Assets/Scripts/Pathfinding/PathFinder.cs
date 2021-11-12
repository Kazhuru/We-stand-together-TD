using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    [SerializeField] Vector2Int destinationCoordinates;

    private GridManager gridManager;

    private Node currentSearchNode;
    private Queue<Node> searchQueue = new Queue<Node>();
    private Dictionary<Vector2Int, Node> reachedNodes = new Dictionary<Vector2Int, Node>();

    private Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

    public Vector2Int GetStartCoordinates { get => startCoordinates; }
    public Vector2Int GetDestinationCoordinates { get => destinationCoordinates; }

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    /// <summary>
    /// Generate the node path from the defined starting point.
    /// </summary>
    /// <returns></returns>
    public List<Node> GetNewPath()
    {
        ResetVariables();
        BreadhFirstSearch(startCoordinates);
        return BuildPath();
    }

    /// <summary>
    /// Generate the node path from a specific starting point.
    /// </summary>
    /// <param name="starSearchCoordinates"></param>
    /// <returns></returns>
    public List<Node> GetNewPath(Vector2Int starSearchCoordinates)
    {
        ResetVariables();
        BreadhFirstSearch(starSearchCoordinates);
        return BuildPath();
    }

    private void ResetVariables()
    {
        searchQueue.Clear();
        reachedNodes.Clear();
        gridManager.ResetNodes();
    }

    private void BreadhFirstSearch(Vector2Int starSearchCoordinates)
    {
        if (gridManager == null) { return; }
        bool isRunning = true;

        Node startSearchNode = gridManager.GetNode(starSearchCoordinates);
        reachedNodes.Add(starSearchCoordinates, startSearchNode);
        searchQueue.Enqueue(startSearchNode);
        
        while (searchQueue.Count > 0 && isRunning)
        {
            currentSearchNode = searchQueue.Dequeue();
            currentSearchNode.isExplored = true;

            List<Node> exploredNeighbors = ExploreNeighbors();
            foreach (Node neighbor in exploredNeighbors)
            {
                if(!reachedNodes.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
                {
                    neighbor.connectedTo = currentSearchNode;
                    reachedNodes.Add(neighbor.coordinates, neighbor);
                    searchQueue.Enqueue(neighbor);
                }
            }

            if(currentSearchNode.coordinates == destinationCoordinates)
            {
                isRunning = false;
                searchQueue.Clear();
            }
        }
    }

    private List<Node> ExploreNeighbors()
    {
        List<Node> neighborsList = new List<Node>();

        for (int i = 0; i < directions.Length; i++)
        {
            Vector2Int neighborCoords = new Vector2Int(
                currentSearchNode.coordinates.x + directions[i].x,
                currentSearchNode.coordinates.y + directions[i].y);

            Node neighborNode = gridManager.GetNode(neighborCoords);
            if (neighborNode != null)
                neighborsList.Add(neighborNode);
        }

        return neighborsList;
    }

    private List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();

        Node currentNode = gridManager.GetNode(destinationCoordinates);
        do
        {
            path.Add(currentNode);
            currentNode.isPath = true;

            currentNode = currentNode.connectedTo;
        } while (currentNode != null);

        path.Reverse();
        return path;
    }

    public bool WillBlockPath(Vector2Int coordinate)
    {
        Node nodeToCheck = gridManager.GetNode(coordinate);
        if(nodeToCheck != null)
        {
            bool prevState = nodeToCheck.isWalkable;

            nodeToCheck.isWalkable = false;
            List<Node> newPath = GetNewPath();
            nodeToCheck.isWalkable = prevState;

            if(newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }
        return false;
    }

    public void RecalculateEnemyPaths()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }
}
