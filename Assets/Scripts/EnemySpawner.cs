using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Magic.Pooling;

public class EnemySpawner : MonoBehaviour {

    public float spawnRange;
    public float spawnRate;
    public float spawnRadius;
    public GameObject enemyPrefab;

    private Transform player;
    private float distanceToPlayer;
    private float timer = 0;

	void Update ()
    {
        if (player == null)
        {
            player = GlobalRef.playerPos;
        }

        if (PlayerWithinRange())
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                float yPos = Mathf.Sin(Random.Range(-1.0f, 1.0f)) * spawnRadius;
                float xPos = Mathf.Cos(Random.Range(-1.0f, 1.0f)) * spawnRadius;
                Vector3 spawnPos = new Vector3(transform.position.x + xPos, transform.position.y, transform.position.z + yPos);
                GameObject enemyRef = PoolManager.Spawn(enemyPrefab, spawnPos, Quaternion.identity, "enemies");
                enemyRef.GetComponent<EnemyBehav>().health = 100;
                timer = spawnRate;
            }
        }
	}

    bool PlayerWithinRange()
    {
        distanceToPlayer = Vector3.Distance(player.gameObject.transform.position, transform.position);
        if (distanceToPlayer <= spawnRange)
            return true;
        return false;
    }
}
