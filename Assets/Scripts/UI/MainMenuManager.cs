using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject instructionsMenu;
    public GameObject optionsMenu;

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene"); // Cambia "GameScene" por el nombre de tu escena de juego.
    }

    public void ShowInstructions()
    {
        mainMenu.SetActive(false);
        instructionsMenu.SetActive(true);
    }

    public void ExitInstructions()
    {
        instructionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ShowOptions()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    
    public void ExitOptions()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}