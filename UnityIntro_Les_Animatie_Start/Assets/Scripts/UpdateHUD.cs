using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHUD : MonoBehaviour
{
    public Text scoreText;
    public Text healthText;
    public Text gameOverText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // display health and score
        scoreText.text = "Score: " + GameManager.score.ToString();
        healthText.text = "Health: " + GameManager.health.ToString();

        if (GameManager.health <= 0) // if health is zero or less, display game over message
        {
            gameOverText.text = "GAME OVER";
            Debug.Log("GAME OVER");
        }
    }
}
