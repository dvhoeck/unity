using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSpawns : MonoBehaviour
{
    /// <summary>
    /// A variable for our enemy prefab
    /// </summary>
    public GameObject ObjectToSpawn;

    /// <summary>
    /// Time before the enemy gets despawned (cleaned up)
    /// </summary>
    public float TimeToLive = 30.0f;

    float spawnTime;
    float timeSinceLastSpawn = 0;

    // Start is called before the first frame 
    void Start()
    {
        spawnTime = Random.Range(3, 7);
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn > spawnTime)
        {
            // create new enemy and assign this enemy to the newEnemy variable
            var newEnemy = GameObject.Instantiate(ObjectToSpawn, this.transform.position, this.transform.rotation);

            // make sure the new enemy gets cleaned up after TimeToLive seconds
            Destroy(newEnemy, TimeToLive);

            // reset timeout counter
            timeSinceLastSpawn = 0;

            // create a new spawn timeout between 3 and 7 (the second parameter is exclusive, 
            // this means "up to but not including". So to get a number from 3 to 7 we need to
            // set the max range to 8
            spawnTime = Random.Range(3, 8);

            
        }
    }
}
