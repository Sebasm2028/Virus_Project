using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Game State")]
    [SerializeField] private GameState gameState;

    [Header("Scene Changer")]
    [SerializeField] private string lobbyScene;
    [SerializeField] private string gameScene;


    public float gameTime = 120f;
    public TextMeshProUGUI timerText;
    public GameObject gameOverUI;
    public GameObject winUI;
    private bool gameEnded = false;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (gameEnded)
            return;

        gameTime -= Time.deltaTime;
        UpdateTimerUI();

        if (gameTime <= 0)
        {
            GameOver();
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    #region Scenes

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == gameScene) gameState = GameState.Playing;
        else if (scene.name == lobbyScene) gameState = GameState.Lobby;
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(gameTime / 60F);
        int seconds = Mathf.FloorToInt(gameTime % 60F);
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    public void GameOver()
    {
        gameEnded = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Win()
    {
        gameEnded = true;
        winUI.SetActive(true);
        Time.timeScale = 0f;
    }


    public void RestartGame()
    {
        Time.timeScale = 1f;
        gameEnded = false;
        // Reiniciar la escena
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }


    #endregion
}
