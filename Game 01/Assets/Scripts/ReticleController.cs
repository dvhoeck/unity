using UnityEngine;

public class ReticleController : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        // make the reticle follow the mouse
        transform.position = Input.mousePosition;
    }
}