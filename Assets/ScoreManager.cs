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
}

public class ScoreManager : MonoBehaviour
{



    // Start is called before the first frame update
    public const string FILE_PATH = "save.json";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void saveScore(Score score) {
        string potion = JsonUtility.ToJson(score);
        System.IO.File.WriteAllText(Application.persistentDataPath + FILE_PATH, potion);
    }
}
