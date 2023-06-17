using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{
    public float totalTime = 60f; // Total time for the timer in seconds
    public Text timerText; // Reference to the UI text element displaying the timer
    public GameObject gameOverPanel; // Reference to the game over panel or screen

    private float currentTime; // Current time remaining
    private bool isTimerRunning; // Flag to check if the timer is running

    private void Start()
    {
        gameOverPanel.SetActive(false);
        currentTime = totalTime;
        isTimerRunning = true;
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            // Decrement the timer each frame
            currentTime -= Time.deltaTime;

            // Check if the timer has reached zero or below
            if (currentTime <= 0f)
            {
                currentTime = 0f; // Ensure the timer doesn't go below zero
                isTimerRunning = false;
                GameOver();
            }

            // Update the UI text element with the current time
            timerText.text = currentTime.ToString("0");
        }
    }

    public void restart()
    {
        SceneManager.LoadScene(0);
    }

    private void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
