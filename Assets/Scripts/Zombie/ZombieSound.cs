using UnityEngine;

public class ZombieSound : MonoBehaviour
{
    public AudioClip zombieSound; // El sonido continuo del zombie
    private Transform player; // Referencia al jugador
    public float maxVolumeDistance = 10f; // Distancia a la que el sonido del zombie está a volumen máximo
    public float minVolumeDistance = 2f; // Distancia a la que el sonido del zombie está a volumen mínimo

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        AudioManager.Instance.PlayEnemySound(zombieSound);
    }

    private void Update()
    {
        AdjustVolumeBasedOnDistance();
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
