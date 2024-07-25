using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Audios")]
    [SerializeField] private AudioClip lobbyMusic;
    [SerializeField] private AudioClip levelAmbient;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;

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

    void Update()
    {
        switch (GameManager.Instance.gameState)
        {
            case GameState.Playing:
                PlayMusic(levelAmbient);
                break;
            case GameState.Lobby:
                PlayMusic(lobbyMusic);
                break;
        }
    }

    void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip != clip)
        {
            musicSource.Stop();
            musicSource.clip = clip;
            musicSource.Play();
        }
    }

}

