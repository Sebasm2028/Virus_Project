using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("Sensitivity Slider")]
    [SerializeField] private Slider sliderSensitivity;
    [SerializeField] private CameraMovement cameraMovement;

    private void Start()
    {
        // añade un listener al slider que llama al método UpdateSensitivity cada vez que el valor del slider cambia.
        sliderSensitivity.onValueChanged.AddListener(UpdateSensitivity);
    }

    private void UpdateSensitivity(float value) // es un método que toma un valor flotante (el nuevo valor del slider) y llama al método UpdateSensitivity en el script CameraMovement con este valor.
    {
        cameraMovement.UpdateSensitivity(value); //  actualiza la sensibilidad del mouse en el script CameraMovement.
    }
}