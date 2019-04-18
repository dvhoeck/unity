using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 _offset;

    // Start is called before the first frame update
    private void Start()
    {
        _offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
    }
}