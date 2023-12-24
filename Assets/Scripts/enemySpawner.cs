using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public float spawnRate = 2.0f;
    public GameObject[] enemyPrefabs;

    private void Start(){
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner(){
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while(true){
            yield return wait;
            // int rand = Random.Range(0, enemyPrefabs.Length);
            // GameObject enemyToSpawn = enemyPrefabs[rand];
            
            Instantiate(enemyPrefabs[0], transform.position, Quaternion.identity);
        }
    }
}
