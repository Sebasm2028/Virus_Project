using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform cameraHolderTransform;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        AssignCameraPosition();
    }

    private void AssignCameraPosition()
    {
        mainCamera.transform.position = cameraHolderTransform.position;
    }

}
