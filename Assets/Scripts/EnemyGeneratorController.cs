using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float firstInvocationDelay = 0.5f;
    public float invocationRate = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    void StartEnemyGeneration()
    {
        InvokeRepeating("CreateEnemy", firstInvocationDelay, invocationRate);
    }
    void CancelEnemyGeneration(bool clearAllEnemies)
    {
        CancelInvoke("CreateEnemy");
        if (clearAllEnemies)
        {
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(enemy);
            }
        }
    }
}
