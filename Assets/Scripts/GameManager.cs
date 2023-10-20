using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public HealthSystem playerHealth;
    public Transform deathZone;
    public TMP_Text gameOverText;
    public TMP_Text timerText;
    public GameObject gameOverPanel;
    private bool gameOver = false;
    private bool levelCompleted = false;
    private bool timerStarted = false;
    private float levelTime = 0f;
    void Update()
    {
        if (!gameOver && !levelCompleted)
        {
            if (playerHealth.transform.position.y < deathZone.position.y || playerHealth.currentHealth <= 0)
            {
                GameOver(false);
            }

            if (!gameOver && !levelCompleted && timerStarted)
            {
                levelTime += Time.deltaTime;
                timerText.text = "Время: " + levelTime.ToString("F2");
            }
        }
    }
    public void StartTimer()
    {
        timerStarted = true;
    }
    public void LevelCompleted()
    {
        levelCompleted = true;
        gameOverText.text = "Победа!" + "\nВаше время: " + levelTime.ToString("F2") + " сек";
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; 
    }

    void GameOver(bool victory)
    {
        gameOver = true;

        if (victory)
        {
            LevelCompleted();
        }
        else
        {
            gameOverText.text = "Поражение!";
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f; 
        }

        PlayerLocomotion playerLocomotion = FindObjectOfType<PlayerLocomotion>();
        if (playerLocomotion != null)
        {
            playerLocomotion.enabled = false;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverPanel.SetActive(false);
    }
}
