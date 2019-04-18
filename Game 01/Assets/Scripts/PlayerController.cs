using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float HitPoints = 1;
    public Image HPCurrentUiObject;
    public GameObject GameMenuUIObject;
    public bool PlayerIsDead = false;

    private Rigidbody _rigidbody;
    private float _maxHitPoints;

    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        if (HPCurrentUiObject == null)
            throw new InvalidOperationException("Current HP UI object is null");

        if (GameMenuUIObject == null)
            throw new InvalidOperationException("Game Over UI object is null");

        _maxHitPoints = HitPoints;

        // DEBUG
        //KillPlayer();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        // movement
        DoMovement();

        // toggle Game Menu display when escape is pressed and released
        if (Input.GetKeyUp(KeyCode.Escape))
            ToggleGameMenu();
    }

    /// <summary>
    /// Toggles the ingame menu
    /// </summary>
    private void ToggleGameMenu()
    {
        GameMenuUIObject.SetActive(!GameMenuUIObject.activeSelf);
    }

    private void DoMovement()
    {
        // crash player ship on HP = 0
        if (PlayerIsDead)
        {
            var killMovement = new Vector3(0.0f, -0.25f, -1.05f);
            _rigidbody.AddForce(killMovement * Speed);
            return;
        }

        float moveH, inputH, moveV, inputV;
        moveH = moveV = 0.0f;
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");

        // movement is limited to within 10% of the screen border and only movement on X and Y is allowed
        if (inputH != 0)
        {
            if (inputH < 0) // go left
                moveH = Camera.main.WorldToScreenPoint(_rigidbody.position).x <= (0 + (Screen.width * 0.1)) ? 0.0f : inputH;
            else // go right
                moveH = Camera.main.WorldToScreenPoint(_rigidbody.position).x >= (Screen.width - (Screen.width * 0.1)) ? 0.0f : inputH;
        }

        if (inputV != 0)
        {
            if (inputV < 0) // go down
                moveV = Camera.main.WorldToScreenPoint(_rigidbody.position).y <= (0 + (Screen.height * 0.1)) ? 0.0f : inputV;
            else // go up
                moveV = Camera.main.WorldToScreenPoint(_rigidbody.position).y >= (Screen.height - (Screen.height * 0.1)) ? 0.0f : inputV;
        }

        // move player ship by adding force on X and Y axis
        var movement = new Vector3(moveH, moveV, 0.0f);
        _rigidbody.AddForce(movement * Speed);

        // rotate ship according to x and y velocity to simulate aircraft attitude
        transform.localRotation = Quaternion.Euler(-6 * _rigidbody.velocity.y, 0, -6 * _rigidbody.velocity.x);
    }

    internal void PlayerIsHit(float damage = 1)
    {
        // set score to 0
        var scoreCounter = Camera.main.GetComponent<ScoreCounter>();
        scoreCounter.AddScore(-scoreCounter.Score);

        // deal damage
        HitPoints -= damage;

        // decrease current HP UI bar
        var newBarSize = HitPoints / _maxHitPoints;
        HPCurrentUiObject.rectTransform.localScale = new Vector3(newBarSize, 1, 1);

        // check if player dies
        if (HitPoints <= 0)
            KillPlayer();
    }

    private void KillPlayer()
    {
        // set hit points to 0
        HitPoints = 0;

        // set a flag to indicate player is dead
        PlayerIsDead = true;

        // show game over menu
        GameMenuUIObject.SetActive(true);
        var textGameOver = GameMenuUIObject.transform.Find("GameOver").gameObject;
        textGameOver.SetActive(true);

        // stop engine sound
        var jetAuduio = GetComponentsInChildren<AudioSource>();
        jetAuduio.ToList().ForEach(audio => audio.Stop());

        // play death sound
        var deathAudio = GetComponent<AudioSource>();
        deathAudio.Play();

        // destroy player ship when death sound has done playing
        Destroy(gameObject, 13.5f);
    }
}