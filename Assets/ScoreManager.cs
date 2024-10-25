using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score
{
    public string name;
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
    public Score score;
    public UIBehaviour UI;


    // Start is called before the first frame update
    public const string FILE_PATH = "save.json";
    void Start()
    {
        UI = GameObject.FindAnyObjectByType<UIBehaviour>();
        score = new Score();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementScore() { 
        score.score ++;
        UI.UpdateScore(score.score);
    }

    public void saveScore(Score score) {
        string potion = JsonUtility.ToJson(score);
        System.IO.File.WriteAllText(Application.persistentDataPath + FILE_PATH, potion);
    }
}
