using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIBehaviour : MonoBehaviour
{
    public TextMeshPro scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = transform.Find("ScoreText").GetComponent<TextMeshPro>();
    }

    public void UpdateScore(int score) {
        Debug.Log(score);
        scoreText.text = string.Format("score : {0}", score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
