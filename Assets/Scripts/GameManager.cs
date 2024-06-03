using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Quiz _quiz;
    private EndScene _endScene;

    private void Awake()
    {
        _quiz = FindObjectOfType<Quiz>();
        _endScene = FindObjectOfType<EndScene>();
    }

    void Start()
    {
        _quiz.gameObject.SetActive(true);
        _endScene.gameObject.SetActive(false);
    }
    
    void Update()
    {
        if (_quiz.isComplete)
        {
            _quiz.gameObject.SetActive(false);
            _endScene.gameObject.SetActive(true);
            _endScene.ShowFinalScore();
        }
    }

    public void OnReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
