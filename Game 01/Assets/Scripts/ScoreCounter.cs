using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public int Score;
    public GameObject ScoreTextObject;
    public GameObject HighScoreTextObject;

    private int _highScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ScoreTextObject != null)
        {
            ScoreTextObject.GetComponent<Text>().text = "Score: " + Score;
        }

        if (HighScoreTextObject != null)
        {
            if (_highScore < Score)
                _highScore = Score;

            HighScoreTextObject.GetComponent<Text>().text = "Hi-score: " + _highScore;
        }
    }

    public void AddScore(int scoreModifier)
    {
        Score += scoreModifier;
    }
}
