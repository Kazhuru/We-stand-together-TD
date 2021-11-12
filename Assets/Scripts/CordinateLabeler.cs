
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = Color.magenta;

    private TextMeshPro label;
    private Vector2Int coords = new Vector2Int();
    private GridManager gridManager;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;

        DisplayCoordinates();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }

        SetLabelsOnRuntime();
        ToggleLabels();
    }

    private void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
            label.enabled = !label.IsActive();
    }

    private void SetLabelsOnRuntime()
    {
        if(gridManager == null) { return;  }

        Node node = gridManager.GetNode(coords);
        if (node == null) { return; }

        if (!node.isWalkable)
            label.color = blockedColor;
        else if (node.isPath)
            label.color = pathColor;
        else if (node.isExplored)
            label.color = exploredColor;
        else
            label.color = defaultColor;
    }

    private void DisplayCoordinates()
    {
        if (gridManager == null) { return; }

        coords.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coords.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);
        label.text = coords.x + " , " + coords.y;
    }

    private void UpdateObjectName()
    {
        transform.parent.name = "Tile " + coords.ToString();
    }
}
