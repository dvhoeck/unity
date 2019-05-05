using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCoin : MonoBehaviour
{
    private int rangeX = 10;
    private int rangeZ = 10; // the available region is 50 x 50, our coin will relocate in a 20 x 20 region around the center

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // we only execute this if the player touches the coin
        if (other.name == "FPSController")
        {
            // increase score
            GameManager.score += 10;
            Debug.Log("Ding-ding! 10 points added!");

            // relocate the coin to a new position in our 20 x 20 grid
            transform.position = new Vector3(
                Random.Range(-rangeX, rangeX),  // randomize X
                transform.position.y,           // do not randomize the height
                Random.Range(-rangeZ, rangeZ)); // randomize Z
        }
    }
}
