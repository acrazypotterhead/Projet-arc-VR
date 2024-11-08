using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>();
        GameObject timer = GameObject.Find("Timer Text");
        if(timer != null)
        {
            timerText = timer.GetComponent<TextMeshProUGUI>();
        }
        scoreManager = GameObject.FindAnyObjectByType<ScoreManager>();
    }

    public void UpdateScore(int score) {

        scoreText.text = $"score : {score}";
    }

    public void UpdateTimer(int timer)
    {
        timerText.text = $"timer: {timer}";
    }

    public void EnableStartSessionButton(bool enable)
    {
        transform.Find("Start Session Button").gameObject.SetActive(enable);
    }

    public void EnableMainMenuButton(bool enable)
    {
        transform.Find("Main Menu Button").gameObject.SetActive(enable);
    }

    public void MainMenuButtonOnClick()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseGamemodeButtonOnClick()
    {
        scoreManager.AddScore(scoreManager.currentScore);
    }
}
