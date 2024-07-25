using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCanvas : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        GameManager.Instance.LoadScene(sceneName);
    }
}
