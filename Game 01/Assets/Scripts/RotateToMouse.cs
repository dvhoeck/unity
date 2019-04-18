using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    public Camera Camera;
    public float MaximumLength;

    private GameObject _objectHit;
    private Ray _rayMouse;
    private Vector3 _position;
    private Vector3 _normal;
    private Vector3 _direction;
    private Quaternion _rotation;

    // Start is called before the first frame update
    private void Update()
    {
        if (Camera == null)
        {
            Debug.Log("Cam is null");
            return;
        }

        // get position of object under cursor
        var mousePos = Input.mousePosition;
        _rayMouse = Camera.ScreenPointToRay(mousePos);
        _position = _rayMouse.GetPoint(MaximumLength);

        // try to get the normal of the point we hit if we hit an Enemy
        if (Physics.Raycast(_rayMouse.origin, _rayMouse.direction, out RaycastHit hit, MaximumLength * 100) && hit.rigidbody?.gameObject?.tag == "Enemies")
        {
            Debug.Log(hit.rigidbody?.gameObject?.tag);

            RotateToMouseDirection(gameObject, hit.point);

            _objectHit = hit.rigidbody.gameObject;
            _position = hit.point;
            _normal = hit.normal;
        }
        else
        {
            RotateToMouseDirection(gameObject, _position);

            _normal = _position;
            _objectHit = null;
        }
    }

    /// <summary>
    /// Rotate this object to a destination
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="destination"></param>
    private void RotateToMouseDirection(GameObject gameObject, Vector3 destination)
    {
        _direction = destination - gameObject.transform.position;
        _rotation = Quaternion.LookRotation(_direction);
        gameObject.transform.localRotation = Quaternion.Lerp(gameObject.transform.rotation, _rotation, 1);
    }

    public Quaternion GetRotation()
    {
        return _rotation;
    }

    public Vector3 GetHitObjectPosition()
    {
        return _position;
    }

    public Vector3 GetHitObjectNormal()
    {
        return _normal;
    }

    public GameObject GetHitObject()
    {
        return _objectHit;
    }
}