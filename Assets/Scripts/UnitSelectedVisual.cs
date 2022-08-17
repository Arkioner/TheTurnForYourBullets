using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] private Unit unit;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        UnitActionSystem.SubscribeToUnitEvent(OnNewSelectedUnitEvent);
    }

    private void OnDestroy()
    {
        UnitActionSystem.UnSubscribeFromUnitEvent(OnNewSelectedUnitEvent);
    }

    private void OnNewSelectedUnitEvent(object sender, UnitActionSystem.UnitEvent unitEvent)
    {
        if (unitEvent is UnitActionSystem.UnitEvent.NewSelectedUnitEvent)
        {
            if (unitEvent.Unit == unit)
                meshRenderer.enabled = true;
            else
                meshRenderer.enabled = false;
        }
    }
}