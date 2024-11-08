using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public GameObject scoreEntryPrefab; // Assign the ScoreEntry prefab in the inspector
    public Transform contentContainer;  // Assign the ScrollView's Content object in the inspector
    public ScoreManager scoreManager;

    void Start()
    {

    }

    void OnEnable()
    {
        Debug.Log(scoreManager);
        if (scoreManager == null) return;

        DisplayScores();
    }

    void DisplayScores()
    {
        ClearScoreEntries(); // Clear previous entries

        List<Score> scores = scoreManager.LoadScores();
        foreach (Score score in scores)
        {
            GameObject entry = Instantiate(scoreEntryPrefab, contentContainer);
            TextMeshProUGUI[] texts = entry.GetComponentsInChildren<TextMeshProUGUI>();

            if (texts.Length >= 2)
            {
                texts[0].text = score.name;   // Assign player name
                texts[1].text = score.score.ToString(); // Assign player score
                texts[2].text = score.difficulty;
            }
        }
    }

    void ClearScoreEntries()
    {
        foreach (Transform child in contentContainer)
        {
            if(child.name != "Scoreboard Header")
                Destroy(child.gameObject);
        }
    }
}