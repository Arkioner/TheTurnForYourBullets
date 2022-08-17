using System;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    private static UnitActionSystem instance;
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitPlaneLayerMask;
    private Camera mainCamera;

    private void Awake()
    {
        instance = this;
        mainCamera = Camera.main;
        UnitEventHandler += (sender, unitEvent) =>
        {
            if (unitEvent is UnitEvent.NewSelectedUnitEvent) selectedUnit = unitEvent.Unit;
        };
    }

    private void Start()
    {
        SelectUnit(selectedUnit);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (TryHandleUnitSelection()) return;

            var mousePosition = MouseWorld.GetPosition();
            if (mousePosition.HasValue) selectedUnit.Move(mousePosition.Value);
        }
    }

    private event EventHandler<UnitEvent> UnitEventHandler;

    private bool TryHandleUnitSelection()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var raycastHit, float.MaxValue, unitPlaneLayerMask))
            if (raycastHit.transform.TryGetComponent(out Unit unit))
            {
                SelectUnit(unit);
                return true;
            }

        return false;
    }

    private void SelectUnit(Unit unit)
    {
        UnitEventHandler?.Invoke(this, UnitEvent.NewSelectedUnit(unit));
    }

    public static void SubscribeToUnitEvent(EventHandler<UnitEvent> subscriber)
    {
        instance.UnitEventHandler += subscriber;
    }

    public static void UnSubscribeFromUnitEvent(EventHandler<UnitEvent> subscriber)
    {
        instance.UnitEventHandler -= subscriber;
    }

    public class UnitEvent
    {
        public readonly Unit Unit;

        private UnitEvent(Unit unit)
        {
            Unit = unit;
        }

        public static UnitEvent NewSelectedUnit(Unit unit)
        {
            return new NewSelectedUnitEvent(unit);
        }

        internal class NewSelectedUnitEvent : UnitEvent
        {
            internal NewSelectedUnitEvent(Unit unit) : base(unit)
            {
            }
        }
    }
}