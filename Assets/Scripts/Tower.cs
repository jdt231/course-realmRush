using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //Params
    [SerializeField] Transform objectToPan;
    [SerializeField] float attackRange = 10f;
    [SerializeField] ParticleSystem projectileParticle;

    public Waypoint baseWaypoint;

    //state of each tower
    Transform targetEnemy;

    void Update()
    {
        SetTargetEnemy();
        if (targetEnemy)
        {
            objectToPan.LookAt(targetEnemy);
            FireAtEnemy();
        }
        else 
        {
            Shoot(false);    
        }
    }

    private void SetTargetEnemy()                                           // allocate a target for the turret to shoot at.
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();                // find all enemies (only enemies will have the "EnemyDamage" component)
        if (sceneEnemies.Length == 0) { return; }                           // if none are found, stop the method (return).

        Transform closestEnemy = sceneEnemies[0].transform;                 // "sceneEnemies[0]" means the first in the array of enemies.

        foreach (EnemyDamage testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);   // feeding two things into the method to compare - the current closest (which is currently just the first in the array), and then the next in the array each time (as it's a foreach loop).
        }

        targetEnemy = closestEnemy;
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        var distToA = Vector3.Distance(transform.position, transformA.position);    // use "Vector3.Distance" to compare the distance between the tower (transform.position) and the current closest enemy (transformA.position)
        var distToB = Vector3.Distance(transform.position, transformB.position);    // do the same but against the next enemy being compared (transformB.position)

        if (distToA < distToB)
        {
            return transformA;
        }

        return transformB;
    }

    private void FireAtEnemy()
    {
        float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);
        if (distanceToEnemy <= attackRange)
        {
            Shoot(true);
        }
        else
        {
            Shoot(false);
        }
    }

    private void Shoot(bool isActive)
    {
        var emissionModule = projectileParticle.emission;
        emissionModule.enabled = isActive;
    }
}
