using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSway : MonoBehaviour
{
    [Header("Sway Settings")]
    [SerializeField] private float smooth;
    [SerializeField] private float swayEffect;

    [Header("Multipliers")]
    [SerializeField] private float handMultiplier;
    [SerializeField] private float itemMultiplier;

    private CameraMovement cameraMovement;
    private Quaternion defaultRotation;
    private PlayerControls playerInput;

    private void Awake()
    {
        defaultRotation = transform.localRotation;
        playerInput = new PlayerControls();
        playerInput.Camera.Enable();
        cameraMovement = GetComponentInParent<CameraMovement>();

    }

    private void Update()
    {
        sway();
    }

    private void sway()
    {
        Vector2 mouseLook = playerInput.Camera.CameraLook.ReadValue<Vector2>();

        // get mouse input
        float mouseX = mouseLook.x * getSwayEffect() * cameraMovement.GetSens().x;
        float mouseY = mouseLook.y * getSwayEffect() * cameraMovement.GetSens().y;

        // calculate target rotation
        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        //if wanna move vertically, add rotationX
        Vector3 newRotation = new Vector3(0, rotationY.eulerAngles.y, 0);
        Quaternion targetRotation = Quaternion.Euler(newRotation);

        // rotate 
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }

    private float getSwayEffect()
    {
        float newSwayEffect = 0;

        newSwayEffect = swayEffect * handMultiplier;

        return newSwayEffect;
    }
}
