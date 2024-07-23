using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource enemySource;
    public AudioSource playerSource; // Nuevo AudioSource para el sonido del jugador
    public AudioSource alarmSource; // Nuevo AudioSource para la alarma

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

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayEnemySound(AudioClip clip)
    {
        enemySource.clip = clip;
        enemySource.loop = true;
        enemySource.Play();
    }

    public void PlayPlayerSound(AudioClip clip)
    {
        playerSource.clip = clip;
        playerSource.loop = true;
        playerSource.Play();
    }

    public void PlayAlarmSound(AudioClip clip)
    {
        alarmSource.clip = clip;
        alarmSource.Play();
    }

    public void StopEnemySound()
    {
        enemySource.Stop();
    }

    public void StopPlayerSound()
    {
        playerSource.Stop();
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void SetEnemyVolume(float volume)
    {
        enemySource.volume = volume;
    }

    public void SetPlayerVolume(float volume)
    {
        playerSource.volume = volume;
    }

    public void SetAlarmVolume(float volume)
    {
        alarmSource.volume = volume;
    }
}

