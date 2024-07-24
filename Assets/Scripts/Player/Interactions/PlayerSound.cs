using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioClip walkSound;

    private void Update()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            if (!AudioManager.Instance.playerSource.isPlaying)
            {
                AudioManager.Instance.PlayPlayerSound(walkSound);
            }
        }
        else
        {
            AudioManager.Instance.StopPlayerSound();
        }
    }
}

