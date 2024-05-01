using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text scoreText;
    private float score;
    void Start()
    {
        score = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime;
        int scoreAsInt = Mathf.RoundToInt(score);
        scoreText.text = "Score: " + scoreAsInt;
        
    }

    public void IncreaseScore()
    {
        score += 10;
    }
}
