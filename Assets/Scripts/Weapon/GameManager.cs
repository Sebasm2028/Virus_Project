using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public static GameManager Instance { get; private set; }

    public int gunAmmo = 10;

    public TextMeshProUGUI healtText;

    public int health = 100;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        ammoText.text = gunAmmo.ToString();
        healtText.text = health.ToString();
    }
}