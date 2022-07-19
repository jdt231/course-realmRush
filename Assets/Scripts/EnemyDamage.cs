using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int hitPoints = 10;
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] ParticleSystem deathParticlePrefab;

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if(hitPoints <= 0 )
        {
            KillEnemy();
        }
    }

    void ProcessHit() 
    {
        hitPoints = hitPoints - 1;
        hitParticlePrefab.Play();
        //print("current hitpoints are " + hitPoints); //TODO - remove when finished.
    }
    
    private void KillEnemy()
    {
        var deathFX = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        deathFX.Play();
        Destroy(deathFX.gameObject, deathFX.main.duration);

        Destroy(gameObject);
    }



}
