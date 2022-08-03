using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private Vector3 targetPosition;
    private bool moveToTargetPosition = false;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float stoppingDistance = 0.1f;
    [SerializeField] private float rotationSpeed = 10f;

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (moveToTargetPosition)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
            transform.position += moveDirection * (Time.deltaTime * moveSpeed);
            if (Vector3.Distance(transform.position, targetPosition) < stoppingDistance)
            {
                transform.position = targetPosition;
                moveToTargetPosition = false;
                animator.SetBool(IsRunning, false);
            }
        }
    }

    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
        moveToTargetPosition = true;
        animator.SetBool(IsRunning, true);
    }
}