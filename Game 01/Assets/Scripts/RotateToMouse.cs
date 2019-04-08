using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    public Camera Camera;
    public float MaximumLength;

    private Ray _rayMouse;
    private Vector3 _position;
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

        RaycastHit hit;
        var mousePos = Input.mousePosition;
        _rayMouse = Camera.ScreenPointToRay(mousePos);
        var position = _rayMouse.GetPoint(MaximumLength);

        if (Physics.Raycast(_rayMouse.origin, _rayMouse.direction, out hit, MaximumLength * 100) && hit.rigidbody?.gameObject?.tag == "Enemies")
        {
            Debug.Log(hit.rigidbody?.gameObject?.tag);

            //if (hit.rigidbody?.gameObject?.tag == "Enemies")
                RotateToMouseDirection(gameObject, hit.point);
            //else
            //    RotateToMouseDirection(gameObject, position);
        }
        else
        {
            RotateToMouseDirection(gameObject, position);
        }
    }

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
}