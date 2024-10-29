using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>();
        scoreManager = GameObject.FindAnyObjectByType<ScoreManager>();
        Debug.Log(scoreManager);
    }

    public void UpdateScore(int score) {
        Debug.Log("UI managaer : " + score);
        Debug.Log("scoreText" + scoreText);
        scoreText.text = $"score : {score}";
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
