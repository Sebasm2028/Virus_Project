using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCanvasUI : MonoBehaviour
{
    [Header("Time Counter")]
    [SerializeField] private TMP_Text timeCounterText;

    [Header("Game Over Properties")]
    [SerializeField] private List<GameObject> disableGO;
    [SerializeField] private GameObject gameOverHolder;
    [SerializeField] private GameObject gameWinHolder;

    [Header("Properties")]
    [SerializeField] private float timeCounter;

    public void Initialize(float timeCounter)
    {
        this.timeCounter = timeCounter;
    }

    public void EnableGameOverScreen()
    {
        foreach (GameObject go in disableGO) { go.SetActive(false); }
        gameOverHolder.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void EnableGameWinScreen()
    {
        foreach (GameObject go in disableGO) { go.SetActive(false); }
        gameWinHolder.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnBackToMenuButton()
    {
        gameOverHolder.SetActive(false);
        gameWinHolder.SetActive(false);
        GameManager.Instance.gameCanvasUI = null;
        GameManager.Instance.LoadScene("MainLobby");
        GameManager.Instance.gameState = GameState.Lobby;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameState == GameState.Playing)
        {
            timeCounter -= Time.deltaTime;
            timeCounterText.text = $"{(int) timeCounter}";
        }

        if (timeCounter <= 0)
        {
            GameManager.Instance.CheckGameOver();
        }
    }
}
