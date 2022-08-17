using UnityEngine;

public class Unit : MonoBehaviour
{
    private Vector3 targetPosition;
    private UnitMoveAction unitMoveAction;
    private void Start()
    {
        unitMoveAction = GetComponent<UnitMoveAction>();
        targetPosition = transform.position;
        LevelGrid.SetUnitAtGridPosition(targetPosition, this);
    }

    private void Update()
    {
    }

    public void Move(Vector3 targetPosition)
    {
        if (LevelGrid.GetUnitAtGridPosition(targetPosition) == null)
        {
            LevelGrid.ClearUnitAtGridPosition(this.targetPosition);
            LevelGrid.SetUnitAtGridPosition(targetPosition, this);
            this.targetPosition = targetPosition;
            unitMoveAction.Move(targetPosition);
        }
    }
}