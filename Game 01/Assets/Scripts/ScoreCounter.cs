using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public int Score;
    public GameObject TextObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(TextObject != null)
        {
            TextObject.GetComponent<Text>().text = "Score: " + Score;
        }
    }

    public void AddScore(int scoreModifier)
    {
        Score += scoreModifier;
    }
}
