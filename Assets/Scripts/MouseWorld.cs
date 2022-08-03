using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    private static MouseWorld _instance;
    private Camera _camera;
    [SerializeField] private LayerMask mousePlaneLayerMask;

    private void Awake()
    {
        _instance = this;
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var mousePosition = GetPosition();
            if (mousePosition.HasValue)
            {
                transform.position = mousePosition.Value;    
            }
        }
    }

    public static Vector3? GetPosition()
    {
        Ray ray = _instance._camera.ScreenPointToRay(Input.mousePosition);
        bool hit = Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _instance.mousePlaneLayerMask);
        if (hit)
        {
            return raycastHit.point;
        }

        return null;
    }
}