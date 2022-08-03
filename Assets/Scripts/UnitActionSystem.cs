using System;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    private static UnitActionSystem _instance;
    private Camera _camera;
    private event EventHandler<UnitEvent> UnitEventHandler;
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitPlaneLayerMask;

    private void Awake()
    {
        _instance = this;
        _camera = Camera.main;
        UnitEventHandler += (sender, unitEvent) =>
        {
            if (unitEvent is UnitEvent.NewSelectedUnitEvent) selectedUnit = unitEvent.unit;
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
            if (mousePosition.HasValue)
            {
                selectedUnit.Move(mousePosition.Value);
            }
        }
    }
    
    private bool TryHandleUnitSelection()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitPlaneLayerMask))
        {
            if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                SelectUnit(unit);
                return true;
            }
        }
        return false;
    }

    private void SelectUnit(Unit unit)
    {
        UnitEventHandler?.Invoke(this, UnitEvent.NewSelectedUnit(unit)); 
    }

    public class UnitEvent
    {
        public readonly Unit unit;

        private UnitEvent(Unit unit)
        {
            this.unit = unit;
        }
        
        internal class NewSelectedUnitEvent : UnitEvent
        {
            internal NewSelectedUnitEvent(Unit unit) : base(unit) {}
        }

        public static UnitEvent NewSelectedUnit(Unit unit)
        {
            return new NewSelectedUnitEvent(unit);
        }
    }

    public static void SubscribeToUnitEvent(EventHandler<UnitEvent> subscriber)
    {
        _instance.UnitEventHandler += subscriber;
    }
    
    public static void UnSubscribeFromUnitEvent(EventHandler<UnitEvent> subscriber)
    {
        _instance.UnitEventHandler -= subscriber;
    }
}
