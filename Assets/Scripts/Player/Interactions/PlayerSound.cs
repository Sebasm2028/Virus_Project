using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [Header("Footsteps")]
    [SerializeField] private AudioClip walkSound;

    [Header("Script References")]
    [SerializeField] private PlayerMovement movement;

    private void Update()
    {
        if (movement.isMoving() && movement.isGrounded())
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

