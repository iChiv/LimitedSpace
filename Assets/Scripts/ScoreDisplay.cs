using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public void UpdateScoreDisplay(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
