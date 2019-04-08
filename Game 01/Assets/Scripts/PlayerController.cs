using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        // movement
        float moveH, inputH, moveV, inputV;
        moveH = moveV = 0.0f;
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");

        // movement is limited to within 10% of the screen border and only movement on X and Y is allowed
        if (inputH != 0)
        {
            if (inputH < 0) // go left
            {
                moveH = Camera.main.WorldToScreenPoint(_rigidbody.position).x <= (0 + (Screen.width * 0.1)) ? 0.0f : inputH;

                // bank left, max 30°
            }
            else // go right
            {
                moveH = Camera.main.WorldToScreenPoint(_rigidbody.position).x >= (Screen.width - (Screen.width * 0.1)) ? 0.0f : inputH;

                // bank right, max 30°

            }
        }

        transform.localRotation = Quaternion.Euler(-6 * _rigidbody.velocity.y, 0,  -6 * _rigidbody.velocity.x);

        if (inputV != 0)
        {
            if (inputV < 0) // go down
                moveV = Camera.main.WorldToScreenPoint(_rigidbody.position).y <= (0 + (Screen.height * 0.1)) ? 0.0f : inputV;
            else // go up
                moveV = Camera.main.WorldToScreenPoint(_rigidbody.position).y >= (Screen.height - (Screen.height * 0.1)) ? 0.0f : inputV;
        }

        var movement = new Vector3(moveH, moveV, 0.0f);
        _rigidbody.AddForce(movement * Speed);
    }
}