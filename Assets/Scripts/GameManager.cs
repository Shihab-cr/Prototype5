using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public Slider volumeSlider;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    public Button restartButton;
    public bool isGameActive;
    private bool isPaused;
    private int score;
    private int lives = 3;
    private float spawnRate = 1.0f;
    public float volumeLevel = 1f;
    public AudioClip[] pauseGameClips;
    private void Awake()
    {
        isGameActive = false;
    }
    private void Start()
    {
        livesText.text = "Lives: "+lives;
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        pauseScreen.gameObject.SetActive(false);

        volumeLevel = PlayerPrefs.GetFloat("VolumeLevel", 1f);
        ChangeSliderOnLoad();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isGameActive) return;
            isPaused = !isPaused;
            if (isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }
    IEnumerator spawnTargets()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
        
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);  
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame(int difficultyLevel)
    {
        isGameActive = true;
        score = 0;
        spawnRate /=difficultyLevel;
        StartCoroutine(spawnTargets());
        UpdateScore(0);

        titleScreen.gameObject.SetActive(false);
    }
    public void DecrementLives()
    {
        if(!isGameActive) return;
        lives--;
        livesText.text = "Lives: " + lives;
        if (lives <= 0)
        {
           GameOver();
        }
    }

    public void PauseGame()
    {
        pauseScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
        if (pauseGameClips.Length > 0)
        {
            int index = Random.Range(0, pauseGameClips.Length);
            AudioSource.PlayClipAtPoint(pauseGameClips[index], transform.position);
        }
    }
    public void ResumeGame()
    {
        pauseScreen.gameObject.SetActive(false);
        Time.timeScale = 1;
        if (pauseGameClips.Length > 0)
        {
            int index = Random.Range(0, pauseGameClips.Length);
            AudioSource.PlayClipAtPoint(pauseGameClips[index], transform.position);
        }
    }
    public void ChangeVolumeLevel()
    {
        volumeLevel = volumeSlider.value;
        AudioListener.volume = volumeLevel;
        PlayerPrefs.SetFloat("VolumeLevel", volumeLevel);
        PlayerPrefs.Save();
    }
    private void ChangeSliderOnLoad()
    {
        volumeSlider.value = volumeLevel;
    }
}
