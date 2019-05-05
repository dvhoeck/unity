using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSpin : MonoBehaviour
{
    /// <summary>
    /// Angular (or in this case rotational) speed
    /// </summary>
    public float angularSpeed = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // rotate the object this script is assigned to
        transform.Rotate(new Vector3(1, 0, 0), angularSpeed);
    }
}
