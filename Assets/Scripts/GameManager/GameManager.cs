using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game State")]
    [SerializeField] private GameState gameState;

    [Header("Scene Changer")]
    [SerializeField] private string lobbyScene;
    [SerializeField] private string gameScene;

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

    #endregion
}
