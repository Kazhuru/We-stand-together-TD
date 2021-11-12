using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int goldCost = 25;
    [SerializeField] [Range(0f, 1f)] float buildDelay = 0.5f;
    [SerializeField] private AudioClip buildAudio;
    [SerializeField] [Range(0f, 1f)] private float buildAudioVolume = 1f;

    private void Start()
    {
        StartCoroutine(BuildTowerDelay());
    }

    public bool Create(Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();
        bool isCreated = false;

        if (bank == null) { return isCreated; }

        if (bank.CurrentBalance >= goldCost)
        {
            bank.Withdraw(goldCost);
            Instantiate(gameObject, position, Quaternion.identity);
            isCreated = true;
        }

        return isCreated;
    }

    IEnumerator BuildTowerDelay()
    {
        AudioSource.PlayClipAtPoint(buildAudio,
            Camera.main.transform.position,
            buildAudioVolume);

        foreach (Transform child in transform)
            child.gameObject.SetActive(false);

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);
        }  
    }

    

}
