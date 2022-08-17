using System;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cmvCamera;
    [SerializeField] private int minZoom = 1;
    [SerializeField] private int maxZoom = 10;
    private CinemachineTransposer cinemachineTransposer;

    private void Start()
    {
        cinemachineTransposer = cmvCamera.GetCinemachineComponent<CinemachineTransposer>();
    }

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    private void HandleZoom()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            cinemachineTransposer.m_FollowOffset.y = Math.Clamp(
                cinemachineTransposer.m_FollowOffset.y + Input.mouseScrollDelta.y,
                minZoom,
                maxZoom
            );
        }
    }

    private void HandleRotation()
    {
        Vector3 rotation = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.Q))
            rotation.y = 90f;
        if (Input.GetKeyDown(KeyCode.E))
            rotation.y = -90;
        transform.eulerAngles += rotation;
    }

    private void HandleMovement()
    {
        Vector3 inputMoveDir = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            inputMoveDir.z = 1f;
        if (Input.GetKey(KeyCode.A))
            inputMoveDir.x = -1f;
        if (Input.GetKey(KeyCode.S))
            inputMoveDir.z = -1f;
        if (Input.GetKey(KeyCode.D))
            inputMoveDir.x = 1f;
        transform.position += inputMoveDir.z * transform.forward + inputMoveDir.x * transform.right;
    }
}