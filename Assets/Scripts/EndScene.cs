using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScoreText;
    private ScoreKeeper _scoreKeeper;
    
    void Awake()
    {
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore()
    {
        if(_scoreKeeper.CalculateScore() <= 60)
        {
            finalScoreText.text = $"Better Luck next time.\n You got a score of: {_scoreKeeper.CalculateScore()}%";
        }
        else if (_scoreKeeper.CalculateScore() >= 61 && _scoreKeeper.CalculateScore() < 100)
        {
            finalScoreText.text = $"Congratulations!\n You got a score of: {_scoreKeeper.CalculateScore()}%";
        }
        else if(_scoreKeeper.CalculateScore() >= 100)
        {
            finalScoreText.text = "Perfect Score!\n You got a score of: 100%";
        }
        
    }


}
