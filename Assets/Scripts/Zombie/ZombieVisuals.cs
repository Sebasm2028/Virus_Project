using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieVisuals : MonoBehaviour
{
    [Header("Zombie Random Skin Properties")]
    [SerializeField] private GameObject[] zombieSkins;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        ApplyRandomSkin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ApplyRandomSkin()
    {
        foreach (var zombie in zombieSkins)
        {
            zombie.SetActive(false);
        }

        zombieSkins[Random.Range(0, zombieSkins.Length)].SetActive(true);
    }
}
