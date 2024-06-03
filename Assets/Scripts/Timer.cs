using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeToCompleteQuestion = 10f;
    [SerializeField] private float timeToShowCorrectAnswer = 5f;
    
    public bool isAnsweringQuestion = false;
    public float fillFraction;
    public bool loadNextQuestion;
    
    private float _timerValue;
    
    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        _timerValue = 0f;
    }

    void UpdateTimer()
    {
        _timerValue -= Time.deltaTime;

        if (isAnsweringQuestion)
        {
            if (_timerValue > 0)
            {
                fillFraction = _timerValue / timeToCompleteQuestion; // Example: 0/10=0; 5/10=0.5; 10/10=1.0;
            }
            else
            {
                isAnsweringQuestion = false;
                _timerValue = timeToShowCorrectAnswer;
            }
        }
        else
        {
            if (_timerValue > 0)
            {
                fillFraction = _timerValue / timeToShowCorrectAnswer;
            }
            else
            {
                isAnsweringQuestion = true;
                _timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
        }
    }
}
