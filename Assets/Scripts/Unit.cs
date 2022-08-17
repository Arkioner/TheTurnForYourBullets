using UnityEngine;

public class Unit : MonoBehaviour
{
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float stoppingDistance = 0.05f;
    [SerializeField] private float rotationSpeed = 10f;
    private bool moveToTargetPosition;
    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;
        LevelGrid.SetUnitAtGridPosition(transform.position, this);
    }

    private void Update()
    {
        if (moveToTargetPosition)
        {
            var moveDirection = (targetPosition - transform.position).normalized;
            if (Vector3.Distance(transform.forward, moveDirection) > stoppingDistance)
            {
                transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
            }
            else
            {
                animator.SetBool(IsRunning, true);
                transform.position += moveDirection * (Time.deltaTime * moveSpeed);
                if (Vector3.Distance(transform.position, targetPosition) < stoppingDistance)
                {
                    transform.position = targetPosition;
                    moveToTargetPosition = false;
                    animator.SetBool(IsRunning, false);
                }
            }
        }
    }

    public void Move(Vector3 targetPosition)
    {
        if (LevelGrid.GetUnitAtGridPosition(targetPosition) == null)
        {
            LevelGrid.ClearUnitAtGridPosition(this.targetPosition);
            LevelGrid.SetUnitAtGridPosition(targetPosition, this);
            this.targetPosition = targetPosition;
            moveToTargetPosition = true;
            animator.SetBool(IsRunning, false);
        }
    }
}