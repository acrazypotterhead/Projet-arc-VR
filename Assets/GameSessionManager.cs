using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSessionManager : MonoBehaviour
{
    public static string SelectedBow { get; set; }
    public static string SelectedArrow { get; set; }
    public static string SelectedDifficulty { get; set; }
    public static string Pseudo { get; set; }

    private bool isSessionActive = false;
    private UIBehaviour uIBehaviour;
    private float sessionTimer = 60f;  // Default session duration for timed modes
    private Coroutine sessionCoroutine;
    private ScoreManager scoreManager;

    private TargetSpawnerBehaviour targetSpawner;

    public void Start()
    {
        targetSpawner = GameObject.FindObjectOfType<TargetSpawnerBehaviour>();
        uIBehaviour = GameObject.FindObjectOfType<UIBehaviour>();
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
    }

    public static void LoadScene()
    {
        Debug.Log(ToString());
        if (SelectedDifficulty == "Training")
            SceneManager.LoadScene("TrainingScene");
        else if (SelectedDifficulty == "Easy")
            SceneManager.LoadScene("EasyScene");
        else if (SelectedDifficulty == "Normal")
            SceneManager.LoadScene("NormalScene");
        else if (SelectedDifficulty == "Hard")
            SceneManager.LoadScene("HardScene");
    }


    public void StartSession()
    {
        scoreManager.currentScore.score = 0;
        uIBehaviour.UpdateScore(0);
        isSessionActive = true;
        targetSpawner.StartSpawning();
        uIBehaviour.EnableStartSessionButton(false);
        uIBehaviour.EnableMainMenuButton(false);

        if (SelectedDifficulty == "Training")
        {
            // Infinite session - player ends the session manually
            Debug.Log("Training mode started. Targets will spawn indefinitely.");
            // Start target spawning if necessary here
        }
        else
        {
            // Start timed session
            Debug.Log($"{SelectedDifficulty} mode started. Session timer: {sessionTimer} seconds.");
            sessionCoroutine = StartCoroutine(SessionCountdown());
        }
    }

    private IEnumerator SessionCountdown()
    {
        float remainingTime = sessionTimer;

        while (remainingTime > 0)
        {
            uIBehaviour.UpdateTimer((int) remainingTime);
            remainingTime -= Time.deltaTime;
            yield return null;
        }

        EndSession();
    }

    public void EndSession()
    {
        targetSpawner.StopSpawning();
        uIBehaviour.EnableStartSessionButton(true);
        uIBehaviour.EnableMainMenuButton(true);

        if (!isSessionActive) return;

        isSessionActive = false;

        if (sessionCoroutine != null)
        {
            StopCoroutine(sessionCoroutine);
            sessionCoroutine = null;
        }

        scoreManager.currentScore.name = Pseudo;
        scoreManager.currentScore.difficulty = SelectedDifficulty;
        scoreManager.AddScore(scoreManager.currentScore);
        Debug.Log("Session ended.");
        // Additional code to stop spawning targets, save scores, and handle session end
    }

    public void PlayerEndsTrainingSession()
    {
        if (SelectedDifficulty == "Training" && isSessionActive)
        {
            EndSession();
        }
    }

    public static string ToString()
    {
        return string.Format("bow: {0}, arrow: {1}, difficulty: {2}, pseudo: {3}", SelectedBow, SelectedArrow, SelectedDifficulty, Pseudo);
    }


}