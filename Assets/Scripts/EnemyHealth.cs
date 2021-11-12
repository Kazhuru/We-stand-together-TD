using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHP = 2;
    [Tooltip("Adds amount of maxHitPoints when enemy")]
    [SerializeField] int difficultyRamp = 1;

    private int currentHP;
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        currentHP = maxHP;
    }


    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        currentHP--;
        
        if (currentHP <= 0)
        {
            enemy.RewardGold();
            maxHP += difficultyRamp;
            gameObject.SetActive(false);
        } 
    }
}
