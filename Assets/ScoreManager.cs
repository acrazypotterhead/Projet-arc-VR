using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Score
{
    [SerializeField]
    public string name;
    [SerializeField]
    public int score;

    
    public Score(string name, int score)
    {
        this.name = name;
        this.score = score;
    }

    public Score()
    {
        this.name = "";
        this.score = 0;
    }
}

public class ScoreManager : MonoBehaviour
{
    public Score currentScore;
    public UIBehaviour UI;

    public const string FILE_NAME = "/save.json";

    void Start()
    {
        UI = GameObject.FindAnyObjectByType<UIBehaviour>();
        currentScore = new Score();
    }

    public void IncrementScore()
    {
        currentScore.score++;
        Debug.Log("score : " + currentScore.score);
        UI.UpdateScore(currentScore.score);
    }

    public List<Score> LoadScores()
    {
        string filePath = Application.persistentDataPath + FILE_NAME;
        Debug.Log(filePath);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            if (string.IsNullOrWhiteSpace(json) || json == "{}")
            {
                return new List<Score>();
            }

            ScoreList scoreList = JsonUtility.FromJson<ScoreList>(json);
            return scoreList?.scores ?? new List<Score>();
        }
        else
        {
            return new List<Score>();
        }
    }

    public void AddScore(Score newScore)
    {
        List<Score> scores = LoadScores();
        scores.Add(newScore);

        ScoreList scoreList = new ScoreList(scores);
        string json = JsonUtility.ToJson(scoreList, true);
        Debug.Log(json); // Check if the JSON output is correctly formed
        File.WriteAllText(Application.persistentDataPath + FILE_NAME, json);
    }
}

[System.Serializable]
public class ScoreList
{
    [SerializeField]
    public List<Score> scores = new List<Score>(); // Initialize the list to prevent null

    public ScoreList(List<Score> scores)
    {
        this.scores = scores;
    }
}
