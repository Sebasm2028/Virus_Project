using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource enemySource;
    [SerializeField] private AudioSource playerSource;
    [SerializeField] private AudioSource alarmSource;
    [SerializeField] private AudioSource hurtSource;
    [SerializeField] private AudioSource stabbingKnifeSource; // Nuevo AudioSource para el cuchillo
    [SerializeField] private AudioSource shootgunSource; // Nuevo AudioSource para la pistola

    [Header("Audio Mixers")]
    [SerializeField] private AudioMixer master;

    public static AudioManager Instance;

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
    public void PlayHurtSound()
    {
        if (hurtSource != null)
        {
            hurtSource.Play();
        }
    }
    public void PlayStabbingKnifeSound()
    {
        if (stabbingKnifeSource != null)
        {
            stabbingKnifeSource.Play();
        }
    }

    public void PlayShootgunSound()
    {
        if (shootgunSource != null)
        {
            shootgunSource.Play();
        }
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

