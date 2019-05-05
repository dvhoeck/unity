using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerEnemy : MonoBehaviour
{
    public Vector3 walkDirection;
    public float walkForce = 10;

    // Start is called before the first frame update
    void Start()
    {
        NewRandomDirection();
    }

    // Update is called once per frame
    void Update()
    {
        // get the rigid body component of the enemy prefab this script is assigned to
        Rigidbody rb = GetComponent<Rigidbody>();

        // create movement by adding force
        rb.AddForce(walkForce * walkDirection);

        // check for player death
        if (GameManager.health <= 0) {
            SceneManager.LoadScene(1);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "FPSController")
        {
            // if possible, decrease health by 10
            if (GameManager.health > 0)
            {
                GameManager.health -= 10;
            }
        }
        else
        {
            // we don't need to change direction when we hit the floor
            if (collision.gameObject.name != "Floor")
            {
                NewRandomDirection();
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // make sure we create a new direction if we're stuck in a collision (again, disregard the floor)
        if (collision.gameObject.name != "Floor")
        {
            NewRandomDirection();
        }
    }

    private void NewRandomDirection()
    {
        Debug.Log("I hit something.... damn, i will catch you!!!");

        // create a new direction by randomizing x and z from -1 to 1
        walkDirection = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), walkDirection.y, UnityEngine.Random.Range(-1.0f, 1.0f));

        // normalize: make sure the total velocity does not exceed 1
        walkDirection.Normalize();
    }
}
