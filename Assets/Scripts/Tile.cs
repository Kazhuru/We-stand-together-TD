using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Tower towerPrefab;
    [SerializeField] private bool isPlaceable;
    
    private GridManager gridManager;
    private PathFinder pathFinder;
    private Vector2Int coordinates = new Vector2Int();

    public bool IsPlaceable { get => isPlaceable; }
    public Vector2 TileCoordinates { get => coordinates;}

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    private void Start()
    {
        if(gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    private void OnMouseDown()
    {
        if(gridManager == null) { return; }

        if (gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates))
        {
            bool wasCreated = towerPrefab.Create(transform.position);
            if (wasCreated)
            {
                gridManager.BlockNode(coordinates);
                pathFinder.RecalculateEnemyPaths();
            } 
        }
    }
}
