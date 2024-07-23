using UnityEngine;

public class ZombieSound : MonoBehaviour
{
    public AudioClip zombieSound; // El sonido continuo del zombie
    private Transform player; // Referencia al jugador
    public float maxVolumeDistance = 10f; // Distancia a la que el sonido del zombie está a volumen máximo
    public float minVolumeDistance = 2f; // Distancia a la que el sonido del zombie está a volumen mínimo

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player no encontrado. Asegúrate de que el objeto del jugador tenga el tag 'Player'.");
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayEnemySound(zombieSound);
        }
        else
        {
            Debug.LogError("AudioManager no encontrado. Asegúrate de que el AudioManager esté presente en la escena.");
        }
    }

    private void Update()
    {
        if (player != null && AudioManager.Instance != null)
        {
            AdjustVolumeBasedOnDistance();
        }
    }

    private void AdjustVolumeBasedOnDistance()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < minVolumeDistance)
        {
            AudioManager.Instance.SetEnemyVolume(1f); // Volumen máximo
        }
        else if (distance > maxVolumeDistance)
        {
            AudioManager.Instance.SetEnemyVolume(0f); // Volumen mínimo
        }
        else
        {
            // Ajustar volumen basado en la distancia
            float normalizedDistance = (distance - minVolumeDistance) / (maxVolumeDistance - minVolumeDistance);
            AudioManager.Instance.SetEnemyVolume(1f - normalizedDistance); // Invertir para que el volumen disminuya con la distancia
        }
    }
}
