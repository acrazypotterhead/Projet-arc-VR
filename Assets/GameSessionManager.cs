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
    private float sessionTimer = 60f;  // Default session duration for timed modes
    private Coroutine sessionCoroutine;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
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
        isSessionActive = true;

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
            remainingTime -= Time.deltaTime;
            yield return null;
        }

        EndSession();
    }

    public void EndSession()
    {
        if (!isSessionActive) return;

        isSessionActive = false;

        if (sessionCoroutine != null)
        {
            StopCoroutine(sessionCoroutine);
            sessionCoroutine = null;
        }

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