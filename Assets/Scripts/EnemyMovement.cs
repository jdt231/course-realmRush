using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] float movementPeriod = 0.5f;
    [SerializeField] ParticleSystem finishedParticlePrefab;

    void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.GetPath();

        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
        //print("Starting patrol..."); //TODO remove when finished
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(movementPeriod);
        }

        Invoke("FinishPatrol", 0.5f);
    }
    private void FinishPatrol()
    {
        print("Mission complete! Self destructing...");

        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        playerHealth.ReduceBaseHealth();

        var finishedFX = Instantiate(finishedParticlePrefab, transform.position, Quaternion.identity);
        finishedFX.Play();
        Destroy(finishedFX.gameObject, finishedFX.main.duration);

        Destroy(gameObject);
    }

}
