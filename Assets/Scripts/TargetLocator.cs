using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] private Transform weapon;
    [SerializeField] private ParticleSystem projectileParticles;
    [SerializeField] private float maxAttackRange = 5f;
    [SerializeField] private float yPosFix = 0;
    [SerializeField] private AudioClip shootAudio;
    [SerializeField] [Range(0f, 1f)] private float shootAudioVolume = 1f;

    private Transform target;
    private ParticleSystem particleSys;
    private int currentNumOfParticles = 0;

    private void Start()
    {
        particleSys = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        ShootAudio();
        FindClosestTarget();
        Attack();

    }

    private void ShootAudio()
    {
        if (particleSys.particleCount > currentNumOfParticles)
            AudioSource.PlayClipAtPoint(shootAudio, Camera.main.transform.position, shootAudioVolume);
        currentNumOfParticles = particleSys.particleCount;
    }

    private void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if(targetDistance < closestDistance)
            {
                closestTarget = enemy.transform;
                closestDistance = targetDistance;
            }
        }

        target = closestTarget;
    }

    private void Attack()
    {
        float targetDistance = Mathf.Infinity;
        var projectileEmission = projectileParticles.emission;

        if (target != null)
            targetDistance = Vector3.Distance(transform.position, target.position);
        
        if (targetDistance < maxAttackRange)
        {
            AimWeapon();
            projectileEmission.enabled = true;
        }
        else
        {
            projectileEmission.enabled = false;
        }

    }

    private void AimWeapon()
    {
        weapon.LookAt(new Vector3(target.position.x, target.position.y + yPosFix, target.position.z));
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("SHOT!");
    }

    private void OnParticleTrigger()
    {
        Debug.Log("SHOT!");
    }
}
