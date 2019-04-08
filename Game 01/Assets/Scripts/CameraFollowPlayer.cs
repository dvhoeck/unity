using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
	public GameObject player;
	private Vector3 _offset;
    
    // Start is called before the first frame update
    void Start()
    {
        _offset =  transform.position - player.transform.position;
    }

    void LateUpdate()
    {
		//transform.position = new Vector3(_offset.x, _offset.y, _offset.z + player.transform.position.z);        
    }
}
