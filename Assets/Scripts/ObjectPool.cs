using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] [Range(0.1f, 30f)] private float spawnTimer = 3f;
    [SerializeField] [Range(0, 50)] private int poolSize = 5;
    [SerializeField] private bool isSpawning = false;

    private Coroutine spawningCoroutine;
    private GameObject[] pool;

    private void Awake()
    {
        PopulatePool();
    }

    void Update()
    {
        InstanceGameObjects();
    }

    private void PopulatePool()
    {
        pool = new GameObject[poolSize];
        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(spawnPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    private void InstanceGameObjects()
    {
        if (spawningCoroutine == null)
        {
            if (isSpawning)
            {
                spawningCoroutine = StartCoroutine(SpawnGameObjects());
            }
        }
        else
        {
            if (!isSpawning)
            {
                StopCoroutine(spawningCoroutine);
                spawningCoroutine = null;
            }
        }
    }

    private void EnableObjectInPool()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }

    private IEnumerator SpawnGameObjects()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
