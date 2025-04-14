using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score;
    private int lives;

    public TMP_Text scoreDisplay;
    public TMP_Text livesDisplay;
    public TMP_Text gameOverDisplay;

    public void AddScore()
    {
        score++;
        UpdateScoreDisplay();
    }

    public void RemoveLife()
    {
        lives--;
        UpdateLivesDisplay();

        if (IsGameOver())
        {
            gameOverDisplay.enabled = true;
        }
    }

    public void ResetGame()
    {
        score = 0;
        lives = 3;
    }

    public bool IsGameOver()
    {
        return lives <= 0;
    }

    private void UpdateScoreDisplay()
    {
        scoreDisplay.text = $"Score: {score}";
    }
    private void UpdateLivesDisplay()
    {
        livesDisplay.text = $"Lives: {lives}";
    }

    private void Start()
    {
        ResetGame();
        UpdateScoreDisplay();
        UpdateLivesDisplay();
        gameOverDisplay.enabled = false;
    }
    private void Update()
    {
        // Reload scene on game over with keypress
        if (IsGameOver())
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Scene current = SceneManager.GetActiveScene();
                SceneManager.LoadScene(current.name);
            }
        }
    }
}
