using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] private Unit unit;

    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
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
            if (unitEvent.unit == this.unit)
            {
                _meshRenderer.enabled = true;
            }
            else
            {
                _meshRenderer.enabled = false;
            }
        }
    }
}