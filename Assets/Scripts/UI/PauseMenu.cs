using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject buttonPause;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenuPause;
    private bool pauseGame = false;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.P)){
            if(pauseGame){
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Pause() {
        pauseGame = true;
        Time.timeScale = 0f;
        buttonPause.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void Resume() 
    {
        pauseGame = false;
        Time.timeScale = 1f;
        buttonPause.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void ShowOptions()
    {
        pauseMenu.SetActive(false);
        optionsMenuPause.SetActive(true);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; // Asegura que el juego esté reanudado antes de cambiar de escena
        SceneManager.LoadScene("MainMenu"); // Cambia a la escena del menú principal
    }
}
