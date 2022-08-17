using System;
using System.Collections.Concurrent;
using System.Timers;
using TMPro;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    private static LevelGrid instance;
    [SerializeField] private Transform gridDebugObjectPrefab;
    private readonly ConcurrentQueue<Action> debugQueue = new();
    private GridSystem gridSystem;

    private void Awake()
    {
        instance = this;
        gridSystem = new GridSystem(10, 10, 2);
        DrawGridDebug();
    }

    private void Update()
    {
        foreach (var action in debugQueue) action.Invoke();
    }

    private void DrawGridDebug()
    {
        for (var x = 0; x < 10; x++)
        for (var z = 0; z < 10; z++)
        {
            var gridPosition = new GridPosition(x, z);
            var gridObject = gridSystem.GetGridObject(gridPosition);
            var debugInstance = Instantiate(gridDebugObjectPrefab, gridSystem.GetWorldPosition(gridPosition), Quaternion.identity);
            var debugText = debugInstance.GetComponentInChildren<TextMeshPro>();
            var timer = new Timer(100);
            timer.AutoReset = true;
            timer.Elapsed += (_, _) => debugQueue.Enqueue(() => debugText.text = gridPosition + "\n" + gridObject.unit);
            timer.Enabled = true;
        }
    }

    public static void SetUnitAtGridPosition(Vector3 position, Unit unit)
    {
        instance.gridSystem.GetGridObject(GetGridPosition(position)).unit = unit;
    }

    public static Unit GetUnitAtGridPosition(Vector3 position)
    {
        return instance.gridSystem.GetGridObject(GetGridPosition(position)).unit;
    }

    public static void ClearUnitAtGridPosition(Vector3 position)
    {
        instance.gridSystem.GetGridObject(GetGridPosition(position)).unit = null;
    }

    private static GridPosition GetGridPosition(Vector3 position)
    {
        return instance.gridSystem.GetGridPosition(position);
    }
}