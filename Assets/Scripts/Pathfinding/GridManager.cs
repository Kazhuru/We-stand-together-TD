using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize;
    [Tooltip("World Grid Size - Should match Unity Editor Settings")]
    [SerializeField] private int worldGridSize = 10;

    private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    public Dictionary<Vector2Int, Node> Grid { get => grid; }
    public int UnityGridSize { get => worldGridSize; set => worldGridSize = value; }

    private void Awake()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        for (int x = 0; x <= gridSize.x; x++)
        {
            for (int y = 0; y <= gridSize.y; y++)
            {
                Vector2Int coords = new Vector2Int(x, y);
                grid.Add(coords, new Node(coords, true));
            }
        }
    }

    public Node GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
            return grid[coordinates];
        else
            return null;
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coords = new Vector2Int();
        coords.x = Mathf.RoundToInt(position.x / worldGridSize);
        coords.y = Mathf.RoundToInt(position.z / worldGridSize);
        return coords;
    }

    public Vector3 GetPostionFromCoordinates(Vector2Int coordinates)
    {
        return new Vector3(coordinates.x * worldGridSize, 0, coordinates.y * worldGridSize);
    }

    public void BlockNode(Vector2Int nodeCoords)
    {
        if (grid.ContainsKey(nodeCoords))
            grid[nodeCoords].isWalkable = false;
    }

    public void ResetNodes()
    {
        foreach (var gridEntry in grid)
            gridEntry.Value.Reset();
    }
}
