using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Camera Properties")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float walkFOV;
    [SerializeField] private float runFOV;
    [SerializeField] private float actualFOV;
    [SerializeField] private Transform cameraHolderTransform;

    [Header("Mouse Properties")]
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;
    [SerializeField] private Vector2 mouseLook;
    float xRotation;
    float yRotation;

    [Space]

    #region References
    [SerializeField ]private Transform playerTransform;
    private PlayerControls playerControls;
    #endregion

    private void Awake()
    {
        mainCamera = Camera.main;
        playerControls = new PlayerControls();
        playerControls.Camera.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        CameraLook();
        RotatePlayer();
    }

    #region Inputs

    private void GetInputs()
    {
        mouseLook = playerControls.Camera.CameraLook.ReadValue<Vector2>();
    }

    #endregion

    private void CameraLook()
    {
        if (mouseLook != Vector2.zero)
        {
            float mouseX = mouseLook.x * sensX;
            float mouseY = mouseLook.y * sensY;

            xRotation -= mouseY;
            yRotation += mouseX;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            cameraHolderTransform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
            mainCamera.transform.rotation = cameraHolderTransform.rotation;
        }
    }

    private void RotatePlayer()
    {
        playerTransform.rotation = Quaternion.Euler(0, yRotation, 0f);
    }
}
