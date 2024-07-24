using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnter : MonoBehaviour
{
    [Header("Things to enable")]
    [SerializeField] private List<GameObject> toEnable;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!triggered)
            {
                triggered = true;
                foreach (GameObject go in toEnable) { go.SetActive(true); }
            }
        }
    }
}
