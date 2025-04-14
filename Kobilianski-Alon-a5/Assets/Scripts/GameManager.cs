using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score;
    private int lives;
    private bool gameStarted = false;

    public TMP_Text scoreDisplay;
    public TMP_Text livesDisplay;
    public TMP_Text gameOverDisplay;
    public TMP_Text startScreenDisplay;
    public TMP_Text winDisplay;

    public AudioSource musicSource;     
    public AudioClip backgroundMusic;  

    public AudioSource audioSource;
    public AudioClip scoreSound;
    public AudioClip loseLifeSound;
    public AudioClip startSound;
    public AudioClip gameOverSound;

    private void Start()
    {
        Time.timeScale = 0f;
        startScreenDisplay.enabled = true;
        gameOverDisplay.enabled = false;
    }

    public void StartGame()
    {
        gameStarted = true;
        Time.timeScale = 1f;
        startScreenDisplay.enabled = false;
        ResetGame();
        UpdateScoreDisplay();
        UpdateLivesDisplay();
        PlaySound(startSound);
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true; 
            musicSource.Play();
        }

    }

    public void AddScore()
    {
        if (!gameStarted) return;

        score++;
        UpdateScoreDisplay();
        PlaySound(scoreSound);
    }

    public void RemoveLife()
    {
        if (!gameStarted) return;

        lives--;
        UpdateLivesDisplay();
        PlaySound(loseLifeSound);

        if (IsGameOver())
        {
            gameOverDisplay.enabled = true;
            PlaySound(gameOverSound);

            if (musicSource != null && musicSource.isPlaying)
            {
                musicSource.Stop();
            }
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

    private void CheckWinCondition()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        if (asteroids.Length == 0 && !IsGameOver())
        {
            winDisplay.enabled = true;
            gameStarted = false;
            Time.timeScale = 0f;

            if (musicSource != null && musicSource.isPlaying)
            {
                musicSource.Stop();
            }
        }
    }


    private void UpdateScoreDisplay()
    {
        scoreDisplay.text = $"Score: {score}";
    }

    private void UpdateLivesDisplay()
    {
        livesDisplay.text = $"Lives: {lives}";
    }

    private void Update()
    {
        if (!gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartGame();
            }
        }

        if (IsGameOver())
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Scene current = SceneManager.GetActiveScene();
                SceneManager.LoadScene(current.name);
            }
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
