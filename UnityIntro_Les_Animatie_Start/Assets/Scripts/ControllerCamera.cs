using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCamera : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    float damping = 8.0f;

    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
            throw new InvalidOperationException("Player is null");

		// store the offset between the player's position and the camera's position 
        offset = player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		// move the camera along with the player by subtracting the offset from the player's position
        Vector3 newPosition = player.transform.position - offset;
		
		// use linear interpolation to smooth out movement and create a little delay
        newPosition = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * damping);
        transform.position = newPosition;
    }
}
