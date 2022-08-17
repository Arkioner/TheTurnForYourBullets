using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    private static MouseWorld instance;
    [SerializeField] private LayerMask mousePlaneLayerMask;
    private Camera mainCamera;

    private void Awake()
    {
        instance = this;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var mousePosition = GetPosition();
            if (mousePosition.HasValue) transform.position = mousePosition.Value;
        }
    }

    public static Vector3? GetPosition()
    {
        var ray = instance.mainCamera.ScreenPointToRay(Input.mousePosition);
        var hit = Physics.Raycast(ray, out var raycastHit, float.MaxValue, instance.mousePlaneLayerMask);
        if (hit) return raycastHit.point;

        return null;
    }
}