using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField]  [Range(0f, 5f)] float speed = 1f;

    private Enemy enemy;
    private List<Node> path = new List<Node>();
    private GridManager gridManager;
    private PathFinder pathFinder;
    private Coroutine moverCoroutine;

    private void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
        
    }

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    private void RecalculatePath(bool resetPath)
    {
        Vector2Int startingCoords = new Vector2Int();
        if (resetPath)
            startingCoords = pathFinder.GetStartCoordinates;
        else
            startingCoords = gridManager.GetCoordinatesFromPosition(transform.position);

        path.Clear();
        if (moverCoroutine != null)
            StopCoroutine(moverCoroutine);

        path = pathFinder.GetNewPath(startingCoords);
        moverCoroutine = StartCoroutine(PathMovement());
    }

    private void ReturnToStart()
    {
        transform.position = gridManager.GetPostionFromCoordinates(pathFinder.GetStartCoordinates);
    }

    private IEnumerator PathMovement()
    {
        for (int i = 1; i < path.Count; i++)
        {
            float travelPercentaje = 0f;
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPostionFromCoordinates(path[i].coordinates);
            transform.LookAt(endPosition);
            while (travelPercentaje < 1f)
            {
                travelPercentaje += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercentaje);
                yield return new WaitForEndOfFrame();
            }
        }

        EndOfThePath();
    }

    private void EndOfThePath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }
}
