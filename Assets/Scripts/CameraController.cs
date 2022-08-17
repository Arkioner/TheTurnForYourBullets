using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 inputMoveDir = Vector3.zero;
        Vector3 rotation = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            inputMoveDir.z = 1f;
        if (Input.GetKey(KeyCode.A))
            inputMoveDir.x = -1f;
        if (Input.GetKey(KeyCode.S))
            inputMoveDir.z = -1f;
        if (Input.GetKey(KeyCode.D))
            inputMoveDir.x = 1f;
        if (Input.GetKeyDown(KeyCode.Q))
            rotation.y = 90f;
        if (Input.GetKeyDown(KeyCode.E))
            rotation.y = -90;
        
        transform.position += inputMoveDir.z * transform.forward + inputMoveDir.x * transform.right;
        transform.eulerAngles += rotation;
    }
}