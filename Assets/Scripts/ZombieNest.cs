using UnityEngine;
using UnityEngine.UI;

public class ZombieNest : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Slider healthSlider; // UI Slider para mostrar la vida

    public GameObject zombiePrefab;
    public float spawnInterval = 5f;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        InvokeRepeating("SpawnZombie", 2f, spawnInterval);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(10); // Ajusta el daño según sea necesario
            Destroy(other.gameObject); // Destruye la bala
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            DestroyNest();
        }
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth / maxHealth;
        }
    }

    private void DestroyNest()
    {
        // Aquí puedes añadir lógica adicional, como reproducir una animación de destrucción
        Debug.Log("Nest destroyed! You win!");
        // Mostrar mensaje de victoria o cargar la siguiente escena
    }

    private void SpawnZombie()
    {
        if (currentHealth > 0)
        {
            Instantiate(zombiePrefab, transform.position, transform.rotation);
        }
    }
}
