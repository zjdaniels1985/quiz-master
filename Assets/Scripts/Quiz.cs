using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] private List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;
    
    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    private int _correctAnswerIndex;
    private bool hasAnsweredEarly = true;
    
    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    
    [Header("Timer")]
    [SerializeField] private Image timerImage;
    private Timer timer;

    [Header("Scoring")] 
    [SerializeField] private TextMeshProUGUI scoreText;
    private ScoreKeeper _scoreKeeper;

    [Header("ProgressBar")] 
    [SerializeField] private Slider progressBar;

    public bool isComplete;
    
    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    private void Update()
    {
        UpdateTimerImage();
        if (timer.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }
            
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    private void UpdateTimerImage()
    {
        timerImage.fillAmount = timer.fillFraction;
    }

    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();
        
        for(int index = 0; index < answerButtons.Length; index++)
        {
            TextMeshProUGUI buttonText = answerButtons[index].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(index);
        }
    }

    void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
            _scoreKeeper.IncrementQuestionsSeen();
        }
    }

    private void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }

    private void SetDefaultButtonSprites()
    {
        for(int index = 0; index < answerButtons.Length; index++)
        {
            Image buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    void SetButtonState(bool state)
    {
        for (int index = 0; index < answerButtons.Length; index++)
        {
            Button button = answerButtons[index].GetComponent<Button>();
            button.interactable = state;
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = $"Score: {_scoreKeeper.CalculateScore()}%";
    }

    private void DisplayAnswer(int index)
    {
        Image buttonImage;

        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            _scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            _correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestion.GetAnswer(_correctAnswerIndex);
            questionText.text = $"Incorrect! The answer is:\n{correctAnswer}";
            buttonImage = answerButtons[_correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }
}
