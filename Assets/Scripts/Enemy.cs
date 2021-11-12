using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int goldReward = 25;
    [SerializeField] private int goldPenalty = 25;
    [SerializeField] private AudioClip stealGoldAudio;
    [SerializeField] [Range(0f, 1f)] private float stealGoldAudioVolume = 1f;

    private Bank bank;

    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void RewardGold()
    {
        if (bank == null) { return; }
        bank.Deposit(goldReward);
    }

    public void StealGold()
    {
        if (bank == null) { return; }
        bank.Withdraw(goldPenalty);
        AudioSource.PlayClipAtPoint(stealGoldAudio,
            Camera.main.transform.position, 
            stealGoldAudioVolume);
    }
}
