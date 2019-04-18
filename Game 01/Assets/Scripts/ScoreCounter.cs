using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public int Score;
    public float AmmoCount;
    public GameObject ScoreTextObject;
    public GameObject HighScoreTextObject;
    public GameObject AmmoTextObject;

    private int _highScore;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (ScoreTextObject != null)
        {
            ScoreTextObject.GetComponent<Text>().text = "Score: " + Score;
        }

        if (HighScoreTextObject != null)
        {
            if (_highScore < Score)
                _highScore = Score;

            HighScoreTextObject.GetComponent<Text>().text = "Hi-score: " + _highScore;
        }

        if (AmmoTextObject != null)
        {
            AmmoTextObject.GetComponent<Text>().text = "Ammo count: " + AmmoCount;
        }
    }

    public void AddScore(int scoreModifier)
    {
        Score += scoreModifier;
    }

    internal void SetFire02Ammo(float fire02AmmoCount)
    {
        AmmoCount = fire02AmmoCount;
    }
}