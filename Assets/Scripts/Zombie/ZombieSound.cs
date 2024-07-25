using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSound : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    [Header("Movement")]
    [SerializeField] private List<AudioClip> walkSounds;

    [Header("Grounts")]
    [SerializeField] private List<AudioClip> grountSounds;

    [Header("Combat")]
    [SerializeField] private List<AudioClip> attackSounds;

    [Header("Script References")]
    [SerializeField] private ZombieMovement movement;
    [SerializeField] private ZombieStats stats;

    private void Start()
    {
        stats.OnZombieAttack += OnZombieAttacks;

        StartCoroutine(GrountCoroutine());
    }

    private void OnDisable()
    {
        stats.OnZombieAttack -= OnZombieAttacks;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (movement.GetAgent().velocity.magnitude > 0.1f)
        {
            if (!source.isPlaying)
            {
                source.PlayOneShot(walkSounds[Random.Range(0, walkSounds.Count)]);
            }
        }
    }

    private IEnumerator GrountCoroutine()
    {
        while (true)
        {
            if (!source.isPlaying)
            {
                source.PlayOneShot(grountSounds[Random.Range(0, grountSounds.Count)]);
            }

            yield return new WaitForSeconds(Random.Range(2, 6));
        }
    }

    private void OnZombieAttacks()
    {
        if (!source.isPlaying)
        {
            source.PlayOneShot(attackSounds[0]);
        }
    }

}
