using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float Speed = 1;
    public int Count;
    public Text CountText;
    public Text WinText;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Count = 0;
        SetCountText();

        WinText.text = "";
    }

    void FixedUpdate()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        var movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * Speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pick Up")
        {
            other.gameObject.SetActive(false);
            Count += 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        CountText.text = "Count: " + Count.ToString();

        if(Count == 12)
            WinText.text = "You win!";
    }
}
