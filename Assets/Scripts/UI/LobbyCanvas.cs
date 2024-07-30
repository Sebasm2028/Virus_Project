using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class LobbyCanvas : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        GameManager.Instance.LoadScene(sceneName);
    }

    public void SetSensX(float sensX)
    {
        GameManager.Instance.SetSensX(sensX);
    }

    public void SetSensY(float sensY)
    {
        GameManager.Instance.SetSensY(sensY);
    }

    public void ChangeMasterVolume(float volume)
    {
        AudioManager.Instance.ChangeMasterVolume(volume);
    }

    public void ChangeMusicVolume(float volume)
    {
        AudioManager.Instance.ChangeMusicVolume(volume);
    }

    public void ChangeSFXVolume(float volume)
    {
        AudioManager.Instance.ChangeSFXVolume(volume);
    }
}
