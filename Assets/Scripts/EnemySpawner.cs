using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f, 120f)]
    [SerializeField] float secondsBetweenSpawns = 5f;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] Transform enemiesParentTransform;
    [SerializeField] int enemiesSpawned;
    [SerializeField] Text enemyText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RepeatedlySpawnEnemies());
        enemyText.text = enemiesSpawned.ToString();
    }

    IEnumerator RepeatedlySpawnEnemies()
    {
        while (true) //forever
        {
            AddScore();
            var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.transform.parent = enemiesParentTransform;
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }

    private void AddScore()
    {
        enemiesSpawned++;
        enemyText.text = enemiesSpawned.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
