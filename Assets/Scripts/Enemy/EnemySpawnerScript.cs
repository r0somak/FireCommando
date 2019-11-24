using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public Transform spawner;
    public Rigidbody enemyPrefab;

    public int spawnSpeed = 4;
    public float rotateSpeed = 20f;

    private int _countdownVal = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateSpawnPoint();

        if (Time.time > _countdownVal)
        {
            _countdownVal += spawnSpeed;
            SpawnEnemy();
        }
    }

    private void RotateSpawnPoint()
    {
        spawner.transform.RotateAround(transform.position, transform.up, rotateSpeed*Time.deltaTime);
    }

    private void SpawnEnemy()
    {
        Rigidbody instance = Instantiate(enemyPrefab, spawner.position, enemyPrefab.rotation);
        instance.velocity = new Vector3(0,0,0);
    }
    
}
